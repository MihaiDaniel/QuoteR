using Microsoft.EntityFrameworkCore;
using Quoter.App.Forms;
using Quoter.App.Helpers;
using Quoter.App.Services;
using Quoter.App.Services.Forms;
using Quoter.App.Views;
using Quoter.Framework.Data;
using Quoter.Framework.Entities;
using Quoter.Framework.Models;
using Quoter.Framework.Services;
using Quoter.Framework.Services.Messaging;
using System.Globalization;

namespace Quoter.App
{
	public class QuoterApplicationContext : ApplicationContext, IMessageSubscriber
	{
		private readonly NotifyIcon _trayIcon;
		//private readonly QuoterContext _context;
		private readonly IFormsManager _formsManager;
		private readonly IStringResources _stringResources;
		private readonly ISettings _settings;
		private readonly IMessagingService _messagingService;
		private readonly IQuoteService _quoteService;

		private System.Timers.Timer _timerShowNotifications;

		public QuoterApplicationContext(//QuoterContext quoterContext,
										IFormsManager formsManager,
										ISettings settings,
										IStringResources stringResources,
										IMessagingService messagingService,
										IQuoteService quoteService)
		{
			//_context = quoterContext;
			_formsManager = formsManager;
			_settings = settings;
			_stringResources = stringResources;
			_messagingService = messagingService;
			_quoteService = quoteService;
			_trayIcon = new NotifyIcon()
			{
				Icon = Resources.Resources.icon_book_black,
				ContextMenuStrip = GetContextMenuStrip(),
				Visible = true
			};
			InitializeApplication();
			InitializeBackgroundTimers();
			ShowWelcomeMessage();
		}

		private void InitializeApplication()
		{
			bool isFirstStart = _settings.Get<bool>(Const.Setting.IsFirstStart);
			if(isFirstStart)
			{
				_settings.Set(Const.Setting.NotificationIntervalSeconds, Const.SettingDefault.NotificationIntervalSeconds);
				_settings.Set(Const.Setting.AutoCloseNotificationSeconds, Const.SettingDefault.AutoCloseNotificationSeconds);
				_settings.Set(Const.Setting.ShowWelcomeNotification, Const.SettingDefault.ShowWelcomeNotification);
				_settings.Set(Const.Setting.KeepNotificationOpenOnMouseOver, Const.SettingDefault.KeepNotificationOpenOnMouseOver);
				_settings.Set(Const.Setting.ShowCollectionsBasedOnLanguage, Const.SettingDefault.ShowCollectionsBasedOnLanguage);

				CultureInfo ci = CultureInfo.CurrentUICulture;
				switch(ci.Name)
				{
					case "ro-RO":
						_settings.Set(Const.Setting.Language, "ro-RO");
						Thread.CurrentThread.CurrentUICulture = new CultureInfo("ro-RO");
						break;
					default:
						_settings.Set(Const.Setting.Language, "en-US");
						Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
						break;
				}
				_settings.Set(Const.Setting.IsFirstStart, false);
			}
			else
			{
				// Unpause if notifications was paused
				_settings.Set(Const.Setting.IsPaused, false);
			}
			_messagingService.Subscribe(this);
		}

		private void InitializeBackgroundTimers()
		{
			int notificationIntervalSec = _settings.Get<int>(Const.Setting.NotificationIntervalSeconds);

			_timerShowNotifications = new(notificationIntervalSec * 1000);
			_timerShowNotifications.Elapsed += async (sender, e) => await ElapsedTimerEventShowNotifications();
			_timerShowNotifications.Start();

		}

		private async void ShowWelcomeMessage()
		{
			if(_settings.Get<bool>(Const.Setting.ShowWelcomeNotification))
			{
				await Task.Delay(1000);
				int autoHideWelcomeMessageSeconds = 7;
				QuoteModel messageModel = new()
				{
					Title = _stringResources["Welcome"],
					Body = _stringResources["WelcomeStartupMessage"],
					CloseAnimation = Framework.Enums.EnumAnimation.FadeOut
				};
				_formsManager.ShowDialog<QuoteForm>(autoHideWelcomeMessageSeconds, messageModel);
			}
		}

