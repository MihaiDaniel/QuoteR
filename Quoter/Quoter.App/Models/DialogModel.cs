using Quoter.App.Helpers;
using Quoter.Framework.Enums;

namespace Quoter.App.Models
{
	public class DialogMessageFormOptions
	{
		public Color TitleColor { get; set; } = Constants.ColorDefault;

		public string Title { get; set; }

		public string Message { get; set; }

		public string Value { get; set; }

		public int ValueMaxLength { get; set; } = 50;

		public EnumDialogButtons MessageBoxButtons { get; set; } = EnumDialogButtons.Ok;
	}
}
