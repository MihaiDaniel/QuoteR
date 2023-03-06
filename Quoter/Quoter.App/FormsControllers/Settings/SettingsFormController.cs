using Quoter.App.Forms;
using Quoter.App.Helpers;
using Quoter.App.Services;
using Quoter.App.Services.Forms;
using Quoter.App.Views;
using Quoter.Framework.Enums;
using Quoter.Framework.Models;
using Quoter.Framework.Services;
using Quoter.Framework.Services.Messaging;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Quoter.App.FormsControllers.Settings
{
	public class SettingsFormController : ISettingsFormController, INotifyPropertyChanged
	{
		private readonly ISettings _settings;
		private readonly IMessagingService _messagingService;
		private readonly IStringResources _stringResources;
		private readonly IFormsManager _formsManager;
		private readonly IQuoteService _quoteService;

		private ISettingsForm _form;

		public SettingsFormController(ISettings settings,
										IMessagingService messagingService,
										IStringResources stringResources,
										IFormsManager formsManager,
										IQuoteService quoteService)
		{
			_settings = settings;
			_messagingService = messagingService;
			_stringResources = stringResources;
			_formsManager = formsManager;
			_quoteService = quoteService;
		}

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

					(bool isValidInput, int intValue) = ValidateValidNumber(value);
					if (isValidInput && intValue > 0)
					{
						_settings.Set(Const.Setting.NotificationIntervalSeconds, intValue * 60);
						_form.SetStatus("", Const.ColorDefault);

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
				_notificationsAutoCloseSeconds= value;
				OnPropertyChanged();

				(bool isValidInput, int intValue) = ValidateValidNumber(value);
				if (isValidInput && intValue > 0)
				{
					_settings.Set(Const.Setting.AutoCloseNotificationSeconds, intValue);
					_form.SetStatus("", Const.ColorDefault);
				}
				else
				{
					_form.SetStatus(_stringResources["NotificationIntervalMustBeBetweenValues", "1", "999"], Color.Red);
				}
			}
		}

		private Tuple<bool, int> ValidateValidNumber(string value)
		{
			bool isNumber = int.TryParse(value, out int intValue);
			if (isNumber)
			{
				if (intValue > 0 || intValue < 999)
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
				if (_opacityValue != value)
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
			NotificationsAutoCloseSeconds = _settings.Get<int>(Const.Setting.AutoCloseNotificationSeconds).ToString();

			double opacity = _settings.Get<double>(Const.Setting.Opacity);
			_form.SetOpacitySlider(opacity);
			OpacityValue = GetOpacityValuePercent(opacity);

			_form.SetShowWelcomeMessage(_settings.Get<bool>(Const.Setting.ShowWelcomeNotification));

			_form.SetNotificationsType((EnumNotificationType)_settings.Get<int>(Const.Setting.NotificationType));
		}

		public void SetLanguage(EnumLanguage language)
		{
			switch (language)
			{
				case EnumLanguage.English:
					CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
					Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
					_settings.Set(Const.Setting.Language, "en-US");
					_form.SetSelectedLanguage(language);
					break;
				case EnumLanguage.Romanian:
					CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ro-RO");
					Thread.CurrentThread.CurrentUICulture = new CultureInfo("ro-RO");
					_settings.Set(Const.Setting.Language, "ro-RO");
					_form.SetSelectedLanguage(language);
					break;
			}
			_form.LocalizeControls();
			_messagingService.SendMessage(Event.LanguageChanged);
		}

		public void SetShowCollectionsBasedOnLanguage(bool value)
		{
			_settings.Set(Const.Setting.ShowCollectionsBasedOnLanguage, value);
			_messagingService.SendMessage(Event.ShowCollectionsBasedOnLanguageChanged);
		}

		public void SetShowWelcomeMessage(bool value)
		{
			_settings.Set(Const.Setting.ShowWelcomeNotification, value);
		}

		public void SetTheme(EnumTheme theme)
		{
			_settings.Set(Const.Setting.Theme, (int)theme);
			_form.SetTheme();
			_messagingService.SendMessage(Event.ThemeChanged);
		}

		public void SetOpacity(double opacity)
		{
			_settings.Set(Const.Setting.Opacity, opacity);
			OpacityValue = GetOpacityValuePercent(opacity);
			_messagingService.SendMessage(Event.ThemeChanged);
		}

		private string GetOpacityValuePercent(double opacity)
		{
			return ((int)(opacity * 100)).ToString() + " %";
		}

		public void SetNotificationType(EnumNotificationType type)
		{
			EnumNotificationType currentType = (EnumNotificationType)_settings.Get<int>(Const.Setting.NotificationType);
			if (currentType != type)
			{
				_messagingService.SendMessage(Event.NotificationTypeChanged, null);
				_settings.Set(Const.Setting.NotificationType, (int)type);

				if (type == EnumNotificationType.AlwaysOn)
				{

					Task.Run(async () =>
					{
						//QuoteModel? quote = await _quoteService.GetRandomQuote();
						//if (quote != null)
						//{
						_formsManager.ShowDialog<QuoteForm>(0/*, quote*/);
						//}
					});
				}
			}


		}
	}
}
