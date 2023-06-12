using Microsoft.Extensions.Options;
using Microsoft.Win32;
using Quoter.App.Forms;
using Quoter.App.Helpers;
using Quoter.App.Models;
using Quoter.App.Services;
using Quoter.App.Services.BackgroundJobs;
using Quoter.App.Services.Forms;
using Quoter.App.Views;
using Quoter.Framework.Enums;
using Quoter.Framework.Models;
using Quoter.Framework.Services;
using Quoter.Framework.Services.Api;
using Quoter.Framework.Services.Messaging;
using System.Globalization;
using System.Media;

namespace Quoter.App
{
	/// <summary>
	/// Main application running context
	/// </summary>
	public class QuoterApplicationContext : ApplicationContext, IMessagingSubscriber
	{
		private readonly NotifyIcon _trayIcon;
		private readonly IFormsManager _formsManager;
		private readonly IStringResources _stringResources;
		private readonly ISettings _settings;
		private readonly IMessagingService _messagingService;
		private readonly ISoundService _soundService;
		private readonly ILogger _logger;
		private readonly IRegistrationService _registrationService;
		private readonly IUpdateService _updateService;
		private readonly IBackgroundJobsFormsService _backgroundJobsService;

		private bool _isUserLoggedOff;

		private System.Windows.Forms.Timer _timerShowNotifications;
		private System.Windows.Forms.Timer _timerStartup;

		public QuoterApplicationContext(IFormsManager formsManager,
										ISettings settings,
										IStringResources stringResources,
										IMessagingService messagingService,
										ISoundService soundService,
										ILogger logger,
										IRegistrationService registrationService,
										IUpdateService updateService,
										IBackgroundJobsFormsService backgroundJobsService)
		{
			_formsManager = formsManager;
			_settings = settings;
			_stringResources = stringResources;
			_messagingService = messagingService;
			_soundService = soundService;
			_logger = logger;
			_registrationService = registrationService;
			_updateService = updateService;
			_backgroundJobsService = backgroundJobsService;

			_isUserLoggedOff = false;
			InitializeApplication();
			InitializeBackgroundTimers();
			
			_trayIcon = new NotifyIcon()
			{
				Icon = Resources.Resources.icon_book_black,
				ContextMenuStrip = GetContextMenuStrip(),
				Text = _stringResources["Quoter"],
				Visible = true
			};
			SetTrayBusyMessage(false);
			SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
		}

		private void InitializeApplication()
		{
			_messagingService.Subscribe(this);
			_soundService.LoadSoundsAsync();

			if(string.IsNullOrEmpty(_settings.InstallId)) // TODO: Remove
				_settings.InstallId = Guid.NewGuid().ToString();

			if (_settings.IsFirstStart)
			{
				_settings.InstallId = Guid.NewGuid().ToString();

				_settings.NotificationIntervalSeconds = Constants.SettingDefault.NotificationIntervalSeconds;
				_settings.AutoCloseNotificationSeconds = Constants.SettingDefault.AutoCloseNotificationSeconds;
				_settings.ShowWelcomeNotification = Constants.SettingDefault.ShowWelcomeNotification;
				_settings.KeepNotificationOpenOnMouseOver = Constants.SettingDefault.KeepNotificationOpenOnMouseOver;
				_settings.ShowCollectionsBasedOnLanguage = Constants.SettingDefault.ShowCollectionsBasedOnLanguage;
				_settings.NotificationType = Constants.SettingDefault.NotificationType;
				_settings.NotificationSound = EnumSound.Click;

				CultureInfo ci = CultureInfo.CurrentUICulture;
				switch (ci.Name)
				{
					case "ro-RO":
						_settings.Language = "ro-RO";
						Thread.CurrentThread.CurrentUICulture = new CultureInfo("ro-RO");
						break;
					case "fr-FR":
						_settings.Language = "fr-FR";
						Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
						break;
					default:
						_settings.Language = "en-US";
						Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
						break;
				}
				_settings.IsFirstStart = false;
			}
			else
			{
				// Unpause if notifications was paused
				_settings.IsPaused = false;

				// Set language
				Thread.CurrentThread.CurrentUICulture = new CultureInfo(_settings.Language);
				Thread.CurrentThread.CurrentCulture = new CultureInfo(_settings.Language);
			}
			if(!_settings.IsSetupFinished)
			{
				_formsManager.ShowAndCloseOthers<WelcomeForm>();
			}
		}

