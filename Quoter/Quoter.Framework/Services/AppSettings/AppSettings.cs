using Quoter.Framework.Data;
using Quoter.Framework.Data.Entities;
using Quoter.Framework.Enums;
using System.Collections.Concurrent;
using System.Drawing;

namespace Quoter.Framework.Services.AppSettings
{
	public class AppSettings : IAppSettings
	{
		private readonly QuoterContext _context;
		private readonly ConcurrentDictionary<string, object> _settingsDic;

		#region IAppSettings

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

		public bool IsUpgradeRequired
		{
			get
			{
				return Get<bool>(nameof(IsUpgradeRequired));
			}
			set
			{
				Set<bool>(nameof(IsUpgradeRequired), value);
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

		public string WebApiUrl
		{
			get
			{
				return Get<string>(nameof(WebApiUrl));
			}
			set
			{
				Set<string>(nameof(WebApiUrl), value);
			}
		}

		public string RegistrationId
		{
			get
			{
				return Get<string>(nameof(RegistrationId));
			}
			set
			{
				Set<string>(nameof(RegistrationId), value);
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

		public bool IsNightMode
		{
			get
			{
				return Get<bool>(nameof(IsNightMode));
			}
			set
			{
				Set<bool>(nameof(IsNightMode), value);
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

		#endregion IAppSettings

		public AppSettings(QuoterContext context)
		{
			_context = context;
			_settingsDic = new ConcurrentDictionary<string, object>();
		}

		/// <summary>
		/// Returns a setting's value. Get first from in memory dic if found.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		private T Get<T>(string key)
		{
			if (_settingsDic.ContainsKey(key))
			{
				return (T)_settingsDic[key];
			}
			else
			{
				Setting? setting = _context.Settings.FirstOrDefault(s => s.Name == key);
				if (setting is not null)
				{
					object value = ConvertToType<T>(setting.Value);
					_settingsDic.TryAdd(setting.Name, value);
					return (T)value;
				}
				throw new ArgumentException($"Setting '{key}' does not exist.");
			}
		}

		/// <summary>
		/// Sets a setting value. Saves changes to the database if values differ
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <exception cref="ArgumentNullException"></exception>
		private void Set<T>(string key, T value)
		{
			if(value is null)
			{
				throw new ArgumentNullException($"Can't set setting '{key}' value to NULL");
			}
			Setting? setting = _context.Settings.FirstOrDefault(s => s.Name == key);
			if (setting is not null)
			{
				_settingsDic.AddOrUpdate(setting.Name, value, (existingKey, existingVal) =>
				{
					if(existingKey == setting.Name && existingVal != (object)value)
					{
						existingVal = value;
					}
					return existingVal;
				});
				string strValue = ConvertToString<T>(value);
				if(setting.Value != strValue)
				{
					setting.Value = strValue;
					_context.SaveChanges();
				}
			}
		}

		public static string ConvertToString<T>(T value)
		{
			if(typeof(T) == typeof(Size))
			{  
				Size? size = value as Size?;
				return $"{size!.Value.Width},{size!.Value.Height}";
			}
			else
			{
				return value.ToString();
			}
		}

		public static object ConvertToType<T>(string value)
		{
			if (typeof(T) == typeof(string))
			{
				return value;
			}
			else if (typeof(T) == typeof(int))
			{
				return int.Parse(value);
			}
			else if (typeof(T) == typeof(float))
			{
				return float.Parse(value);
			}
			else if (typeof(T) == typeof(double))
			{
				return double.Parse(value);
			}
			else if (typeof(T) == typeof(bool))
			{
				return bool.Parse(value);
			}
			else if (typeof(T) == typeof(Guid))
			{
				return Guid.Parse(value);
			}
			else if (typeof(T) == typeof(Size))
			{
				string[] values = value.Split(',');
				return new Size(int.Parse(values[0]), int.Parse(values[1]));
			}
			throw new ArgumentException($"Setting value '{value}' is of unknown type.");
		}
	}
}
