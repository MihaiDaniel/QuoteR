using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Helpers
{
	/// <summary>
	/// Contains application constants
	/// </summary>
	public static class Const
	{
		public static readonly Color ColorError = Color.Red;
		public static readonly Color ColorWarn = Color.DarkOrange;
		public static readonly Color ColorOk = Color.Green;
		public static readonly Color ColorDefault = Color.SlateGray;

		/// <summary>
		/// Name of the directory where the collections of quotes will be stored
		/// </summary>
		public const string DirectoryCollections = "Collections";

		/// <summary>
		/// Contains names of application settings
		/// </summary>
		public static class Setting
		{
			/// <summary>
			/// Path of the directory where collections of quotes are stored
			/// </summary>
			public const string CollectionsDirectory = "CollectionsDirectory";
			/// <summary>
			/// Indicates how often notifications with quotes will be shown. In seconds.
			/// </summary>
			public const string NotificationIntervalSeconds = "NotificationIntervalSeconds";
			/// <summary>
			/// Indicates in how many seconds the notifications with quotes will closed automatically. 0 to never close
			/// </summary>
			public const string AutoCloseNotificationSeconds = "AutoCloseNotificationSeconds";
			/// <summary>
			/// Indicates if a welcome notification should be shown
			/// </summary>
			public const string ShowWelcomeNotification = "ShowWelcomeNotification";

			public const string KeepNotificationOpenOnMouseOver = "KeepNotificationOpenOnMouseOver";

			public const string IsFirstStart = "IsFirstStart";

			public const string IsPaused = "IsPaused";

			public const string ConnectionString = "ConnectionString";
		}

		public static class SettingDefault
		{
			public const int NotificationIntervalSeconds = 900;	// 15 minutes
			public const int AutoCloseNotificationSeconds = 30; // 30 seconds
			public const bool ShowWelcomeNotification = true;
			public const bool KeepNotificationOpenOnMouseOver = true;

		}

	}
}
