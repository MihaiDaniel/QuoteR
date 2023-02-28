using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Forms
{
	public interface IForm
	{
		void SetTheme();
		void SetStatus(string message, Color color);
	}
}
