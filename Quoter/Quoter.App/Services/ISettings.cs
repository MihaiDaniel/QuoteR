using Quoter.Framework.Enums;

namespace Quoter.App.Services
{
	public interface ISettings
	{
		bool IsFirstStart { get; set; }
		bool IsPaused { get; set; }
		string Language { get; set; }
		string FontName { get; set; }
		string FontStyle { get; set; }
		float FontSize { get; set; }
		int NotificationIntervalSeconds { get; set; }
		int AutoCloseNotificationSeconds { get; set; }
		EnumAnimation NotificationOpenAnimation { get; set; }
		EnumAnimation NotificationCloseAnimation { get; set;}
		EnumNotificationType NotificationType { get; set; }	
		EnumSound NotificationSound { get; set; }

		bool ShowCollectionsBasedOnLanguage { get; set; }
		bool ShowWelcomeNotification { get; set; }
		bool KeepNotificationOpenOnMouseOver { get; set; }

		EnumTheme Theme { get; set; }
		double Opacity { get; set; }
		bool IsStartWithWindows { get; set; }
		bool IsSetupFinished { get; set; }
	}
}
