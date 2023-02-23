using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Helpers
{
	public interface IDialogReturnable
	{
		DialogResult DialogResult { get; }

		string StringResult { get; }
	}
}
