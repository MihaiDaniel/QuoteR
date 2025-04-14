using Quoter.App.Models;
using Quoter.Framework.Enums;
using Quoter.Shared.Enums;

namespace Quoter.App.Forms.Manage
{
	/// <summary>
	/// Interface for the settings form
	/// </summary>
	/// <remarks>
	/// This is actually a tab in the <see cref="Forms.ManageForm"/>
	/// Associated controller: <see cref="FormsControllers.Settings.ISettingsFormController"/>
	/// </remarks>
	public interface ISettingsForm : IManageForm
	{
		void LocalizeControls();

		void SetSelectedLanguage(EnumLanguage language);

		void SetSelectedCollectionByLanguage(bool isShowByLanguage);

		void SetOpacitySlider(double opacity);

		void SetShowWelcomeMessage(bool value);

		void SetNotificationsType(EnumNotificationType type);

		void SetNotificationsLocation(EnumAnimation notificationOpenAnimation);

		void SetNotificationFont(string fontName, string fontStyle, float fontSize);

		void SetIsStartWithWindows(bool isStartWithWindows);

		void SetNotificationSounds(List<string> notificationSounds);

		void SetUpdateModes(List<UpdateModeItem> lstUpdateModeItems);
	}
}
