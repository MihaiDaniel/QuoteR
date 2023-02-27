using Microsoft.EntityFrameworkCore;
using Quoter.App.Forms;
using Quoter.App.Helpers;
using Quoter.App.Services;
using Quoter.App.Services.BackgroundWorkers;
using Quoter.App.Services.Forms;
using Quoter.App.Views;
using Quoter.Framework.Data;
using Quoter.Framework.Entities;
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
		private readonly QuoterContext _context;
		private readonly IFormsManager _formsManager;
		private readonly IStringResources _stringResources;
		private readonly ISettings _settings;

		private System.Timers.Timer _timerShowNotifications;

		public QuoterApplicationContext(QuoterContext quoterContext,
										IFormsManager formsManager,
										ISettings settings,
										IStringResources stringResources)
		{
			_context = quoterContext;
			_formsManager = formsManager;
			_settings = settings;
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
			bool isFirstStart = _settings.Get<bool>(Const.Setting.IsFirstStart);
			if(isFirstStart)
			{
				_settings.Set(Const.Setting.NotificationIntervalSeconds, Const.SettingDefault.NotificationIntervalSeconds);
				_settings.Set(Const.Setting.AutoCloseNotificationSeconds, Const.SettingDefault.AutoCloseNotificationSeconds);
				_settings.Set(Const.Setting.ShowWelcomeNotification, Const.SettingDefault.ShowWelcomeNotification);
				_settings.Set(Const.Setting.KeepNotificationOpenOnMouseOver, Const.SettingDefault.KeepNotificationOpenOnMouseOver);
			}
			else
			{
				_settings.Set(Const.Setting.IsPaused, false);
				_settings.Set(Const.Setting.IsFirstStart, false);

			}
			
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

				// Show a random quote from the database
				List<long> idQuotes = await _context.Quotes.Select(q => q.QuoteId).ToListAsync();
				Random random = new Random();
				long quoteIdToShow = random.NextInt64(idQuotes.Count);

				Quote quote = await _context.Quotes
											.Include(q => q.Book)
											.Include(q => q.Chapter)
											.Include(q => q.Collection)
											.FirstAsync(q => q.QuoteId == quoteIdToShow);
				string title = "";
				string footer = "";

				if (quote.Book != null && quote.Chapter != null)
				{
					title = quote.Book.Name;
					footer = quote.Chapter.Name;
				}
				else if (quote.Book!= null)
				{
					title = quote.Book.Name;
				}
				else
				{
					title = quote.Collection.Name;
				}
				

				QuoteModel quoteModel = new()
				{
					Title = quote.Book?.Name?? string.Empty,
					Footer = footer,
					Body= quote.Content,
					OpenAnimation = Framework.Enums.EnumAnimation.FadeInFromBottomRight,
					CloseAnimation = Framework.Enums.EnumAnimation.FadeOut
				};
				int autoCloseSec = _settings.Get<int>(Const.Setting.AutoCloseNotificationSeconds);

				_formsManager.ShowDialog<QuoteForm>(autoCloseSec, quoteModel);

			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private bool IsTimerOnPause()
		{
			return _settings.Get<bool>(Const.Setting.IsPaused);
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
