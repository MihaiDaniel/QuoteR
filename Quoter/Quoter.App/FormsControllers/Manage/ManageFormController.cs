using Quoter.App.Forms;
using Quoter.App.Forms.Manage;
using Quoter.App.Services;
using Quoter.App.Services.Forms;
using Quoter.Framework.Models;
using Quoter.Framework.Services.Messaging;
using Quoter.Framework.Services.AppSettings;
using Quoter.Framework.Services.Versioning;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Quoter.App.FormsControllers.Manage
{
    /// <summary>
    /// Controller for the <see cref="ManageForm"/> that handles global stuff (not things related to inner tabs)
    /// </summary>
    public class ManageFormController : IManageFormController, IMessagingSubscriber, INotifyPropertyChanged
	{
		private readonly IMessagingService _messagingService;
		private readonly IStringResources _stringResources;
		private readonly IAppSettings _settings;
		private readonly IThemeService _themeService;
		private readonly IVersionService _versionService;
		private IManageForm _form;

		private string _version;
		public string Version
		{
			get => _version;
			set
			{
				_version = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public ManageFormController(IMessagingService messagingService,
									IStringResources stringResources,
									IAppSettings settings,
									IThemeService themeService,
									IVersionService versionService)
		{
			_messagingService = messagingService;
			_stringResources = stringResources;
			_settings = settings;
			_themeService = themeService;
			_versionService = versionService;
		}

		public Task EventFormClosingAsync()
		{
			_messagingService.Unsubscribe(this);
			return Task.CompletedTask;
		}

		public Task EventFormLoadedAsync()
		{
			_messagingService.Subscribe(this);
			bool isAnnouncementImport = _messagingService.ExistsAnnouncement(Event.ImportInProgress);
			if (isAnnouncementImport)
			{
				_form.SetBackgroundTask(true, _stringResources["ImportingInProgress"]);
			}
			bool isAnnouncementExport = _messagingService.ExistsAnnouncement(Event.ExportInProgress);
			if (isAnnouncementExport)
			{
				_form.SetBackgroundTask(true, _stringResources["ExportingInProgress"]);
			}
			Version = _versionService.GetCurrentAppVersion().ToString();
			return Task.CompletedTask;
		}

		public void RegisterForm(IManageForm form)
		{
			_form = form;
			// Set the size as soon as the form is intialized.
			_form.SetSize(_settings.WindowSize);
			// Also set the theme
			_form.SetTheme(_themeService.GetCurrentTheme());
		}

		public void OnMessageEvent(string message, object? argument)
		{
			// If Manage form is already open and user tries to open one of the tabs
			// from the tray menu, just switch the current tab to the one the user pressed
			if (message == Event.OpeningForm && argument is OpeningFormArgs)
			{
				OpeningFormArgs formsManagerOptions = (OpeningFormArgs)argument;
				if (formsManagerOptions.Type == typeof(ManageForm))
				{
					ManageFormOptions manageFormOptions = (ManageFormOptions)formsManagerOptions.Parameters[0];
					if (manageFormOptions != null)
					{
						_form.SetSelectedTab(manageFormOptions.Tab);
					}
				}
			}
			if (message == Event.ThemeChanged)
			{
				_form.SetTheme(_themeService.GetCurrentTheme());
			}
			if (message == Event.ImportInProgress)
			{
				_form.SetBackgroundTask(true, _stringResources["ImportingInProgress"]);
			}

			if (message == Event.ExportInProgress)
			{
				_form.SetBackgroundTask(true, _stringResources["ExportingInProgress"]);
			}
			if (message == Event.ImportSuccesfull
				|| message == Event.ImportFailed
				|| message == Event.ExportSucessfull
				|| message == Event.ExportFailed)
			{
				_form.SetBackgroundTask(false, default);
			}
		}
	}
}
