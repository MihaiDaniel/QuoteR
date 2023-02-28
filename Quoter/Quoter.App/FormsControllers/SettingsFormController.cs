using Quoter.App.Forms;
using Quoter.App.Helpers;
using Quoter.App.Services;
using Quoter.Framework.Enums;
using Quoter.Framework.Services.Messaging;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Quoter.App.FormsControllers
{
	public class SettingsFormController : ISettingsFormController, INotifyPropertyChanged
	{
		private readonly ISettings _settings;
		private readonly IMessagingService _messagingService;
		private readonly IStringResources _stringResources;

		private ISettingsForm _form;

		public SettingsFormController(ISettings settings, 
										IMessagingService messagingService,
										IStringResources stringResources)
		{
			_settings = settings;
			_messagingService = messagingService;
			_stringResources = stringResources;
		}

		private string _notificationIntervalMinutes;
		public string NotificationsIntervalMinutes 
		{
			get => _notificationIntervalMinutes;
			set
			{
				if(_notificationIntervalMinutes != value)
				{
					_notificationIntervalMinutes = value;
					OnPropertyChanged();

					(bool isValidInput, int intValue) = ValidateNotificationsIntervalMinutes(value);
					if(isValidInput && intValue > 0)
					{
						_settings.Set<int>(Const.Setting.NotificationIntervalSeconds, intValue * 60);
						_form.SetStatus("", Color.Black);

						_messagingService.SendMessage(Const.Event.NotificationIntervalChanged);
					}
					else
					{
						_form.SetStatus(_stringResources["NotificationIntervalMustBeBetweenValues", "1", "999"], Color.Red);
					}
				}
			}
		}

		private Tuple<bool, int> ValidateNotificationsIntervalMinutes(string value)
		{
			bool isNumber = int.TryParse(value, out int intValue);
			if(isNumber)
			{
				if(intValue > 0 || intValue < 999)
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

		private string _opacityValue;
		public string OpacityValue
		{
			get => _opacityValue;
			set
			{
				if(_opacityValue != value)
				{
					_opacityValue = value;
					OnPropertyChanged();
				}
			}
		}

		public int AutoCloseNotificationsSeconds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public void RegisterForm(ISettingsForm form)
		{
			_form = form;

			// Set selected language buttons
			string appLanguage = _settings.Get<string>(Const.Setting.Language);
			EnumLanguage selectedLanguage = LanguageHelper.GetEnumLanguageFromString(appLanguage);
			_form.SetSelectedLanguage(selectedLanguage);

			// Set if the collections are shown by language or not
			bool isShowByLanguage = _settings.Get<bool>(Const.Setting.ShowCollectionsBasedOnLanguage);
			_form.SetSelectedCollectionByLanguage(isShowByLanguage);

			// Set other settings
			NotificationsIntervalMinutes = (_settings.Get<int>(Const.Setting.NotificationIntervalSeconds) / 60).ToString();

			double opacity = _settings.Get<double>(Const.Setting.Opacity);
			_form.SetOpacitySlider(opacity);
			OpacityValue = GetOpacityValuePercent(opacity);
		}

		public void SetLanguage(EnumLanguage language)
		{
			switch (language)
			{
				case EnumLanguage.English:
					CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
					Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
					_settings.Set<string>(Const.Setting.Language, "en-US");
					_form.SetSelectedLanguage(language);
					break;
				case EnumLanguage.Romanian:
					CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ro-RO");
					Thread.CurrentThread.CurrentUICulture = new CultureInfo("ro-RO");
					_settings.Set<string>(Const.Setting.Language, "ro-RO");
					_form.SetSelectedLanguage(language);
					break;
			}
			_form.LocalizeControls();
			_messagingService.SendMessage(Const.Event.LanguageChanged);
		}

		public void SetShowCollectionsBasedOnLanguage(bool value)
		{
			_settings.Set<bool>(Const.Setting.ShowCollectionsBasedOnLanguage, value);
			_messagingService.SendMessage(Const.Event.ShowCollectionsBasedOnLanguageChanged);
		}

		public void SetShowWelcomeMessage(bool value)
		{
			_settings.Set<bool>(Const.Setting.ShowWelcomeNotification, value);
		}

		public void SetTheme(EnumTheme theme)
		{
			_settings.Set<int>(Const.Setting.Theme, (int)theme);
			_form.SetTheme();
		}

		public void SetOpacity(double opacity)
		{
			_settings.Set<double>(Const.Setting.Opacity, opacity);
			OpacityValue = GetOpacityValuePercent(opacity);
		}

		private string GetOpacityValuePercent(double opacity)
		{
			return ((int)(opacity * 100)).ToString() + " %";
		}
	}
}
