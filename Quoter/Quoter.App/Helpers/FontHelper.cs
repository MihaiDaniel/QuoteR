using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Helpers
{
	public static class FontHelper
	{
		public static FontStyle GetFontStyle(string style)
		{
			return style switch
			{
				"Bold" => FontStyle.Bold,
				"Italic" => FontStyle.Italic,
				_ => FontStyle.Regular
			};
		}
	}
}
