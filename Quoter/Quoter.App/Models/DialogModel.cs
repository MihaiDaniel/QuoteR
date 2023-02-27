using Quoter.Framework.Enums;

namespace Quoter.App.Models
{
	public class DialogModel
	{
		public Color TitleColor { get; set; } = Color.SlateGray;

		public string Title { get; set; }

		public string Message { get; set; }

		public string Value { get; set; }

		public EnumDialogButtons MessageBoxButtons { get; set; } = EnumDialogButtons.Ok;
	}
}
