using Quoter.App.Forms;
using Quoter.App.Helpers;
using Quoter.App.Models;
using Quoter.App.Services;
using Quoter.App.Services.Forms;
using Quoter.Framework.Enums;
using Quoter.Framework.Models;
using Quoter.Framework.Models.ImportExport;
using Quoter.Framework.Services.ImportExport;
using Quoter.Framework.Services.Messaging;
using System.Globalization;

namespace Quoter.App.FormsControllers.Welcome
{
	public class WelcomeFormController : IWelcomeFormController
	{
		private readonly ISettings _settings;
		private readonly IMessagingService _messagingService;
		private readonly IFormsManager _formsManager;
		private readonly IImportService _importService;
		private IWelcomeForm _form;


		private List<CollectionFileModel> _importableCollections;

		public WelcomeFormController(ISettings settings, IMessagingService messagingService, IFormsManager formsManager, IImportService importService)
		{
			_settings = settings;
			_messagingService = messagingService;
			_formsManager = formsManager;
			_importService = importService;
			_importableCollections = new List<CollectionFileModel>();
		}

		public void RegisterForm(IWelcomeForm form)
		{
			_form = form;
		}

		public Task EventFormClosingAsync()
		{
			return Task.CompletedTask;
		}

		public Task EventFormLoadedAsync()
		{
			string collectionsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Collections");


			string[] roFiles = Directory.GetFiles(Path.Combine(collectionsDir, "ro"));
			string[] enFiles = Directory.GetFiles(Path.Combine(collectionsDir, "en"));
			string[] frFiles = Directory.GetFiles(Path.Combine(collectionsDir, "fr"));

			AddCollectionFiles(roFiles, EnumLanguage.Romanian);
			AddCollectionFiles(enFiles, EnumLanguage.English);
			AddCollectionFiles(frFiles, EnumLanguage.French);

			_form.SetImportableCollections(_importableCollections.Where(f => f.Language == LanguageHelper.GetEnumLanguageFromString(_settings.Language)).ToList());
			return Task.CompletedTask;
		}

		private void AddCollectionFiles(string[] files, EnumLanguage language)
		{
			foreach (string file in files)
			{
				string fileName = Path.GetFileNameWithoutExtension(file);
				_importableCollections.Add(new CollectionFileModel()
				{
					Name = fileName,
					Language = language,
					FilePath = file
				});
			}
		}

		public void FinishSetup()
		{
			_settings.IsSetupFinished = true;
			//throw new NotImplementedException();
		}

		public void SetLanguage(EnumLanguage language)
		{
			switch (language)
			{
				case EnumLanguage.English:
					CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
					Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
					_settings.Language = "en-US";
					_form.SetSelectedLanguage(language);
					break;
				case EnumLanguage.Romanian:
					CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ro-RO");
					Thread.CurrentThread.CurrentUICulture = new CultureInfo("ro-RO");
					_settings.Language = "ro-RO";
					_form.SetSelectedLanguage(language);
					break;
				case EnumLanguage.French:
					CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("fr-FR");
					Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
					_settings.Language = "fr-FR";
					_form.SetSelectedLanguage(language);
					break;
			}
			_form.SetImportableCollections(_importableCollections.Where(f => f.Language == LanguageHelper.GetEnumLanguageFromString(_settings.Language)).ToList());
			_messagingService.SendMessage(Event.LanguageChanged);
		}

		public void SetNotificationsInterval(int interval)
		{
			_settings.NotificationIntervalSeconds = interval * 60;
		}

		public void SetSelectedTab(EnumWelcomeTab tab)
		{
			switch (tab)
			{
				// This case is when the user leaves the collection choose tab
				case EnumWelcomeTab.SetNotificationSettings:
					List<CollectionFileModel> lstCollectionFiles = _form.GetSelectedCollections();
					if (lstCollectionFiles.Count == 0)
					{
						DialogModel dialogModel = new DialogModel()
						{
							Title = "Error",
							Message = "Please choose at least 1 collection",
							TitleColor = Const.ColorWarn,
							MessageBoxButtons = EnumDialogButtons.Ok
						};
						_formsManager.ShowDialog<DialogMessageForm>(dialogModel);
					}
					else
					{
						// Begin import of selected collections
						_form.SetTab(EnumWelcomeTab.SetNotificationSettings);
						string[] filesToImport = _form.GetSelectedCollections().Select(c => c.FilePath).ToArray();
						ImportParameters importParameters = new ImportParameters()
						{
							Files = filesToImport,
							IsFavourite = true,			// Set the imported collections by default as favourites
							IsMergeCollections = true	// In case the user goes back and forth, we don't want to duplicate the import
						};
						_importService.QueueImportJob(importParameters);
					}
					break;
				default:
					_form.SetTab(tab);
					break;
			}
		}
	}
}
