using Quoter.App.Forms;
using Quoter.Framework.Models;
using Quoter.Framework.Services.Messaging;

namespace Quoter.App.FormsControllers.Manage
{
	public class ManageFormController : IManageFormController, IMessagingSubscriber
	{
		private readonly IMessagingService _messagingService;
		private IManageForm _form;

		public ManageFormController(IMessagingService messagingService)
		{
			_messagingService = messagingService;
		}

		public Task EventFormClosing()
		{
			_messagingService.Unsubscribe(this);
			return Task.CompletedTask;
		}

		public Task EventFormLoaded()
		{
			_messagingService.Subscribe(this);
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
		}
	}
}
