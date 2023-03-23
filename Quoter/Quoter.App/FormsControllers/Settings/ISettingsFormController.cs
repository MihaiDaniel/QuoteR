using Quoter.App.Forms;
using Quoter.Framework.Enums;
using System.ComponentModel;

namespace Quoter.App.FormsControllers.Settings
{
	public interface ISettingsFormController : IFormController<ISettingsForm>
	{

		string NotificationsIntervalMinutes { get; set; }

		string NotificationsAutoCloseSeconds { get; set; }

		string SelectedNotificationSound { get; set; }

		string OpacityValue { get; set; }

		void SetTheme(EnumTheme theme);

		void SetLanguage(EnumLanguage language);

		void SetShowCollectionsBasedOnLanguage(bool value);

		void SetShowWelcomeMessage(bool value);

		void SetOpacity(double opacity);

		void SetNotificationType(EnumNotificationType type);

		void SetNotificationAnimation(EnumAnimation animation);
		void SelectNotificationFont();
		void SetStartWithWindows(bool value);
		void SetSelectedNotificationSound(EnumSound selectedSound);
		void PlayCurrentNotificationSound();
		void SetWindowSize(Size size);
	}
}
