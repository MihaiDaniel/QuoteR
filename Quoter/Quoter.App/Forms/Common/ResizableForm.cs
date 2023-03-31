using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Forms.Common
{
	public class ResizableForm : Form
	{
		private const int WM_NCHITTEST = 0x0084;
		private const int HTBOTTOMRIGHT = 17;


		/// <summary>
		/// Used to make the form resizable considering the fact it does not have any borders
		/// </summary>
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if (m.Msg == WM_NCHITTEST)
			{
				Point p = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
				if (p.X >= this.ClientSize.Width - 16 && p.Y >= this.ClientSize.Height - 16)
				{
					m.Result = (IntPtr)HTBOTTOMRIGHT;
				}
			}
		}
	}
}
