using Quoter.App.Forms;
using Quoter.App.Services;
using Quoter.Framework.Models;
using Quoter.Framework.Services.Messaging;

namespace Quoter.App.FormsControllers.Manage
{
	public class ManageFormController : IManageFormController, IMessagingSubscriber
	{
		private readonly IMessagingService _messagingService;
		private readonly IStringResources _stringResources;
		private IManageForm _form;

		public ManageFormController(IMessagingService messagingService, IStringResources stringResources)
		{
			_messagingService = messagingService;
			_stringResources = stringResources;
		}

		public Task EventFormClosingAsync()
		{
			_messagingService.Unsubscribe(this);
			return Task.CompletedTask;
		}

		public Task EventFormLoadedAsync()
		{
			_messagingService.Subscribe(this);
			Announcement<object>? announcementImport = _messagingService.FindAnnouncement<object>(Event.ImportInProgress);
			if(announcementImport != null)
			{
				_form.SetBackgroundTask(true, _stringResources["ImportingInProgress"]);
			}
			Announcement<object>? announcementExport = _messagingService.FindAnnouncement<object>(Event.ExportInProgress);
			if (announcementExport != null)
			{
				_form.SetBackgroundTask(true, _stringResources["ExportingInProgress"]);
			}
			return Task.CompletedTask;
		}

		public void RegisterForm(IManageForm form)
		{
			_form = form;
		}

		public void OnMessageEvent(string message, object? argument)
		{
			if (message == Event.OpeningForm && argument is FormsManagerOptions)
			{
				FormsManagerOptions formsManagerOptions = (FormsManagerOptions)argument;
				if(formsManagerOptions.Type == typeof(ManageForm))
				{
					ManageFormOptions manageFormOptions = (ManageFormOptions)formsManagerOptions.Parameters[0];
					if(manageFormOptions != null)
					{
						_form.SetSelectedTab(manageFormOptions.Tab);
					}
				}
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
