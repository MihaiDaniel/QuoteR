using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Helpers
{
	/// <summary>
	/// Contains application constants, descriptions
	/// </summary>
	public static class Const
	{
		public static readonly Color ColorError = Color.Red;
		public static readonly Color ColorWarn = Color.DarkOrange;
		public static readonly Color ColorOk = Color.Green;
		public static readonly Color ColorDefault = Color.SlateGray;

		public static class AppColor
		{
			public static readonly Color ColorWindow = Color.WhiteSmoke;
		}


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

			public const string NotificationOpenAnimation = "NotificationOpenAnimation";

			public const string NotificationCloseAnimation = "NotificationCloseAnimation";
			/// <summary>
			/// Indicates in how many seconds the notifications with quotes will closed automatically. 0 to never close
			/// </summary>
			public const string AutoCloseNotificationSeconds = "AutoCloseNotificationSeconds";
			/// <summary>
			/// Indicates notification type
			/// </summary>
			public const string NotificationType = "NotificationType";
			/// <summary>
			/// Indicates if a welcome notification should be shown
			/// </summary>
			public const string ShowWelcomeNotification = "ShowWelcomeNotification";
			/// <summary>
			/// Indicates if the notification should not be automatically closed while
			/// the user has the mouse cursor over the window. Default is always on
			/// </summary>
			public const string KeepNotificationOpenOnMouseOver = "KeepNotificationOpenOnMouseOver";
			/// <summary>
			/// Supported languages in 2 language / country code ex: en-US
			/// </summary>
			public const string Language = "Language";
			/// <summary>
			/// If this is true, collections displayed in the UI and also quotes shown will be only of
			/// the language that the collection was created in. When a colection is created by default
			/// it's created with the current application language. If this is false, all collections are
			/// shown, and quotes can be used from any collection regardless of app language
			/// </summary>
			public const string ShowCollectionsBasedOnLanguage = "ShowCollectionsBasedOnLanguage";
			/// <summary>
			/// Indicator if it's the first startup of the application.
			/// </summary>
			public const string IsFirstStart = "IsFirstStart";
			/// <summary>
			/// Indicates if the user paused the quotes notifications. If this is true no notifications will be shown.
			/// </summary>
			public const string IsPaused = "IsPaused";
			/// <summary>
			/// Connection string to the SQLite database. Set when the application first starts.
			/// </summary>
			public const string ConnectionString = "ConnectionString";
			/// <summary>
			/// Current UI theme of the application
			/// </summary>
			public const string Theme = "Theme";
			/// <summary>
			/// The opacity of the quotes notification window
			/// </summary>
			public const string Opacity = "Opacity";
		}

		public static class SettingDefault
		{
			public const int NotificationIntervalSeconds = 900;	// 15 minutes
			public const int AutoCloseNotificationSeconds = 30; // 30 seconds
			public const bool ShowWelcomeNotification = true;
			public const bool KeepNotificationOpenOnMouseOver = true;
			public const bool ShowCollectionsBasedOnLanguage = false;
			public const int NotificationType = 1; // popup
		}

	}
}
