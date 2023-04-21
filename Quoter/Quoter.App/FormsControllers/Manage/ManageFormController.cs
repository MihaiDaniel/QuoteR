using Quoter.App.Forms;
using Quoter.App.Services;
using Quoter.App.Services.Forms;
using Quoter.Framework.Models;
using Quoter.Framework.Services.Messaging;

namespace Quoter.App.FormsControllers.Manage
{
	/// <summary>
	/// Controller for the <see cref="ManageForm"/> that handles global stuff (not things related to inner tabs)
	/// </summary>
	public class ManageFormController : IManageFormController, IMessagingSubscriber
	{
		private readonly IMessagingService _messagingService;
		private readonly IStringResources _stringResources;
		private readonly ISettings _settings;
		private readonly IThemeService _themeService;
		private IManageForm _form;

		public ManageFormController(IMessagingService messagingService, 
									IStringResources stringResources, 
									ISettings settings,
									IThemeService themeService)
		{
			_messagingService = messagingService;
			_stringResources = stringResources;
			_settings = settings;
			_themeService = themeService;
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
			if(isAnnouncementImport)
			{
				_form.SetBackgroundTask(true, _stringResources["ImportingInProgress"]);
			}
			bool isAnnouncementExport = _messagingService.ExistsAnnouncement(Event.ExportInProgress);
			if (isAnnouncementExport)
			{
				_form.SetBackgroundTask(true, _stringResources["ExportingInProgress"]);
			}
			return Task.CompletedTask;
		}

		public void RegisterForm(IManageForm form)
		{
			_form = form;
			// Set the size as soon as the form is intialized.
			_form.SetSize(_settings.ManageWindowSize);
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
				if(formsManagerOptions.Type == typeof(ManageForm))
				{
					ManageFormOptions manageFormOptions = (ManageFormOptions)formsManagerOptions.Parameters[0];
					if(manageFormOptions != null)
					{
						_form.SetSelectedTab(manageFormOptions.Tab);
					}
				}
			}
			if(message == Event.ThemeChanged)
			{
				_form.SetTheme(_themeService.GetCurrentTheme());
			}
			if(message == Event.ImportInProgress)
			{
				_form.SetBackgroundTask(true, _stringResources["ImportingInProgress"]);
			}
			
			if(message == Event.ExportInProgress)
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