		private void InitializeBackgroundTimers()
		{
			// Main timer for showing notifications (quotes)
			_timerShowNotifications = new();
			_timerShowNotifications.Interval = GetNotificationsIntervalMiliseconds();
			_timerShowNotifications.Tick += (sender, e) => ElapsedTimerEventShowNotifications();
			_timerShowNotifications.Start();

			// Timer to show a message at startup or open the quote form if is always on
			_timerStartup = new();
			_timerStartup.Interval = 5000;
			_timerStartup.Tick += (sender, e) => ElapsedTimerEventStartup();
			_timerStartup.Start();
		}

		/// <summary>
		/// Event used to track when the user logs off or locks the scrren, to know
		/// whether to suspend showing notifications when this happens
		/// </summary>
		private void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
		{
			if (e.Reason == SessionSwitchReason.SessionLock
				|| e.Reason == SessionSwitchReason.SessionLogoff)
			{
				_isUserLoggedOff = true;
			}
			else
			{
				_isUserLoggedOff = false;
			}
		}

		void IMessagingSubscriber.OnMessageEvent(string message, object? argument)
		{
			switch (message)
			{
				case Event.LanguageChanged:     // Reset the context strip with the new language
					SetContextMenuStripTranslationsThreadSafe();
					break;
				case Event.NotificationIntervalChanged: // Stop the timer and set the new notification interval
					_timerShowNotifications.Stop();
					int miliseconds = _settings.NotificationIntervalSeconds * 1000;
					_timerShowNotifications.Interval = miliseconds;
					_timerShowNotifications.Start();
					break;
				case Event.ImportInProgress:
				case Event.ExportInProgress:
					SetTrayBusyMessage(true);
					break;
				case Event.ExportSucessfull:
					HideTrayBusyMsgIfAnnouncementNotExists(Event.ImportInProgress);
					ShowDialog(_stringResources["ExportSuccessfull"], _stringResources["ExportSuccesfullMsg", argument?.ToString()], false);
					break;
				case Event.ExportFailed:
					HideTrayBusyMsgIfAnnouncementNotExists(Event.ImportInProgress);
					ShowDialog(_stringResources["ExportFailed"], _stringResources["ExportFailedMsg", argument?.ToString()], true);
					break;
				case Event.ImportSuccesfull:
					HideTrayBusyMsgIfAnnouncementNotExists(Event.ExportInProgress);
					ShowDialog(_stringResources["ImportSuccessfull"], _stringResources["ImportSuccessfullMsg", argument?.ToString()], false);
					break;
				case Event.ImportFailed:
					HideTrayBusyMsgIfAnnouncementNotExists(Event.ExportInProgress);
					ShowDialog(_stringResources["ImportFailed"], _stringResources["ImportFailedMsg", argument?.ToString()], true);
					break;
			}
		}

