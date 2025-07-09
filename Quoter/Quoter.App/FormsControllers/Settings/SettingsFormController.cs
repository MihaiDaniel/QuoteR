using IWshRuntimeLibrary;
using Quoter.App.Forms;
using Quoter.App.Forms.Manage;
using Quoter.App.Helpers;
using Quoter.App.Models;
using Quoter.App.Services;
using Quoter.App.Services.Forms;
using Quoter.App.Views;
using Quoter.Framework.Enums;
using Quoter.Framework.Services;
using Quoter.Framework.Services.Messaging;
using Quoter.Framework.Services.AppSettings;
using Quoter.Shared.Enums;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Quoter.App.FormsControllers.Settings
{
	/// <summary>
	/// Controller for the Settings Tab of the <see cref="ManageForm"/>
	/// </summary>
	public class SettingsFormController : ISettingsFormController, INotifyPropertyChanged
	{
		private readonly IAppSettings _settings;
		private readonly IMessagingService _messagingService;
		private readonly IStringResources _stringResources;
		private readonly IFormsManager _formsManager;
		private readonly ISoundService _soundService;
		private readonly ILogger _logger;

		private ISettingsForm _form;

		private string _notificationIntervalMinutes;
		public string NotificationsIntervalMinutes
		{
			get => _notificationIntervalMinutes;
			set
			{
				if (_notificationIntervalMinutes != value)
				{
					_notificationIntervalMinutes = value;
					OnPropertyChanged();

					(bool isValidInput, int intValue) = IsValidInteger(value);
					if (isValidInput && intValue > 0)
					{
						_settings.NotificationIntervalSeconds = intValue * 60;
						_form.SetStatus("", Constants.ColorDefault);

						_messagingService.SendMessage(Event.NotificationIntervalChanged);
					}
					else
					{
						_form.SetStatus(_stringResources["NotificationIntervalMustBeBetweenValues", "1", "999"], Color.Red);
					}
				}
			}
		}

		private string _notificationsAutoCloseSeconds;
		public string NotificationsAutoCloseSeconds
		{
			get => _notificationsAutoCloseSeconds;
			set
			{
				_notificationsAutoCloseSeconds = value;
				OnPropertyChanged();

				(bool isValidInput, int intValue) = IsValidInteger(value);
				if (isValidInput && intValue > 0)
				{
					_settings.AutoCloseNotificationSeconds = intValue;
					_form.SetStatus("", Constants.ColorDefault);
				}
				else
				{
					_form.SetStatus(_stringResources["NotificationIntervalMustBeBetweenValues", "1", "999"], Color.Red);
				}
			}
		}

		private string _opacityValue;
		public string OpacityValue
		{
			get => _opacityValue;
			set
			{
				if (_opacityValue != value)
				{
					_opacityValue = value;
					OnPropertyChanged();
				}
			}
		}

		private string _selectedNotificationSound;
		public string SelectedNotificationSound
		{
			get => _selectedNotificationSound;
			set
			{
				if (_selectedNotificationSound != value)
				{
					_selectedNotificationSound = value;
					OnPropertyChanged();
				}
			}
		}

		private UpdateModeItem? _selectedUpdateMode;
		public UpdateModeItem? SelectedUpdateMode
		{
			get => _selectedUpdateMode;
			set
			{
				if (_selectedUpdateMode != value)
				{
					_selectedUpdateMode = value;
					OnPropertyChanged();
				}
				_logger.Debug($"SelectedUpdateMode:{SelectedUpdateMode.DisplayName}");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public SettingsFormController(IAppSettings settings,
										IMessagingService messagingService,
										IStringResources stringResources,
										IFormsManager formsManager,
										ISoundService soundService,
										ILogger logger)
		{
			_settings = settings;
			_messagingService = messagingService;
			_stringResources = stringResources;
			_formsManager = formsManager;
			_soundService = soundService;
			_logger = logger;
		}

		public void RegisterForm(ISettingsForm form)
		{
			_form = form;
		}

		public Task EventFormLoadedAsync()
		{
			// Set selected language buttons
			EnumLanguage selectedLanguage = LanguageHelper.GetEnumLanguageFromString(_settings.Language);
			_form.SetSelectedLanguage(selectedLanguage);

			// Set if the collections are shown by language or not
			_form.SetSelectedCollectionByLanguage(_settings.ShowCollectionsBasedOnLanguage);

			// Set other settings
			NotificationsIntervalMinutes = (_settings.NotificationIntervalSeconds / 60).ToString();
			NotificationsAutoCloseSeconds = _settings.AutoCloseNotificationSeconds.ToString();

			_form.SetOpacitySlider(_settings.Opacity);
			OpacityValue = GetOpacityValuePercent(_settings.Opacity);

			_form.SetShowWelcomeMessage(_settings.ShowWelcomeNotification);

			_form.SetNotificationsType(_settings.NotificationType);
			_form.SetNotificationsLocation(_settings.NotificationOpenAnimation);
			_form.SetNotificationFont(_settings.FontName, _settings.FontStyle, _settings.FontSize);
			_form.SetIsStartWithWindows(_settings.IsStartWithWindows);

			List<string> notificationSounds = new List<string>()
			{
				"-",
				"Click",
				"Pop",
				"Arpeggio",
				"Bell"
			};
			_form.SetNotificationSounds(notificationSounds);
			switch (_settings.NotificationSound)
			{
				case EnumSound.None:
					SelectedNotificationSound = "-"; break;
				case EnumSound.Click:
					SelectedNotificationSound = "Click"; break;
				case EnumSound.Pop:
					SelectedNotificationSound = "Pop"; break;
				case EnumSound.Arpeggio:
					SelectedNotificationSound = "Arpeggio"; break;
				case EnumSound.Bell:
					SelectedNotificationSound = "Bell"; break;
			}

			SetUpdateModes();

			return Task.CompletedTask;
		}

		public Task EventFormClosingAsync()
		{
			// Nothing to do
			return Task.CompletedTask;
		}

		public void SetLanguage(EnumLanguage language)
		{
			switch (language)
			{
				case EnumLanguage.English:
					CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
					Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
					_settings.Language = "en-US";
					_form.SetSelectedLanguage(language);
					break;
				case EnumLanguage.Romanian:
					CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ro-RO");
					Thread.CurrentThread.CurrentUICulture = new CultureInfo("ro-RO");
					_settings.Language = "ro-RO";
					_form.SetSelectedLanguage(language);
					break;
				case EnumLanguage.French:
					CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("fr-FR");
					Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
					_settings.Language = "fr-FR";
					_form.SetSelectedLanguage(language);
					break;
			}
			_form.LocalizeControls();
			SetUpdateModes();
			_messagingService.SendMessage(Event.LanguageChanged);
		}

		public void SetShowCollectionsBasedOnLanguage(bool value)
		{
			_settings.ShowCollectionsBasedOnLanguage = value;
			_messagingService.SendMessage(Event.ShowCollectionsBasedOnLanguageChanged);
		}

		public void SetShowWelcomeMessage(bool value)
		{
			_settings.ShowWelcomeNotification = value;
		}

		public void SetTheme(EnumTheme theme)
		{
			_settings.Theme = theme;
			// theme will be set on ManageFormController
			_messagingService.SendMessage(Event.ThemeChanged);
		}

		public void SetOpacity(double opacity)
		{
			_settings.Opacity = opacity;
			OpacityValue = GetOpacityValuePercent(opacity);
			_messagingService.SendMessage(Event.ThemeChanged);
		}

		public void SetNightMode()
		{
			_settings.IsNightMode = !_settings.IsNightMode;
			_messagingService.SendMessage(Event.ThemeChanged);
		}

		public void SetNotificationType(EnumNotificationType type)
		{
			if (_settings.NotificationType != type)
			{
				_messagingService.SendMessage(Event.NotificationTypeChanged, null);
				_settings.NotificationType = type;

				if (type == EnumNotificationType.AlwaysOn)
				{
					//Task.Run(() =>
					//{
					_formsManager.Show<QuoteForm>(0);
					//});
				}
			}
		}

		public void SetNotificationAnimation(EnumAnimation animation)
		{
			_settings.NotificationOpenAnimation = animation;
		}

		public void SelectNotificationFont()
		{
			FontDialog fontDialog = new FontDialog();
			fontDialog.FontMustExist = true;
			fontDialog.ShowApply = false;
			fontDialog.ShowEffects = false;
			fontDialog.MinSize = 8;
			fontDialog.MaxSize = 20;
			DialogResult result = fontDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				_settings.FontName = fontDialog.Font.Name;
				_settings.FontStyle = fontDialog.Font.Style.ToString();
				_settings.FontSize = fontDialog.Font.Size;
				_form.SetNotificationFont(_settings.FontName, _settings.FontStyle, _settings.FontSize);
			}
		}

		public void SetStartWithWindows(bool startWithWindows)
		{
			_settings.IsStartWithWindows = startWithWindows;
			try
			{
				if (_settings.IsStartWithWindows)
				{
					string startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
					string shortCutLinkFilePath = Path.Combine(startupFolderPath, Application.ProductName + ".lnk");
					WshShell shell = new WshShell();
					IWshShortcut windowsApplicationShortcut = (IWshShortcut)shell.CreateShortcut(shortCutLinkFilePath);
					windowsApplicationShortcut.Description = "Startup MinuteVerse";
					windowsApplicationShortcut.WorkingDirectory = Application.StartupPath;
					windowsApplicationShortcut.TargetPath = Application.ExecutablePath;
					windowsApplicationShortcut.Save();
				}
				else
				{
					string startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
					string shortCutLinkFilePath = Path.Combine(startupFolderPath, Application.ProductName + ".lnk");
					if (System.IO.File.Exists(shortCutLinkFilePath))
					{
						System.IO.File.Delete(shortCutLinkFilePath);
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}
		}

		public void SetSelectedNotificationSound(EnumSound selectedSound)
		{
			_settings.NotificationSound = selectedSound;
			switch (_settings.NotificationSound)
			{
				case EnumSound.None:
					SelectedNotificationSound = "-"; break;
				case EnumSound.Click:
					SelectedNotificationSound = "Click"; break;
				case EnumSound.Pop:
					SelectedNotificationSound = "Pop"; break;
				case EnumSound.Arpeggio:
					SelectedNotificationSound = "Arpeggio"; break;
				case EnumSound.Bell:
					SelectedNotificationSound = "Bell"; break;
			}
		}

		public void PlayCurrentNotificationSound()
		{
			_soundService.Play(_settings.NotificationSound);
		}

		public void SetWindowSize(Size size)
		{
			_settings.WindowSize = size;
		}

		public void SetSelectedUpdateMode(UpdateModeItem updateModeItem)
		{
			_settings.AutoUpdate = updateModeItem.UpdateMode;
			SelectedUpdateMode = updateModeItem;
		}

		private string GetOpacityValuePercent(double opacity)
		{
			return ((int)(opacity * 100)).ToString() + " %";
		}

		private Tuple<bool, int> IsValidInteger(string value)
		{
			const int maxValue = 999;
			bool isNumber = int.TryParse(value, out int intValue);
			if (isNumber)
			{
				if (intValue > 0 || intValue < maxValue)
				{
					return new(true, intValue);
				}
				else
				{
					return new(false, intValue);
				}

			}
			else if (string.IsNullOrWhiteSpace(value))
			{
				return new(true, 0);
			}
			return new(false, 0);
		}

		private void SetUpdateModes()
		{
			List<UpdateModeItem> lstUpdateModeItems = GetUpdateModesLocalized();
			_form.SetUpdateModes(lstUpdateModeItems);
			SelectedUpdateMode = lstUpdateModeItems.First(ui => ui.UpdateMode == _settings.AutoUpdate);
		}

		private List<UpdateModeItem> GetUpdateModesLocalized()
		{
			return new List<UpdateModeItem>()
			{
				new UpdateModeItem() { UpdateMode = EnumAutoUpdate.Auto, DisplayName = _stringResources["UpdateAuto"] },
				new UpdateModeItem() { UpdateMode = EnumAutoUpdate.AskFirst, DisplayName = _stringResources["UpdateAsk"] },
				new UpdateModeItem() { UpdateMode = EnumAutoUpdate.None, DisplayName = _stringResources["UpdateNever"] }
			};
		}
	}
}