		public void OnMessageEvent(string message, object? argument)
		{
			if(message == Const.Event.LanguageChanged)
			{
				// Reset the context strip with the new language
				bool isPaused = _settings.Get<bool>(Const.Setting.IsPaused);
				_trayIcon.ContextMenuStrip = GetContextMenuStrip(isPaused);
			}
			else if (message == Const.Event.NotificationIntervalChanged)
			{
				_timerShowNotifications.Stop();
				_timerShowNotifications.Interval = _settings.Get<int>(Const.Setting.NotificationIntervalSeconds) * 1000;
				_timerShowNotifications.Start();
			}
		}

		private async Task ElapsedTimerEventShowNotifications()
		{
			try
			{
				bool isPaused = IsTimerOnPause();
				if(isPaused)
				{
					// Lower the interval untill unpaused
					_timerShowNotifications.Interval= 10000; // 10 sec
					return;
				}
				else
				{
					// Reset the interval if it was on pause
					_timerShowNotifications.Interval = _settings.Get<int>(Const.Setting.NotificationIntervalSeconds) * 1000;
				}

				await ShowQuote();
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private async Task ShowQuote()
		{
			// Show a random quote from the database
			QuoteModel? quoteModel = await _quoteService.GetRandomQuote();
			
			if(quoteModel == null)
			{
				return;
			}

			int autoCloseSec = _settings.Get<int>(Const.Setting.AutoCloseNotificationSeconds);
			_formsManager.ShowDialog<QuoteForm>(autoCloseSec, quoteModel);
		}

		private bool IsTimerOnPause()
		{
			return _settings.Get<bool>(Const.Setting.IsPaused);
		}

		void PauseOrResumeEventHandler(object? sender, EventArgs e)
		{
			bool isPaused = _settings.Get<bool>(Const.Setting.IsPaused);
			isPaused = !isPaused;
			_settings.Set(Const.Setting.IsPaused, isPaused);
			_trayIcon.ContextMenuStrip = GetContextMenuStrip(isPaused);
		}

		void OpenManageEventHandler(object? sender, EventArgs e)
		{
			_formsManager.Show<ManageForm>();
		}

		void OpenSettingsEventHandler(object? sender, EventArgs e)
		{
			_formsManager.Show<SettingsForm>();
		}

		void ShowQuoteEventHandler(object? sender, EventArgs e)
		{
			try
			{
				Task.Run( async () => { await ShowQuote(); });
			}
			catch(Exception ex)
			{
				throw;
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
			Bitmap pauseResumeImage = isPaused ? Resources.Resources.play_64 : Resources.Resources.pause_64;
			ContextMenuStrip contextMenuStrip = new ContextMenuStrip()
			{
				Items =
				{
					new ToolStripMenuItem(pauseResumeText, pauseResumeImage, new EventHandler(PauseOrResumeEventHandler), "PauseOrResume"),
					new ToolStripMenuItem(_stringResources["Settings"], Resources.Resources.settings_64, new EventHandler(OpenSettingsEventHandler), "Settings"),
					new ToolStripMenuItem(_stringResources["ShowAQuote"], Resources.Resources.quote_64, new EventHandler(ShowQuoteEventHandler), "ShowAQuote"),
					new ToolStripMenuItem(_stringResources["Manage"], Resources.Resources.book_open_64, new EventHandler(OpenManageEventHandler), "Settings"),
					new ToolStripMenuItem(_stringResources["Exit"], Resources.Resources.exit_64, new EventHandler(ExitEventHandler), "Exit")
				}
			};
			return contextMenuStrip;
		}
	}
}
