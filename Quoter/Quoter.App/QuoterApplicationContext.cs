using Quoter.App.Forms;
using Quoter.App.Helpers;
using Quoter.App.Services;
using Quoter.App.Services.BackgroundWorkers;
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

		public QuoterApplicationContext(IFormsManager formsManager,
										IMemoryCache memoryCache,
										IStringResources stringResources)
		{
			_formsManager = formsManager;
			_memoryCache = memoryCache;
			_stringResources = stringResources;
			_trayIcon = new NotifyIcon()
			{
				Icon = Resources.Resources.icon_book_black,
				ContextMenuStrip = GetContextMenuStrip(),
				Visible = true
			};

			InitializeBacgroundTimers();
		}

		private void InitializeBacgroundTimers()
		{
			int notificationIntervalSec = (int)Properties.Settings.Default["NotificationIntervalSeconds"];
			int scanFolderIntervalSec = (int)Properties.Settings.Default["ScanFolderInterval"];

			System.Timers.Timer  timerShowNotifications = new(notificationIntervalSec * 1000);
			timerShowNotifications.Elapsed += async (sender, e) => await ElapsedTimerEventShowNotifications();
			timerShowNotifications.Start();

			System.Timers.Timer  timerScanWorkFolder = new(scanFolderIntervalSec * 1000);
			timerScanWorkFolder.Elapsed += async (sender, e) => await ElapsedTimerScanWorkFolder();
			timerScanWorkFolder.Start();
		}

		private async Task ElapsedTimerEventShowNotifications()
		{

		}

		private async Task ElapsedTimerScanWorkFolder()
		{

		}


		void PauseOrResume(object? sender, EventArgs e)
		{
			bool isPaused = _memoryCache.GetOrDefault<bool>(Const.IsPaused);
			isPaused = !isPaused;
			_memoryCache.TryAddOrUpdate<bool>(Const.IsPaused, isPaused);

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
