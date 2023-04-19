using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Quoter.App.Controls
{
	/// <summary>
	/// This class allows to apply shadow effect on windows with style None.
	/// new DropShadow().ApplyShadows(this); add this line after IntializeComponent()
	/// </summary>
	public static class DropShadow
	{
		[EditorBrowsable(EditorBrowsableState.Never)]
		public struct MARGINS
		{
			public int leftWidth;
			public int rightWidth;
			public int topHeight;
			public int bottomHeight;
		}


		[DllImport("dwmapi.dll")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

		[DllImport("dwmapi.dll")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

		public static void ApplyShadows(Form form)
		{
			var v = 2;

			DwmSetWindowAttribute(form.Handle, 2, ref v, 4);

			MARGINS margins = new MARGINS()
			{
				bottomHeight = 1,
				leftWidth = 0,
				rightWidth = 0,
				topHeight = 0
			};

			DwmExtendFrameIntoClientArea(form.Handle, ref margins);
		}

	}
}
