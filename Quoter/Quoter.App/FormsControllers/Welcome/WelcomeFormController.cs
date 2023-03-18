using Quoter.App.Forms;
using Quoter.App.Helpers;
using Quoter.App.Models;
using Quoter.App.Services;
using Quoter.App.Services.Forms;
using Quoter.Framework.Enums;
using Quoter.Framework.Models.ImportExport;
using Quoter.Framework.Services.Messaging;
using System.Globalization;

namespace Quoter.App.FormsControllers.Welcome
{
	public class WelcomeFormController : IWelcomeFormController
	{
		private readonly ISettings _settings;
		private readonly IMessagingService _messagingService;
		private readonly IFormsManager _formsManager;
		private IWelcomeForm _form;

		public WelcomeFormController(ISettings settings, IMessagingService messagingService, IFormsManager formsManager)
		{
			_settings = settings;
			_messagingService = messagingService;
			_formsManager = formsManager;
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
			List<CollectionFileModel> files = new List<CollectionFileModel>();
			files.Add(new CollectionFileModel() { Name = "Test1" });
			files.Add(new CollectionFileModel() { Name = "Test2" });
			_form.SetImportableCollections(files);
			return Task.CompletedTask;
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
						// Begin import
						_form.SetTab(EnumWelcomeTab.SetNotificationSettings);
					}
					break;
				default:
					_form.SetTab(tab);
					break;
			}
		}
	}
}
