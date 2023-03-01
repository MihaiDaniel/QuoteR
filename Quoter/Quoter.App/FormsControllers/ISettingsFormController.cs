using Quoter.App.Forms;
using Quoter.Framework.Enums;

namespace Quoter.App.FormsControllers
{
	public interface ISettingsFormController
	{
		/// <summary>
		/// Mandatory step before using other methods.
		/// Due to DI we would have a circular dependency between the 
		/// controller and the form, so we use this method to avoid this.
		/// </summary>
		void RegisterForm(ISettingsForm form);

		string NotificationsIntervalMinutes { get; set; }

		string OpacityValue { get; set; }

		int AutoCloseNotificationsSeconds { get; set; }

		void SetTheme(EnumTheme theme);

		void SetLanguage(EnumLanguage language);

		void SetShowCollectionsBasedOnLanguage(bool value);

		void SetShowWelcomeMessage(bool value);

		void SetOpacity(double opacity);

		void SetNotificationType(EnumNotificationType type);
	}
}
