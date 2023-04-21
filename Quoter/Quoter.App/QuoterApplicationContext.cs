using Microsoft.Extensions.Options;
using Microsoft.Win32;
using Quoter.App.Forms;
using Quoter.App.Helpers;
using Quoter.App.Models;
using Quoter.App.Services;
using Quoter.App.Services.Forms;
using Quoter.App.Views;
using Quoter.Framework.Enums;
using Quoter.Framework.Models;
using Quoter.Framework.Services;
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

		private bool _isUserLoggedOff;

		private System.Timers.Timer _timerShowNotifications;

		public QuoterApplicationContext(IFormsManager formsManager,
										ISettings settings,
										IStringResources stringResources,
										IMessagingService messagingService,
										ISoundService soundService,
										ILogger logger)
		{
			_formsManager = formsManager;
			_settings = settings;
			_stringResources = stringResources;
			_messagingService = messagingService;
			_soundService = soundService;
			_logger = logger;
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
			SetContextMenuStripIsWorkInBackground(false);
			SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
		}

		private void InitializeApplication()
		{
			_messagingService.Subscribe(this);
			_soundService.LoadSoundsAsync();

			if (_settings.IsFirstStart)
			{
				_settings.NotificationIntervalSeconds = Const.SettingDefault.NotificationIntervalSeconds;
				_settings.AutoCloseNotificationSeconds = Const.SettingDefault.AutoCloseNotificationSeconds;
				_settings.ShowWelcomeNotification = Const.SettingDefault.ShowWelcomeNotification;
				_settings.KeepNotificationOpenOnMouseOver = Const.SettingDefault.KeepNotificationOpenOnMouseOver;
				_settings.ShowCollectionsBasedOnLanguage = Const.SettingDefault.ShowCollectionsBasedOnLanguage;
				_settings.NotificationType = Const.SettingDefault.NotificationType;
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

				// Show welcome message
				ShowWelcomeMessage();
			}
			if(!_settings.IsSetupFinished)
			{
				_formsManager.ShowAndCloseOthers<WelcomeForm>();
			}
		}

		private void InitializeBackgroundTimers()
		{
			_timerShowNotifications = new(GetNotificationsIntervalMiliseconds());
			_timerShowNotifications.Elapsed += (sender, e) => ElapsedTimerEventShowNotifications();
			_timerShowNotifications.Start();
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

		private void ShowWelcomeMessage()
		{
			try
			{
				Task.Run(async () =>
				{
					if (_settings.ShowWelcomeNotification)
					{
						await Task.Delay(1000);
						int autoHideWelcomeMessageSeconds = 7;
						QuoteFormOptions messageModel = new()
						{
							Title = _stringResources["Welcome"],
							Body = _stringResources["WelcomeStartupMessage"]
						};
						_formsManager.ShowDialog<QuoteForm>(autoHideWelcomeMessageSeconds, messageModel);
					}
					if (_settings.NotificationType == EnumNotificationType.AlwaysOn)
					{
						ShowQuoteNotification();
					}
				}).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
		}

		public void OnMessageEvent(string message, object? argument)
		{
			if (message == Event.LanguageChanged)
			{
				// Reset the context strip with the new language
				SetContextMenuStripTranslationsThreadSafe();
			}
			if (message == Event.NotificationIntervalChanged)
			{
				// Stop the timer and set the new notification interval
				_timerShowNotifications.Stop();
				double miliseconds = _settings.NotificationIntervalSeconds * 1000;
				_timerShowNotifications.Interval = miliseconds;
				_timerShowNotifications.Start();
			}
			if (message == Event.ImportInProgress || message == Event.ExportInProgress)
			{
				SetContextMenuStripIsWorkInBackground(true);
			}
			if (message == Event.ExportSucessfull)
			{
				if (!_messagingService.ExistsAnnouncement(Event.ImportInProgress))
				{
					SetContextMenuStripIsWorkInBackground(false);
				}
				ShowDialog(_stringResources["ExportSuccessfull"], _stringResources["ExportSuccesfullMsg", argument?.ToString()], false);
			}
			if (message == Event.ExportFailed)
			{
				if (!_messagingService.ExistsAnnouncement(Event.ImportInProgress))
				{
					SetContextMenuStripIsWorkInBackground(false);
				}
				ShowDialog(_stringResources["ExportFailed"], _stringResources["ExportFailedMsg", argument?.ToString()], true);
			}
			if (message == Event.ImportSuccesfull)
			{
				if (!_messagingService.ExistsAnnouncement(Event.ExportInProgress))
				{
					SetContextMenuStripIsWorkInBackground(false);
				}
				ShowDialog(_stringResources["ImportSuccessfull"], _stringResources["ImportSuccessfullMsg", argument?.ToString()], false);
			}
			if (message == Event.ImportFailed)
			{
				if (!_messagingService.ExistsAnnouncement(Event.ExportInProgress))
				{
					SetContextMenuStripIsWorkInBackground(false);
				}
				ShowDialog(_stringResources["ImportFailed"], _stringResources["ImportFailedMsg", argument?.ToString()], true);
			}
		}

		private void ShowDialog(string title, string message, bool isError)
		{
			DialogMessageFormOptions dialogModel = new DialogMessageFormOptions()
			{
				Title = title,
				TitleColor = isError ? Const.ColorError : Const.ColorDefault,
				Message = message,
				MessageBoxButtons = EnumDialogButtons.Ok
			};
			_formsManager.ShowDialog<DialogMessageForm>(dialogModel);
		}

		private void ElapsedTimerEventShowNotifications()
		{
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
				_formsManager.ShowDialog<QuoteForm>(_settings.AutoCloseNotificationSeconds);
			}
			else if (_settings.NotificationType == EnumNotificationType.AlwaysOn)
			{
				if (_formsManager.IsOpen<QuoteForm>())
				{
					_messagingService.SendMessage(Event.RequestDisplayNewQuote);
				}
				else
				{
					_formsManager.ShowDialog<QuoteForm>(0);
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

		private void SetContextMenuStripIsWorkInBackground(bool isWorkInBackground)
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

		private double GetNotificationsIntervalMiliseconds()
		{
			return _settings.NotificationIntervalSeconds * 1000;
		}
	}
}
