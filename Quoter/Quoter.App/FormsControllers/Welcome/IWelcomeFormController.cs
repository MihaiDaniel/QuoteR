﻿using Quoter.App.Forms.Welcome;
using Quoter.Framework.Enums;
using Quoter.Shared.Enums;

namespace Quoter.App.FormsControllers.Welcome
{
	public interface IWelcomeFormController : IFormController<IWelcomeForm>
	{
		void SetSelectedTab(EnumWelcomeTab tab);
		void SetLanguage(EnumLanguage language);
		void SetNotificationsInterval(int interval);
		void FinishSetup();
		bool Close();
	}
}