		private async void ElapsedTimerEventStartup()
		{
			_logger.Debug("");
			try
			{
				// Display the quotes form or the welcome message if option is set
				if (_settings.NotificationType == EnumNotificationType.AlwaysOn) 
				{
					ShowQuoteNotification();
				}
				else if (_settings.ShowWelcomeNotification)
				{
					int autoHideWelcomeMessageSeconds = 7;
					QuoteFormOptions messageModel = new()
					{
						Title = _stringResources["Welcome"],
						Body = _stringResources["WelcomeStartupMessage"]
					};
					_formsManager.Show<QuoteForm>(autoHideWelcomeMessageSeconds, messageModel);
				}
				_timerStartup.Enabled = false;

				// Enqueue background job for registering the application
				_backgroundJobsService.Enqueue(async () =>
				{
					if (_settings.RegistrationId == Guid.Empty)
					{
						await _registrationService.GetRegistrationId();
					}
				}, "RegisterApp");

				_backgroundJobsService.Enqueue(async () =>
				{
					await _updateService.VerifyIfUpdateApplied();
				}, "VerifyUpdate");

				// Enqueue background job for updating the application if necessary
				_backgroundJobsService.Enqueue(async () =>
				{
					_logger.Debug("Beginning auto update job");
					if(_settings.AutoUpdate == EnumAutoUpdate.Auto)
					{
						_logger.Debug("Auto updating");
						await _updateService.TryUpdate();
					}
					else if(_settings.AutoUpdate == EnumAutoUpdate.AskFirst)
					{
						_logger.Debug("Asking user for update");
						bool isUpdateAvailable = await _updateService.VerifyIfNewVersionAvailable();
						if (isUpdateAvailable)
						{
							DialogMessageFormOptions options = new()
							{
								MessageBoxButtons = EnumDialogButtons.YesNo,
								Title = "Quoter",
								Message = "A new update is available. Do you want to download and install the update now?"
							};
							IDialogReturnable result = _formsManager.ShowDialog<DialogMessageForm>(options);
							if(result.DialogResult == DialogResult.OK)
							{
								await _updateService.TryUpdate();
							}
						}
					}
					else
					{
						// Do not do any updates
					}
				}, "TryUpdate");

				// Start the background jobs service
				_backgroundJobsService.Start();
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
		}

		private void ElapsedTimerEventShowNotifications()
		{
			_logger.Debug("");
			try
			{
				if (_isUserLoggedOff)
				{
					return;
				}
				if (_settings.IsPaused)
				{
					// Lower the interval untill unpaused
					_timerShowNotifications.Interval = 10000; // 10 sec
					return;
				}
				else
				{
					// Reset the interval if it was on pause
					_timerShowNotifications.Interval = GetNotificationsIntervalMiliseconds();
				}
				ShowQuoteNotification();
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
		}

		private void ShowQuoteNotification()
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

		private void PauseOrResumeEventHandler(object? sender, EventArgs e)
		{
			_settings.IsPaused = !_settings.IsPaused;
			SetContextMenuStripIsPaused();
		}

		private void EventHandlerOpenEditQuotes(object? sender, EventArgs e)
		{
			_formsManager.ShowAndCloseOthers<ManageForm>(new ManageFormOptions() { Tab = EnumTab.EditQuotes });
		}
		private void EventHandlerOpenFavourties(object? sender, EventArgs e)
		{
			_formsManager.ShowAndCloseOthers<ManageForm>(new ManageFormOptions() { Tab = EnumTab.FavouriteQuotes });
		}

		private void EventHandlerOpenSettings(object? sender, EventArgs e)
		{
			_formsManager.ShowAndCloseOthers<ManageForm>(new ManageFormOptions() { Tab = EnumTab.Settings });
		}

		private void ShowQuoteEventHandler(object? sender, EventArgs e)
		{
			try
			{
				ShowQuoteNotification();
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
		}

		private void ExitEventHandler(object? sender, EventArgs e)
		{
			// Hide tray icon, otherwise it will remain shown until user mouses over it
			_trayIcon.Visible = false;
			Application.Exit();
		}

		private void WelcomeEventHandler(object? sender, EventArgs e)
		{
			_formsManager.ShowAndCloseOthers<WelcomeForm>();
		}

		private void ReaderEventHandler(object? sender, EventArgs e)
		{
			_formsManager.ShowAndCloseOthers<ReaderForm>();
		}

		private void ShowDialog(string title, string message, bool isError)
		{
			DialogMessageFormOptions dialogModel = new DialogMessageFormOptions()
			{
				Title = title,
				TitleColor = isError ? Constants.ColorError : Constants.ColorDefault,
				Message = message,
				MessageBoxButtons = EnumDialogButtons.Ok
			};
			_formsManager.ShowDialog<DialogMessageForm>(dialogModel);
		}

		private ContextMenuStrip GetContextMenuStrip()
		{
			string pauseResumeText = _settings.IsPaused ? _stringResources["Resume"] : _stringResources["Pause"];
			Bitmap pauseResumeImage = _settings.IsPaused ? Resources.Resources.play_32 : Resources.Resources.pause_32;
			ContextMenuStrip contextMenuStrip = new ContextMenuStrip()
			{

				Items =
				{
					new ToolStripLabel(_stringResources["Quoter"]),
					new ToolStripLabel(_stringResources["WorkInBackground"], Resources.Resources.loading_transparent_128)
					{
						TextImageRelation = TextImageRelation.TextBeforeImage,
						ImageAlign = ContentAlignment.MiddleRight,
						//Available = false
					},

					new ToolStripSeparator(),
					new ToolStripMenuItem(pauseResumeText, pauseResumeImage, new EventHandler(PauseOrResumeEventHandler), "PauseOrResume"),
					new ToolStripMenuItem(_stringResources["ShowAQuote"], Resources.Resources.quote_32, new EventHandler(ShowQuoteEventHandler), "ShowAQuote"),
					new ToolStripSeparator(),
					new ToolStripMenuItem(_stringResources["Edit"], Resources.Resources.edit_32, new EventHandler(EventHandlerOpenEditQuotes), "Edit"),
					new ToolStripMenuItem(_stringResources["Favourites"], Resources.Resources.star_32, new EventHandler(EventHandlerOpenFavourties), "Favourites"),
					new ToolStripMenuItem(_stringResources["Settings"], Resources.Resources.settings_32, new EventHandler(EventHandlerOpenSettings), "Settings"),
					new ToolStripSeparator(),
					new ToolStripMenuItem(_stringResources["Exit"], Resources.Resources.exit_32, new EventHandler(ExitEventHandler), "Exit"),
#if DEBUG
					new ToolStripMenuItem("Welcome", null, new EventHandler(WelcomeEventHandler), "Welcome"),
					new ToolStripMenuItem("Reader", null, new EventHandler(ReaderEventHandler), "Reader")
#endif
				}
			};
			return contextMenuStrip;
		}

		private void SetContextMenuStripIsPaused()
		{
			string pauseResumeText = _settings.IsPaused ? _stringResources["Resume"] : _stringResources["Pause"];
			Bitmap pauseResumeImage = _settings.IsPaused ? Resources.Resources.play_32 : Resources.Resources.pause_32;

			_trayIcon.ContextMenuStrip.Items[3].Text = pauseResumeText;
			_trayIcon.ContextMenuStrip.Items[3].Image = pauseResumeImage;
		}

		private void HideTrayBusyMsgIfAnnouncementNotExists(string eventName)
		{
			if (!_messagingService.ExistsAnnouncement(eventName))
			{
				SetTrayBusyMessage(false);
			}
		}

		private void SetTrayBusyMessage(bool isWorkInBackground)
		{
			if (_trayIcon.ContextMenuStrip.InvokeRequired)
			{
				_trayIcon.ContextMenuStrip.BeginInvoke(() =>
				{
					_trayIcon.ContextMenuStrip.Items[1].Available = isWorkInBackground;
				});
			}
			else
			{
				_trayIcon.ContextMenuStrip.Items[1].Available = isWorkInBackground;
			}
		}

		private void SetContextMenuStripTranslationsThreadSafe()
		{
			if (_trayIcon.ContextMenuStrip.InvokeRequired)
			{
				_trayIcon.ContextMenuStrip.BeginInvoke(() =>
				{
					SetContextMenuStripTranslations();
				});
			}
			else
			{
				SetContextMenuStripTranslations();
			}
		}

		private void SetContextMenuStripTranslations()
		{
			_trayIcon.ContextMenuStrip.Items[0].Text = _stringResources["Quoter"];
			_trayIcon.ContextMenuStrip.Items[1].Text = _stringResources["WorkInBackground"];
			// separator
			SetContextMenuStripIsPaused();
			_trayIcon.ContextMenuStrip.Items[4].Text = _stringResources["ShowAQuote"];
			// separator
			_trayIcon.ContextMenuStrip.Items[6].Text = _stringResources["Edit"];
			_trayIcon.ContextMenuStrip.Items[7].Text = _stringResources["Favourites"];
			_trayIcon.ContextMenuStrip.Items[8].Text = _stringResources["Settings"];

			_trayIcon.ContextMenuStrip.Items[10].Text = _stringResources["Exit"];
		}

		private int GetNotificationsIntervalMiliseconds()
		{
			return _settings.NotificationIntervalSeconds * 300;
		}
	}
}
