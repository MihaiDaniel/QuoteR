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

namespace Quoter.App
{
	public class QuoterApplicationContext : ApplicationContext, IMessagingSubscriber
	{
		private readonly NotifyIcon _trayIcon;
		private readonly IFormsManager _formsManager;
		private readonly IStringResources _stringResources;
		private readonly ISettings _settings;
		private readonly IMessagingService _messagingService;
		private readonly ILogger _logger;

		private System.Timers.Timer _timerShowNotifications;

		public QuoterApplicationContext(IFormsManager formsManager,
										ISettings settings,
										IStringResources stringResources,
										IMessagingService messagingService,
										ILogger logger)
		{
			_formsManager = formsManager;
			_settings = settings;
			_stringResources = stringResources;
			_messagingService = messagingService;
			_logger = logger;
			InitializeApplication();
			InitializeBackgroundTimers();

			_trayIcon = new NotifyIcon()
			{
				Icon = Resources.Resources.icon_book_black,
				ContextMenuStrip = GetContextMenuStrip(),
				Text = _stringResources["Quoter"],
				Visible = true
			};

			ShowWelcomeMessage();
		}

		private void InitializeApplication()
		{
			if (_settings.IsFirstStart)
			{
				_settings.NotificationIntervalSeconds = Const.SettingDefault.NotificationIntervalSeconds;
				_settings.AutoCloseNotificationSeconds = Const.SettingDefault.AutoCloseNotificationSeconds;
				_settings.ShowWelcomeNotification = Const.SettingDefault.ShowWelcomeNotification;
				_settings.KeepNotificationOpenOnMouseOver = Const.SettingDefault.KeepNotificationOpenOnMouseOver;
				_settings.ShowCollectionsBasedOnLanguage = Const.SettingDefault.ShowCollectionsBasedOnLanguage;
				_settings.NotificationType = Const.SettingDefault.NotificationType;

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
			_messagingService.Subscribe(this);
		}

		private void InitializeBackgroundTimers()
		{
			int miliseconds = _settings.NotificationIntervalSeconds * 1000;
			_timerShowNotifications = new(miliseconds);
			_timerShowNotifications.Elapsed += async (sender, e) => await ElapsedTimerEventShowNotifications();
			_timerShowNotifications.Start();
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
						await ShowQuoteNotification();
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
				_trayIcon.ContextMenuStrip = GetContextMenuStrip(_settings.IsPaused);
			}
			if (message == Event.NotificationIntervalChanged)
			{
				// Stop the timer and set the new notification interval
				_timerShowNotifications.Stop();
				double miliseconds = _settings.NotificationIntervalSeconds * 1000;
				_timerShowNotifications.Interval = miliseconds;
				_timerShowNotifications.Start();
			}
			if (message == Event.ExportSucessfull)
			{
				DialogModel dialogModel = new DialogModel()
				{
					Title = _stringResources["ExportSuccessfull"],
					Message = _stringResources["ExportSuccesfullMsg", argument?.ToString()],
					MessageBoxButtons = EnumDialogButtons.Ok
				};
				_formsManager.ShowDialog<DialogMessageForm>(dialogModel);
			}
			if (message == Event.ExportFailed)
			{
				DialogModel dialogModel = new DialogModel()
				{
					Title = _stringResources["ExportFailed"],
					TitleColor = Color.Red,
					Message = _stringResources["ExportFailedMsg", argument?.ToString()],
					MessageBoxButtons = EnumDialogButtons.Ok
				};
				_formsManager.ShowDialog<DialogMessageForm>(dialogModel);
			}
			if (message == Event.ImportSuccesfull)
			{
				DialogModel dialogModel = new DialogModel()
				{
					Title = _stringResources["ImportSuccessfull"],
					Message = _stringResources["ImportSuccessfullMsg", argument?.ToString()],
					MessageBoxButtons = EnumDialogButtons.Ok
				};
				_formsManager.ShowDialog<DialogMessageForm>(dialogModel);
			}
			if (message == Event.ImportFailed)
			{
				DialogModel dialogModel = new DialogModel()
				{
					Title = _stringResources["ImportFailed"],
					TitleColor = Color.Red,
					Message = _stringResources["ImportFailedMsg", argument?.ToString()],
					MessageBoxButtons = EnumDialogButtons.Ok
				};
				_formsManager.ShowDialog<DialogMessageForm>(dialogModel);
			}
		}

