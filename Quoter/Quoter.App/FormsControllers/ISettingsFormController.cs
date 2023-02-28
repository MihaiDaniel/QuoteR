using Quoter.App.Forms;
using Quoter.App.Models;
using Quoter.Framework.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.FormsControllers
{
	public interface ISettingsFormController
	{
		void RegisterForm(ISettingsForm form);

		string NotificationsIntervalMinutes { get; set; }
		string OpacityValue { get; set; }

		int AutoCloseNotificationsSeconds { get; set; }

		void SetTheme(EnumTheme theme);
		void SetLanguage(EnumLanguage language);
		void SetShowCollectionsBasedOnLanguage(bool value);
		void SetShowWelcomeMessage(bool value);
		void SetOpacity(double opacity);
	}
}
