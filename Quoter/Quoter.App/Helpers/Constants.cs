using Quoter.Framework.Enums;

namespace Quoter.App.Helpers
{
	/// <summary>
	/// Contains application constants, descriptions
	/// </summary>
	public static class Constants
	{
		public static class Colors
		{
			public static readonly Color Error = Color.Red;
			public static readonly Color Warn = Color.DarkOrange;
			public static readonly Color Ok = Color.Green;
			public static readonly Color Default = Color.SlateGray;
		}


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

		public static class SettingDefault
		{
			public const int NotificationIntervalSeconds = 900;	// 15 minutes
			public const int AutoCloseNotificationSeconds = 30; // 30 seconds
			public const bool ShowWelcomeNotification = true;
			public const bool KeepNotificationOpenOnMouseOver = true;
			public const bool ShowCollectionsBasedOnLanguage = false;
			public const EnumNotificationType NotificationType = EnumNotificationType.Popup; // popup
		}

	}
}
