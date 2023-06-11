using Quoter.Framework.Enums;
using System.Drawing;

namespace Quoter.Framework.Services
{
	/// <summary>
	/// Interface for settings. Settings should be saved automatically when is set.
	/// </summary>
	public interface ISettings
	{
		/// <summary>
		/// Indicates if the application is started for the first time
		/// </summary>
		bool IsFirstStart { get; set; }
		/// <summary>
		/// A guid generted when the application is started for the first time
		/// </summary>
		string InstallId { get; set; }
		/// <summary>
		/// Indicates if the notifications are paused or not
		/// </summary>
		bool IsPaused { get; set; }
		string Language { get; set; }
		string FontName { get; set; }
		string FontStyle { get; set; }
		float FontSize { get; set; }
		int NotificationIntervalSeconds { get; set; }
		int AutoCloseNotificationSeconds { get; set; }
		EnumAnimation NotificationOpenAnimation { get; set; }
		EnumAnimation NotificationCloseAnimation { get; set; }
		EnumNotificationType NotificationType { get; set; }
		EnumSound NotificationSound { get; set; }

		EnumAutoUpdate AutoUpdate { get; set; }
		string WebApiDomainUrl { get; set; }
		Guid RegistrationId { get; set; }

		bool ShowCollectionsBasedOnLanguage { get; set; }
		bool ShowWelcomeNotification { get; set; }
		bool KeepNotificationOpenOnMouseOver { get; set; }

		EnumTheme Theme { get; set; }
		double Opacity { get; set; }
		bool IsStartWithWindows { get; set; }
		bool IsSetupFinished { get; set; }
		Size WindowSize { get; set; }
	}
}
