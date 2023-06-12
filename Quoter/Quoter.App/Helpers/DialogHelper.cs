using Quoter.App.Forms;
using Quoter.App.Models;
using Quoter.App.Services.Forms;
using Quoter.Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Helpers
{
	public static class DialogHelper
	{
		public static void ShowWarning(IFormsManager formsManager, string title, string message)
		{
			DialogMessageFormOptions options = new DialogMessageFormOptions()
			{
				Message = message,
				Title = title,
				TitleColor = Constants.ColorWarn,
				MessageBoxButtons = EnumDialogButtons.Ok
			};
			formsManager.ShowDialog<DialogMessageForm>(options);
		}
	}
}
