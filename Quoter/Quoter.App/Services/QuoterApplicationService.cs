using Quoter.App.Forms;
using Quoter.App.Helpers;
using Quoter.App.Models;
using Quoter.App.Services.BackgroundJobs;
using Quoter.App.Services.Forms;
using Quoter.App.Views;
using Quoter.Framework.Enums;
using Quoter.Framework.Models;
using Quoter.Framework.Services;
using Quoter.Framework.Services.Messaging;
using Quoter.Framework.Services.AppSettings;
using Quoter.Framework.Services.Versioning;
using Quoter.Shared.Models;
using Quoter.Framework.Services.Registration;

namespace Quoter.App.Services
{
	/// <summary>
	/// Service handling different features for <see cref="QuoterApplicationContext"/>
	/// </summary>
	public class QuoterApplicationService : IQuoterApplicationService
	{
		private const string JobNameNotifyIfAppWasUpdated = "notify_if_app_was_updated";
		private const string JobNameRegisterApp = "register_app";
		private const string JobNameUpdateApp = "update_app";

		private readonly ILogger _logger;
		private readonly IAppSettings _settings;
		private readonly IBackgroundJobsFormsService _backgroundJobsService;
		private readonly IRegistrationService _registrationService;
		private readonly IStringResources _stringResources;
		private readonly IUpdateService _updateService;
		private readonly IFormsManager _formsManager;
		private readonly IMessagingService _messagingService;

		public QuoterApplicationService(IFormsManager formsManager,
										IAppSettings settings,
										IStringResources stringResources,
										ILogger logger,
										IRegistrationService registrationService,
										IUpdateService updateService,
										IBackgroundJobsFormsService backgroundJobsService,
										IMessagingService messagingService)
		{
			_formsManager = formsManager;
			_settings = settings;
			_stringResources = stringResources;
			_logger = logger;
			_registrationService = registrationService;
			_updateService = updateService;
			_backgroundJobsService = backgroundJobsService;
			_messagingService = messagingService;
		}

		public void EnqueueBackgroundJobAppRegistration()
		{
			_backgroundJobsService.Enqueue(async () =>
			{
				if (string.IsNullOrEmpty(_settings.RegistrationId))
				{
					await _registrationService.RegisterAsync();
				}
			}, JobNameRegisterApp);
		}

		// TODO REMOVE
		public void EnqueueBackgroundJobDisplayMessageIfAppWasUpdated()
		{
			_backgroundJobsService.Enqueue(async () =>
			{
				ActionResult result = await _updateService.VerifyIfUpdateAppliedAsync();
				if (result.IsSuccess)
				{
					
				}
			}, JobNameNotifyIfAppWasUpdated);
		}

		public void EnqueueBackgroundJobAppUpdate()
		{
			_backgroundJobsService.Enqueue(async () =>
			{
				_logger.Debug($"Beginning auto update job. AutoUpdate setting value: {_settings.AutoUpdate}");
				switch (_settings.AutoUpdate)
				{
					case EnumAutoUpdate.Auto:
						await _updateService.TryUpdateAsync(isSilent: true);
						break;
					case EnumAutoUpdate.AskFirst:
						bool isUpdateAvailable = await _updateService.VerifyIfNewVersionAvailable();
						if (isUpdateAvailable)
						{
							IDialogResult result = AskUserIfHeWantsToUpdateToNewVersion();
							if (result.DialogResult == DialogResult.OK)
							{
								await _updateService.TryUpdateAsync(isSilent: false);
							}
						}
						break;
					default:
						// Do not do any updates
						break;
				}
			}, JobNameUpdateApp);
		}

		public bool IsAnyUpdateApplied()
		{
			ActionResult result = _updateService.MarkUpdateAsAppliedIfAppUpdated();
			return result.IsSuccess;
		}

		public bool ShowUpdateAppliedNotification()
		{
			string version = _updateService.GetLastAppliedVersion();
			
			int autohideSeconds = 10;
			QuoteFormOptions messageModel = new()
			{
				Title = _stringResources["UpdateApplied"],
				Body = _stringResources["UpdateAppliedMessage", version]
			};
			_formsManager.Show<QuoteForm>(autohideSeconds, messageModel);
			return true;
		}

		public void ShowRandomQuoteInNotificationWindow()
		{
			try
			{
				if (_settings.NotificationType == EnumNotificationType.Popup)
				{
					_messagingService.SendMessage(Event.OpeningQuoteWindow);
					_formsManager.Show<QuoteForm>(_settings.AutoCloseNotificationSeconds);
				}
				else if (_settings.NotificationType == EnumNotificationType.AlwaysOn)
				{
					if (_formsManager.IsOpen<QuoteForm>())
					{
						_messagingService.SendMessage(Event.RequestDisplayNewQuote);
					}
					else
					{
						_formsManager.Show<QuoteForm>(0);
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Error occured while trying to show a quote in the notification window");
			}
		}

		public void ShowWelcomeNotificationWindow()
		{
			int autoHideWelcomeMessageSeconds = 7;
			QuoteFormOptions messageModel = new()
			{
				Title = _stringResources["Welcome"],
				Body = _stringResources["WelcomeStartupMessage"]
			};
			_formsManager.Show<QuoteForm>(autoHideWelcomeMessageSeconds, messageModel);
		}

		private IDialogResult AskUserIfHeWantsToUpdateToNewVersion()
		{
			_logger.Debug("Asking the user if he wants to update to a new version");

			DialogOptions options = new()
			{
				MessageBoxButtons = EnumDialogButtons.YesLater,
				Title = _stringResources["Quoter"],
				Message = _stringResources["NewUpdateAvailableMsg"],
				DialogSound = Enums.DialogOptionsSound.Default,
				DialogTheme = Enums.DialogOptionsTheme.Default,
				OpenAnimation = EnumAnimation.FadeInFromBottomRight,
			};
			return _formsManager.ShowDialog<DialogMessageForm>(options);
		}
	}
}
