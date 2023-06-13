using Quoter.App.Helpers;
using Quoter.Framework.Enums;
using Quoter.Framework.Services;

namespace Quoter.App.Services
{
	public class Settings : ISettings
	{
		private readonly object _lock = new object();

		public bool IsFirstStart
		{
			get
			{
				return Get<bool>(nameof(IsFirstStart));
			}
			set
			{
				Set<bool>(nameof(IsFirstStart), value);
			}
		}

		public string InstallId
		{
			get
			{
				return Get<string>(nameof(InstallId));
			}
			set
			{
				Set<string>(nameof(InstallId), value);
			}
		}

		public bool IsPaused
		{
			get
			{
				return Get<bool>(nameof(IsPaused));
			}
			set
			{
				Set<bool>(nameof(IsPaused), value);
			}
		}

		public string FontName
		{
			get
			{
				return Get<string>(nameof(FontName));
			}
			set
			{
				Set<string>(nameof(FontName), value);
			}
		}

		public string FontStyle
		{
			get
			{
				return Get<string>(nameof(FontStyle));
			}
			set
			{
				Set<string>(nameof(FontStyle), value);
			}
		}

		public float FontSize
		{
			get
			{
				return Get<float>(nameof(FontSize));
			}
			set
			{
				Set<float>(nameof(FontSize), value);
			}
		}

		public int NotificationIntervalSeconds
		{
			get
			{
				return Get<int>(nameof(NotificationIntervalSeconds));
			}
			set
			{
				Set<int>(nameof(NotificationIntervalSeconds), value);
			}
		}

		public int AutoCloseNotificationSeconds
		{
			get
			{
				return Get<int>(nameof(AutoCloseNotificationSeconds));
			}
			set
			{
				Set<int>(nameof(AutoCloseNotificationSeconds), value);
			}
		}

		public EnumAnimation NotificationOpenAnimation
		{
			get
			{
				return (EnumAnimation)Get<int>(nameof(NotificationOpenAnimation));
			}
			set
			{
				Set<int>(nameof(NotificationOpenAnimation), (int)value);
			}
		}

		public EnumAnimation NotificationCloseAnimation
		{
			get
			{
				return (EnumAnimation)Get<int>(nameof(NotificationCloseAnimation));
			}
			set
			{
				Set<int>(nameof(NotificationCloseAnimation), (int)value);
			}
		}

		public EnumNotificationType NotificationType
		{
			get
			{
				return (EnumNotificationType)Get<int>(nameof(NotificationType));
			}
			set
			{
				Set<int>(nameof(NotificationType), (int)value);
			}
		}

		public EnumSound NotificationSound
		{
			get
			{
				return (EnumSound)Get<int>(nameof(NotificationSound));
			}
			set
			{
				Set<int>(nameof(NotificationSound), (int)value);
			}
		}

		
		public EnumAutoUpdate AutoUpdate
		{
			get
			{
				return (EnumAutoUpdate)Get<int>(nameof(AutoUpdate));
			}
			set
			{
				Set<int>(nameof(AutoUpdate), (int)value);
			}
		}

		public string WebApiDomainUrl
		{
			get
			{
				return Get<string>(nameof(WebApiDomainUrl));
			}
			set
			{
				Set<string>(nameof(WebApiDomainUrl), value);
			}
		}

		public Guid RegistrationId
		{
			get
			{
				return Get<Guid>(nameof(RegistrationId));
			}
			set
			{
				Set<Guid>(nameof(RegistrationId), value);
			}
		}

		public string Language
		{
			get
			{
				return Get<string>(nameof(Language));
			}
			set
			{
				Set<string>(nameof(Language), value);
			}
		}

		public bool ShowCollectionsBasedOnLanguage
		{
			get
			{
				return Get<bool>(nameof(ShowCollectionsBasedOnLanguage));
			}
			set
			{
				Set<bool>(nameof(ShowCollectionsBasedOnLanguage), value);
			}
		}

		public bool ShowWelcomeNotification
		{
			get
			{
				return Get<bool>(nameof(ShowWelcomeNotification));
			}
			set
			{
				Set<bool>(nameof(ShowWelcomeNotification), value);
			}
		}

		public bool KeepNotificationOpenOnMouseOver
		{
			get
			{
				return Get<bool>(nameof(KeepNotificationOpenOnMouseOver));
			}
			set
			{
				Set<bool>(nameof(KeepNotificationOpenOnMouseOver), value);
			}
		}

		public EnumTheme Theme
		{
			get
			{
				return (EnumTheme)Get<int>(nameof(Theme));
			}
			set
			{
				Set<int>(nameof(Theme), (int)value);
			}
		}
		public double Opacity
		{
			get
			{
				return Get<double>(nameof(Opacity));
			}
			set
			{
				Set<double>(nameof(Opacity), value);
			}
		}

		public bool IsStartWithWindows
		{
			get
			{
				return Get<bool>(nameof(IsStartWithWindows));
			}
			set
			{
				Set<bool>(nameof(IsStartWithWindows), value);
			}
		}

		public bool IsSetupFinished
		{
			get
			{
				return Get<bool>(nameof(IsSetupFinished));
			}
			set
			{
				Set<bool>(nameof(IsSetupFinished), value);
			}
		}

		public Size WindowSize
		{
			get
			{
				return Get<Size>(nameof(WindowSize));
			}
			set
			{
				Set<Size>(nameof(WindowSize), value);
			}
		}

		public void SetDefaults()
		{
			NotificationIntervalSeconds = Constants.SettingDefault.NotificationIntervalSeconds;
			AutoCloseNotificationSeconds = Constants.SettingDefault.AutoCloseNotificationSeconds;
			ShowWelcomeNotification = Constants.SettingDefault.ShowWelcomeNotification;
			KeepNotificationOpenOnMouseOver = Constants.SettingDefault.KeepNotificationOpenOnMouseOver;
			ShowCollectionsBasedOnLanguage = Constants.SettingDefault.ShowCollectionsBasedOnLanguage;
			NotificationType = Constants.SettingDefault.NotificationType;
			NotificationSound = EnumSound.Click;
		}

		/// <summary>
		/// Gets the value of a setting thread-safe based on the <paramref name="key"/>
		/// </summary>
		/// <typeparam name="T">Type expected of the setting value</typeparam>
		/// <param name="key">Key of the setting</param>
		/// <returns></returns>
		private T Get<T>(string key)
		{
			lock (_lock)
			{
				ValidateSetting<T>(key);
				return (T)Properties.Settings.Default[key];
				
			}
		}

		/// <summary>
		/// Sets the value of a setting thread-safe if it's different than the current value and saves changes.
		/// </summary>
		/// <typeparam name="T">Type expected of the setting value</typeparam>
		/// <param name="key">Key of the setting</param>
		/// <param name="value">Value to set the setting to</param>
		private void Set<T>(string key, T value)
		{
			lock(_lock)
			{
				ValidateSetting<T>(key);
				if (Properties.Settings.Default[key] != (object?)value)
				{
					Properties.Settings.Default[key] = value;
					Properties.Settings.Default.Save();
				}
			}
		}

		private void ValidateSetting<T>(string key)
		{
			if (Properties.Settings.Default[key] == null)
			{
				throw new ArgumentException($"Setting {key} does not exist");

			}
			if (Properties.Settings.Default[key].GetType() != typeof(T))
			{
				throw new ArgumentException($"Setting {key} is not of type {typeof(T)}");
			}
		}
	}
}
