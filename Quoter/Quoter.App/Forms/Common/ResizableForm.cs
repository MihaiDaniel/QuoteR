namespace Quoter.App.Forms.Common
{
	public class ResizableForm : Form
	{
		private const int WM_NCHITTEST = 0x0084;
		private const int HTBOTTOMRIGHT = 17;
		int WM_DWMCOMPOSITIONCHANGED = 0x031E;

		public ResizableForm()
		{
			
		}

		public void CreateResizePictureBox()
		{
			PictureBox pbResize = new PictureBox();
			pbResize.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			pbResize.Cursor = Cursors.SizeNWSE;
			pbResize.Enabled = false;
			pbResize.Image = Resources.Resources.resize_12;

			int locationX = this.Width - 12 - 2;
			int locationY = this.Height - 12 - 2;
			pbResize.Location = new Point(locationX, locationY);
			pbResize.Name = "pbResize";
			pbResize.Size = new Size(12, 12);
			pbResize.TabIndex = 20;
			pbResize.TabStop = false;
			Controls.Add(pbResize);
		}

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

		// Can use this as alternative for bad flickering. Stops drawing while resizing
		//protected override void OnResizeBegin(EventArgs e)
		//{
		//	SuspendLayout();
		//	base.OnResizeBegin(e);
		//}
		//protected override void OnResizeEnd(EventArgs e)
		//{
		//	ResumeLayout();
		//	base.OnResizeEnd(e);
		//}
	}
}
