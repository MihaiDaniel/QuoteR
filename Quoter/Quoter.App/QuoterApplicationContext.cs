﻿using Microsoft.Win32;
using Quoter.App.Forms;
using Quoter.App.Helpers;
using Quoter.App.Helpers.Extensions;
using Quoter.App.Models;
using Quoter.App.Services;
using Quoter.App.Services.Forms;
using Quoter.Framework.Enums;
using Quoter.Framework.Models;
using Quoter.Framework.Models.ImportExport;
using Quoter.Framework.Services;
using Quoter.Framework.Services.Messaging;
using Quoter.Framework.Services.AppSettings;
using Quoter.Shared.Models;
using System.Diagnostics;

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
		private readonly IAppSettings _settings;
		private readonly IMessagingService _messagingService;
		private readonly ISoundService _soundService;
		private readonly ILogger _logger;
		private readonly IQuoterApplicationService _quoterApplicationService;

		private bool _isUserLoggedOff;

		private System.Windows.Forms.Timer _timerShowNotifications;
		private System.Windows.Forms.Timer _timerStartup;

		public QuoterApplicationContext(IFormsManager formsManager,
										IAppSettings settings,
										IStringResources stringResources,
										IMessagingService messagingService,
										ISoundService soundService,
										ILogger logger,
										IQuoterApplicationService quoterApplicationService)
		{
			_formsManager = formsManager;
			_settings = settings;
			_stringResources = stringResources;
			_messagingService = messagingService;
			_soundService = soundService;
			_logger = logger;
			_quoterApplicationService = quoterApplicationService;

			_isUserLoggedOff = false;
			_messagingService.Subscribe(this);
			_soundService.LoadSoundsAsync();

			InitializeApplicationSettings();
			InitializeBackgroundTimers();

			_trayIcon = new NotifyIcon()
			{
				Icon = ConvertByteArrayToIcon(Resources.Resources.minute_verse),
				ContextMenuStrip = GetContextMenuStrip(),
				Text = _stringResources["Quoter"],
				Visible = true
			};
			SetTrayBusyMessage(false);
			SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
		}

		private void InitializeApplicationSettings()
		{
			if (_settings.IsFirstStart)
			{
				_settings.InstallId = Guid.NewGuid().ToString();
				_settings.Language = LanguageHelper.SetCurrentUICultureForCurrentThread();
				_settings.IsFirstStart = false;
			}
			else
			{
				// Unpause if notifications was paused
				_settings.IsPaused = false;

				// Set language
				LanguageHelper.SetCurrentThreadCulture(_settings.Language);
			}
		}

		private void InitializeBackgroundTimers()
		{
			// Main timer for showing notifications (quotes)
			_timerShowNotifications = new();
			_timerShowNotifications.Interval = GetNotificationsIntervalMiliseconds();
			_timerShowNotifications.Tick += (sender, e) => ElapsedTimerEventShowNotifications();
			_timerShowNotifications.Start();

			// Timer to for startup intitializations. Should run only one time at startup.
			// Used to delay intializations without using Task.Run inside constructor logic
			_timerStartup = new();
			_timerStartup.Interval = 1000;
			_timerStartup.Tick += (sender, e) => ElapsedTimerEventStartup();
			_timerStartup.Start();
		}

		/// <summary>
		/// A timer event to delay the display of windows and update of the application
		/// </summary>
		private void ElapsedTimerEventStartup()
		{
			_logger.Debug("ElapsedTimerEventStartup called");
			try
			{
				// Startup initialization should run with a delay only once
				_timerStartup.Stop();

				if (!_settings.IsSetupFinished)
				{
					_formsManager.ShowAndCloseOthers<WelcomeForm>();
				}
				else
				{
					if (_quoterApplicationService.IsAnyUpdateApplied())
					{
						_quoterApplicationService.ShowUpdateAppliedNotification();
					}
					// Only start diplaying welcome message or quote window after completing the setup
					// Display the quotes form if we have it set to AlwaysOn
					else if (_settings.NotificationType == EnumNotificationType.AlwaysOn)
					{
						_quoterApplicationService.ShowRandomQuoteInNotificationWindow();
					}
					else if (_settings.ShowWelcomeNotification)
					{
						_quoterApplicationService.ShowWelcomeNotificationWindow();
					}
				}

				// Enqueue background jobs for the application
				_quoterApplicationService.EnqueueBackgroundJobAppRegistration();

				// Don't auto-update the application if the setup was not finished.
				// This way we verify on next startup if update is available as normally 
				// user should have already downloaded the latest version to install.
				if (_settings.IsSetupFinished)
				{
					_quoterApplicationService.EnqueueBackgroundJobDisplayMessageIfAppWasUpdated();
					_quoterApplicationService.EnqueueBackgroundJobAppUpdate();
				}
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
			finally
			{
				_timerStartup.Dispose();
			}
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
					ActionResult exportResult = argument as ActionResult;
					ShowDialogExportSuccesfull(exportResult);
					HideTrayBusyMsgIfAnnouncementNotExists(Event.ImportInProgress);
					break;
				case Event.ExportFailed:
					HideTrayBusyMsgIfAnnouncementNotExists(Event.ImportInProgress);
					_formsManager.ShowDialogError(_stringResources["ExportFailed"], _stringResources["ExportFailedMsg", argument?.ToString()]);
					break;
				case Event.ImportSuccesfull:
					ImportResult? result = argument as ImportResult;
					if (result?.NotifyUser == true)
					{
						_formsManager.ShowDialogOk(_stringResources["ImportSuccessfull"], _stringResources["ImportSuccessfullMsg", result.ImportedFilesMessage]);
					}
					HideTrayBusyMsgIfAnnouncementNotExists(Event.ExportInProgress);
					break;
				case Event.ImportFailed:
					HideTrayBusyMsgIfAnnouncementNotExists(Event.ExportInProgress);
					_formsManager.ShowDialogError(_stringResources["ImportFailed"], _stringResources["ImportFailedMsg", argument?.ToString()]);
					break;
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
				_quoterApplicationService.ShowRandomQuoteInNotificationWindow();
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
		}

		private void PauseOrResumeEventHandler(object? sender, EventArgs e)
		{
			_settings.IsPaused = !_settings.IsPaused;
			SetContextMenuStripIsPaused();
		}

		private void EventHandlerShowQuoteOnUserRequest(object? sender, EventArgs e)
		{
			_quoterApplicationService.ShowRandomQuoteInNotificationWindow();
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

		private void EventHandlerExitApp(object? sender, EventArgs e)
		{
			_trayIcon.Visible = false; // Hide tray icon, otherwise it will remain shown until user mouses over it
			Application.Exit();
		}

		private void EventHandlerShowWelcomeForm(object? sender, EventArgs e)
		{
			_formsManager.ShowAndCloseOthers<WelcomeForm>();
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
					new ToolStripMenuItem(_stringResources["ShowAQuote"], Resources.Resources.quote_32, new EventHandler(EventHandlerShowQuoteOnUserRequest), "ShowAQuote"),
					new ToolStripSeparator(),
					new ToolStripMenuItem(_stringResources["Edit"], Resources.Resources.edit_32, new EventHandler(EventHandlerOpenEditQuotes), "Edit"),
					new ToolStripMenuItem(_stringResources["Favourites"], Resources.Resources.star_32, new EventHandler(EventHandlerOpenFavourties), "Favourites"),
					new ToolStripMenuItem(_stringResources["Settings"], Resources.Resources.settings_32, new EventHandler(EventHandlerOpenSettings), "Settings"),
					new ToolStripSeparator(),
					new ToolStripMenuItem(_stringResources["Exit"], Resources.Resources.exit_32, new EventHandler(EventHandlerExitApp), "Exit"),
#if DEBUG
					new ToolStripMenuItem("Welcome", null, new EventHandler(EventHandlerShowWelcomeForm), "Welcome"),
					new ToolStripMenuItem("Debug", null, new EventHandler((obj, e) => _formsManager.ShowAndCloseOthers<DebugForm>()), "Debug"),
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
			return _settings.NotificationIntervalSeconds * 1000;
		}

		private void ShowDialogExportSuccesfull(ActionResult exportResult)
		{
			string fileName = exportResult.GetValue<string>();
			string directory = Path.GetDirectoryName(fileName);
			IDialogResult dialogResult = _formsManager.ShowDialog<DialogExportFinishedForm>(new DialogOptions()
			{
				Title = _stringResources["ExportSuccessfull"],
				Message = _stringResources["ExportSuccesfullMsg", fileName]
			});
			if (dialogResult.DialogResult == DialogResult.OK)
			{
				try
				{
					Process.Start("explorer.exe", directory);
				}
				catch (Exception ex)
				{
					_logger.Error(ex);
				}
			}
		}

		private Icon ConvertByteArrayToIcon(byte[] iconBytes)
		{
			using (var ms = new MemoryStream(iconBytes))
			{
				return new Icon(ms);
			}
		}
	}
}
