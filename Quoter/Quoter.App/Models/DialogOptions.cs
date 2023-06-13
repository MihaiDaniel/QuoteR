using Quoter.App.Enums;
using Quoter.App.Helpers;
using Quoter.Framework.Enums;

namespace Quoter.App.Models
{
	public class DialogOptions
	{
		public DialogOptionsTheme DialogTheme { get; set; } = DialogOptionsTheme.Default;

		public DialogOptionsSound DialogSound { get; set; } = DialogOptionsSound.Warning;

		public string Title { get; set; }

		public string Message { get; set; }

		public string Value { get; set; }

		public int ValueMaxLength { get; set; } = 80;

		public EnumDialogButtons MessageBoxButtons { get; set; } = EnumDialogButtons.Ok;

		public EnumAnimation OpenAnimation { get; set; } = EnumAnimation.None;

	}
}