		private async Task ElapsedTimerEventShowNotifications()
		{
			try
			{
				bool isPaused = _settings.IsPaused;
				if (isPaused)
				{
					// Lower the interval untill unpaused
					_timerShowNotifications.Interval = 10000; // 10 sec
					return;
				}
				else
				{
					// Reset the interval if it was on pause
					double miliseconds = _settings.NotificationIntervalSeconds * 1000;
					_timerShowNotifications.Interval = miliseconds;
				}

				if (_settings.NotificationType == EnumNotificationType.Popup)
				{
					await ShowQuoteNotification();
				}
				else
				{
					_messagingService.SendMessage(Event.NotificationTimerElapsed);
				}
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
		}

		private async Task ShowQuoteNotification()
		{
			if (_settings.NotificationType == EnumNotificationType.Popup)
			{
				_messagingService.SendMessage(Event.OpeningQuoteWindow);

				int autoCloseSec = 0;
				if (_settings.NotificationType == EnumNotificationType.Popup)
				{
					autoCloseSec = _settings.AutoCloseNotificationSeconds;
				}
				_formsManager.ShowDialog<QuoteForm>(autoCloseSec);
			}
			else if (_settings.NotificationType == EnumNotificationType.AlwaysOn)
			{
				if (_formsManager.IsOpen<QuoteForm>())
				{
					_messagingService.SendMessage(Event.ShowQuoteButtonEvent);
				}
				else
				{
					_formsManager.ShowDialog<QuoteForm>(0);
				}
			}
		}

		void PauseOrResumeEventHandler(object? sender, EventArgs e)
		{
			_settings.IsPaused = !_settings.IsPaused;
			_trayIcon.ContextMenuStrip = GetContextMenuStrip(_settings.IsPaused);
		}

		void EventHandlerOpenEditQuotes(object? sender, EventArgs e)
		{
			_formsManager.Show<ManageForm>(new ManageFormOptions() { Tab = EnumTab.EditQuotes });
		}
		void EventHandlerOpenFavourties(object? sender, EventArgs e)
		{
			_formsManager.Show<ManageForm>(new ManageFormOptions() { Tab = EnumTab.FavouriteQuotes });
		}

		void EventHandlerOpenSettings(object? sender, EventArgs e)
		{
			_formsManager.Show<ManageForm>(new ManageFormOptions() { Tab = EnumTab.Settings });
		}

		void ShowQuoteEventHandler(object? sender, EventArgs e)
		{
			try
			{
				Task.Run(async () => { await ShowQuoteNotification(); });
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}

		}

		void ExitEventHandler(object? sender, EventArgs e)
		{
			// Hide tray icon, otherwise it will remain shown until user mouses over it
			_trayIcon.Visible = false;
			Application.Exit();
		}

		private ContextMenuStrip GetContextMenuStrip(bool isPaused = false)
		{
			string pauseResumeText = isPaused ? _stringResources["Resume"] : _stringResources["Pause"];
			Bitmap pauseResumeImage = isPaused ? Resources.Resources.play_32 : Resources.Resources.pause_32;
			ContextMenuStrip contextMenuStrip = new ContextMenuStrip()
			{
				Items =
				{
					new ToolStripLabel(_stringResources["Quoter"]),
					new ToolStripMenuItem(pauseResumeText, pauseResumeImage, new EventHandler(PauseOrResumeEventHandler), "PauseOrResume"),
					new ToolStripMenuItem(_stringResources["ShowAQuote"], Resources.Resources.quote_32, new EventHandler(ShowQuoteEventHandler), "ShowAQuote"),
					new ToolStripSeparator(),
					new ToolStripMenuItem(_stringResources["Edit"], Resources.Resources.edit_32, new EventHandler(EventHandlerOpenEditQuotes), "Edit"),
					new ToolStripMenuItem(_stringResources["Favourites"], Resources.Resources.star_32, new EventHandler(EventHandlerOpenFavourties), "Favourites"),
					new ToolStripMenuItem(_stringResources["Settings"], Resources.Resources.settings_32, new EventHandler(EventHandlerOpenSettings), "Settings"),
					new ToolStripSeparator(),
					new ToolStripMenuItem(_stringResources["Exit"], Resources.Resources.exit_32, new EventHandler(ExitEventHandler), "Exit")
				}
			};
			return contextMenuStrip;
		}

	}
}
