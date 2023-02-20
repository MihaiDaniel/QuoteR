using Quoter.App.Services.BackgroundWorkers;
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
		private bool _isPaused;

		public QuoterApplicationContext()
		{
			Debug.WriteLine($"Start ctor.{nameof(QuoterApplicationContext)} Thread: {Thread.CurrentThread.ManagedThreadId}");
			// Initialize Tray Icon
			_trayIcon = new NotifyIcon()
			{
				Icon = Resources.Resources.icon_book_black,
				ContextMenuStrip = GetContextMenuStrip(),
				Visible = true
			};

			Properties.Settings.Default["IsPaused"] = _isPaused = false;
			Properties.Settings.Default.Save();

			BackgroundService backgroundService = new BackgroundService();
			backgroundService.Start();
		}

		void Settings(object sender, EventArgs e)
		{
			new SettingsForm().Show();
		}

		void Exit(object sender, EventArgs e)
		{
			// Hide tray icon, otherwise it will remain shown until user mouses over it
			_trayIcon.Visible = false;
			Application.Exit();
		}

		void PauseOrResume(object sender, EventArgs e)
		{
			//new SettingsForm().Show();
			_isPaused = !_isPaused;
			Properties.Settings.Default["IsPaused"] = _isPaused;
			Properties.Settings.Default.Save();

			_trayIcon.ContextMenuStrip = GetContextMenuStrip(_isPaused);
		}

		private ContextMenuStrip GetContextMenuStrip(bool isPaused = false)
		{
			string pauseResumeText = isPaused ? "Resume" : "Pause";
			Bitmap pauseResumeImage = isPaused ? Resources.Resources.play_64 : Resources.Resources.pause_64;
			ContextMenuStrip contextMenuStrip = new ContextMenuStrip()
			{
				Items =
				{
					new ToolStripMenuItem(pauseResumeText, pauseResumeImage, new EventHandler(PauseOrResume), "PauseOrResume"),
					new ToolStripMenuItem("Settings", Resources.Resources.settings_64, new EventHandler(Settings), "Settings"),
					new ToolStripMenuItem("Exit", Resources.Resources.exit_64, new EventHandler(Exit), "Exit")
				}
			};
			return contextMenuStrip;
		}

	}
}
