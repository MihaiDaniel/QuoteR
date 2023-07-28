using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quoter.Framework.Data;
using Quoter.Framework.Entities;
using Quoter.Framework.Models.ImportExport;
using Quoter.Framework.Services.Messaging;
using Quoter.Shared.Models;
using System.Reflection;
using System.Text.Json;

namespace Quoter.Framework.Services.ImportExport
{
	/// <summary>
	/// <see cref="IExportService"/> implementation
	/// </summary>
	public class ExportService : IExportService
	{
		private readonly object _lock = new object();
		private readonly QuoterContext _context;
		private readonly IMessagingService _messagingService;

		private bool _isExportInProgress;
		private bool IsExportInProgress
		{
			get
			{
				lock (_lock)
				{
					return _isExportInProgress;
				}
			}
			set
			{
				lock (_lock)
				{
					_isExportInProgress = value;
				}
			}
		}

		public ExportService(QuoterContext context, IMessagingService messagingService)
		{
			_context = context;
			_messagingService = messagingService;
		}

		/// <inheritdoc/>
		public void QueueExportJob(ExportParameters exportParameters)
		{
			Task.Run(async () =>
			{
				while (IsExportInProgress)
				{
					await Task.Delay(1000);
				}
				await BeginExport(exportParameters);
			}).ConfigureAwait(false);
		}

		private async Task BeginExport(ExportParameters exportParameters)
		{
			PostedAnnouncement announcement = _messagingService.PostAnnouncement<string>(Event.ExportInProgress, "");
			try
			{
				IsExportInProgress = true;

				// Prepare query
				IQueryable<Collection> queryCollections = _context.Collections
						.Include(c => c.LstBooks)
							.ThenInclude(c => c.LstChapters)
								.ThenInclude(c => c.LstQuotes)
						.AsQueryable();
				if (exportParameters.IsExportOnlyFavouriteCollections)
				{
					queryCollections = queryCollections.Where(c => c.IsFavourite == true);
				}
				if (exportParameters.Language != null)
				{
					queryCollections = queryCollections.Where(c => c.Language == exportParameters.Language);
				}

				// Execute & get export models
				List<Collection> lstCollections = await queryCollections.ToListAsync();
				ImportExportModel exportModel = GetExportModel(lstCollections);

				// Serialize & write
				string content = JsonConvert.SerializeObject(exportModel);
				await File.WriteAllTextAsync(exportParameters.ExportFilePath, content);

				_messagingService.SendMessage(Event.ExportSucessfull, new ActionResult(true, exportParameters.ExportFilePath));
			}
			catch (Exception ex)
			{
				
				_messagingService.SendMessage(Event.ExportFailed, ex.Message);
			}
			finally
			{
				announcement.Remove();
				IsExportInProgress = false;
			}
		}

		private ImportExportModel GetExportModel(List<Collection> lstCollections)
		{
			ImportExportModel export = new ImportExportModel()
			{
				Id = Guid.NewGuid(),
				Version = Assembly.GetEntryAssembly().GetName().Version.ToString(),
				Collections = new List<CollectionModel>(),
				Books = new List<BookModel>(),
				Chapters = new List<ChapterModel>(),
				Quotes = new List<QuoteModel>()
			};

			foreach (Collection collection in lstCollections)
			{
				export.Collections.Add(GetCollectionExportModel(collection));

				if (collection.LstBooks != null && collection.LstBooks.Any())
				{
					foreach (Book book in collection.LstBooks)
					{
						export.Books.Add(GetBookExportModel(book));

						if (book.LstChapters != null && book.LstChapters.Any())
						{
							foreach (Chapter chapter in book.LstChapters)
							{
								export.Chapters.Add(GetChapterExportModel(chapter));
							}
						}
					}
				}
				if (collection.LstQuotes != null && collection.LstQuotes.Any())
				{
					foreach (Quote quote in collection.LstQuotes)
					{
						export.Quotes.Add(GetQuoteExportModel(quote));
					}
				}
			}
			return export;
		}

		private BookModel GetBookExportModel(Book book)
		{
			return new BookModel()
			{
				CollectionId = book.CollectionId,
				Description = book.Description,
				BookId = book.BookId,
				IsFavourite = book.IsFavourite,
				Name = book.Name,
			};
		}

		private CollectionModel GetCollectionExportModel(Collection collection)
		{
			return new CollectionModel()
			{
				CollectionId = collection.CollectionId,
				Description = collection.Description,
				IsFavourite = collection.IsFavourite,
				Name = collection.Name,
				Language = collection.Language,
			};
		}

		private ChapterModel GetChapterExportModel(Chapter chapter)
		{
			return new ChapterModel()
			{
				Name = chapter.Name,
				BookId = chapter.BookId,
				ChapterId = chapter.ChapterId,
				ChapterIndex = chapter.ChapterIndex,
				Description = chapter.Description,
				IsFavourite = chapter.IsFavourite,
			};
		}

		private QuoteModel GetQuoteExportModel(Quote quote)
		{
			return new QuoteModel()
			{
				BookId = quote.BookId,
				ChapterId = quote.ChapterId,
				CollectionId = quote.CollectionId,
				Content = quote.Content,
				Description = quote.Description,
				QuoteId = quote.QuoteId,
				QuoteIndex = quote.QuoteIndex
			};
		}
	}
}
