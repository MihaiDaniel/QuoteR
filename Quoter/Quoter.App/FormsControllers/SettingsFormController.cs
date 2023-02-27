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
	public class SettingsFormController : ISettingsFormController
	{
		public int NotificationsFrequency { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public int AutoCloseNotificationsSeconds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public LanguageModel SelectedLanguage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public BindingList<LanguageModel> Languages => throw new NotImplementedException();

		public void SetTheme(EnumTheme theme)
		{
			throw new NotImplementedException();
		}
	}
}
