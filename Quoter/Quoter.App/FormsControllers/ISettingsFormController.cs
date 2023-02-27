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

		int NotificationsFrequency { get; set; }

		int AutoCloseNotificationsSeconds { get; set; }

		LanguageModel SelectedLanguage { get; set; }

		BindingList<LanguageModel> Languages { get; }

		void SetTheme(EnumTheme theme);

	}
}
