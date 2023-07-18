using Newtonsoft.Json;
using Quoter.Framework.Data;
using Quoter.Framework.Models.ImportExport;
using Quoter.Framework.Services.ImportExport.ImportStrategies;
using Quoter.Framework.Services.Messaging;

namespace Quoter.Framework.Services.ImportExport
{
	/// <summary>
	/// Service used to import collection of books,chapters,quotes
	/// </summary>
	public class ImportService : IImportService
	{
		private readonly object _lock = new object();
		private readonly QuoterContext _context;
		private readonly IMessagingService _messagingService;
		private readonly ILogger _logger;
		private readonly IImportStrategyServiceFactory _importStrategyFactory;

		private bool _isImportInProgress;
		public bool IsImportInProgress
		{
			get
			{
				lock (_lock)
				{
					return _isImportInProgress;
				}
			}
			private set
			{
				lock (_lock)
				{
					_isImportInProgress = value;
				}
			}
		}

		public ImportService(
			QuoterContext context,
			IMessagingService messagingService,
			ILogger logger,
			IImportStrategyServiceFactory importStrategyFactory)
		{
			_context = context;
			_messagingService = messagingService;
			_logger = logger;
			_importStrategyFactory = importStrategyFactory;
		}

		/// <inheritdoc/>
		public void QueueImportJob(ImportParameters importParameters)
		{
			Task.Run(async () =>
			{
				bool canStart = await WaitForCurrentJobToFinish();
				if (canStart)
				{
					await BeginImport(importParameters);
				}
			}).ConfigureAwait(false);
		}

		private async Task<bool> WaitForCurrentJobToFinish()
		{
			// We want to have a single import job at any one time
			// so we wait before starting another one
			DateTime startTime = DateTime.Now;
			while (IsImportInProgress)
			{
				if (DateTime.Now > startTime.AddHours(1))
				{
					// Something might be off, so reset the bool
					// and reject the pending job
					IsImportInProgress = false;
					return false;
				}
				await Task.Delay(3000);
			}
			return true;
		}

		public async Task BeginImport(ImportParameters importParameters)
		{
			PostedAnnouncement announcement = _messagingService.PostAnnouncement(Event.ImportInProgress, "");
			try
			{
				IsImportInProgress = true;
				string importedCollections = string.Empty;
				foreach (string file in importParameters.Files)
				{
					importedCollections += await ImportFileAsync(file, importParameters);
				}

				_messagingService.SendMessage(Event.ImportSuccesfull, new ImportResult()
				{
					ImportedFilesMessage = importedCollections,
					NotifyUser = importParameters.NotifyUser
				});
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
				announcement.Remove();
				_messagingService.SendMessage(Event.ImportFailed, ex.Message);
			}
			finally
			{
				announcement.Remove();
				IsImportInProgress = false;
			}
		}

		private async Task<string> ImportFileAsync(string file, ImportParameters importParameters)
		{
			_logger.Info($"Starting Import on file: {file}");

			string fileContent = await File.ReadAllTextAsync(file);
			if (string.IsNullOrEmpty(fileContent))
			{
				return string.Empty;
			}

			ImportExportModel? importModel = JsonConvert.DeserializeObject<ImportExportModel>(fileContent);

			IImportStrategyService importStrategyService = _importStrategyFactory.GetImportStrategyService(importParameters.ImportStrategy);
			await importStrategyService.ImportAsync(importModel, importParameters);

			string importedCollectionNames = string.Empty;
			foreach (CollectionModel collModel in importModel.Collections)
			{
				importedCollectionNames += Environment.NewLine + collModel.Name;
			}
			return importedCollectionNames;
		}

	}
}
