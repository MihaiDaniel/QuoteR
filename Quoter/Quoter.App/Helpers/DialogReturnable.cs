using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Helpers
{
	public class DialogReturnable : IDialogResult
	{
		public DialogResult DialogResult { get; private set; }

		public string StringResult { get; private set; }


		public DialogReturnable(DialogResult dialogResult, string returnResult)
		{
			DialogResult = dialogResult;
			StringResult = returnResult;
		}
	}
}
