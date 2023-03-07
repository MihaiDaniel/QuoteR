using Quoter.Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Forms
{
	public interface ISettingsForm : IForm
	{
		void LocalizeControls();

		void SetSelectedLanguage(EnumLanguage language);
		
		void SetSelectedCollectionByLanguage(bool isShowByLanguage);

		void SetOpacitySlider(double opacity);

		void SetShowWelcomeMessage(bool value);

		void SetNotificationsType(EnumNotificationType type);

		void SetNotificationsLocation(EnumAnimation notificationOpenAnimation);

		void SetNotificationFont(string fontName, string fontStyle, float fontSize);
	}
}
