using Quoter.App.Forms;
using Quoter.App.Helpers;
using Quoter.App.Services;
using Quoter.App.Services.BackgroundWorkers;
using Quoter.App.Services.Forms;
using Quoter.App.Views;
using Quoter.Framework.Models;
using Quoter.Framework.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App
{
	public class QuoterApplicationContext : ApplicationContext
	{
		private readonly NotifyIcon _trayIcon;
		private readonly IFormsManager _formsManager;
		private readonly IMemoryCache _memoryCache;
		private readonly IStringResources _stringResources;
		private readonly ISettings _settings;

		public QuoterApplicationContext(IFormsManager formsManager,
										ISettings settings,
										IMemoryCache memoryCache,
										IStringResources stringResources)
		{
			_formsManager = formsManager;
			_settings = settings;
			_memoryCache = memoryCache;
			_stringResources = stringResources;
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

		private async void InitializeApplication()
		{
			string? collectionDirectory = _settings.Get<string>(Const.Setting.CollectionsDirectory);
			if(collectionDirectory == null)
			{
				// If we don't have collectionDirectory we assume the application was started for the first time.
				// So we setup the 
				collectionDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Collections");
				_settings.Set(Const.Setting.CollectionsDirectory, collectionDirectory);
				_settings.Set(Const.Setting.NotificationIntervalSeconds, Const.SettingDefault.NotificationIntervalSeconds);
				_settings.Set(Const.Setting.AutoCloseNotificationSeconds, Const.SettingDefault.AutoCloseNotificationSeconds);
				_settings.Set(Const.Setting.ShowWelcomeNotification, Const.SettingDefault.ShowWelcomeNotification);
				_settings.Set(Const.Setting.KeepNotificationOpenOnMouseOver, Const.SettingDefault.KeepNotificationOpenOnMouseOver);
			}
			else
			{
				_settings.Set(Const.Setting.IsPaused, false);

			}
			
		}

		private void InitializeBackgroundTimers()
		{
			int notificationIntervalSec = _settings.Get<int>(Const.Setting.NotificationIntervalSeconds);
			//int scanFolderIntervalSec = (int)Properties.Settings.Default["ScanFolderInterval"];

			System.Timers.Timer  timerShowNotifications = new(notificationIntervalSec * 1000);
			timerShowNotifications.Elapsed += async (sender, e) => await ElapsedTimerEventShowNotifications();
			timerShowNotifications.Start();

			//System.Timers.Timer  timerScanWorkFolder = new(scanFolderIntervalSec * 1000);
			//timerScanWorkFolder.Elapsed += async (sender, e) => await ElapsedTimerScanWorkFolder();
			//timerScanWorkFolder.Start();
		}

		private async void ShowWelcomeMessage()
		{
			if(_settings.Get<bool>(Const.Setting.ShowWelcomeNotification))
			{
				await Task.Delay(1000);
				int autoHideWelcomeMessageSeconds = 7;
				MessageModel messageModel = new()
				{
					Title = _stringResources["Welcome"],
					Body = _stringResources["WelcomeStartupMessage"],
					CloseAnimation = Framework.Enums.EnumAnimation.FadeOut
				};
				_formsManager.ShowDialog<MessageForm>(autoHideWelcomeMessageSeconds, messageModel);
			}
		}

		private async Task ElapsedTimerEventShowNotifications()
		{

		}

		private async Task ElapsedTimerScanWorkFolder()
		{

		}


		void PauseOrResume(object? sender, EventArgs e)
		{
			bool isPaused = _settings.Get<bool>(Const.Setting.IsPaused);
			isPaused = !isPaused;
			_settings.Set(Const.Setting.IsPaused, isPaused);
			_trayIcon.ContextMenuStrip = GetContextMenuStrip(isPaused);
		}

		void Manage(object? sender, EventArgs e)
		{
			_formsManager.Show<ManageQuotesForm>();
		}

		void Settings(object? sender, EventArgs e)
		{
			_formsManager.Show<SettingsForm>();
		}

		void Exit(object? sender, EventArgs e)
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
					new ToolStripMenuItem(pauseResumeText, pauseResumeImage, new EventHandler(PauseOrResume), "PauseOrResume"),
					new ToolStripMenuItem(_stringResources["Settings"], Resources.Resources.settings_64, new EventHandler(Settings), "Settings"),
					new ToolStripMenuItem(_stringResources["Manage"], Resources.Resources.book_open_64, new EventHandler(Manage), "Settings"),
					new ToolStripMenuItem(_stringResources["Exit"], Resources.Resources.exit_64, new EventHandler(Exit), "Exit")
				}
			};
			return contextMenuStrip;
		}

	}
}
