using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quoter.App.Services.FormAnimation
{
	public class FormPositioningService : IFormPositioningService
	{
		private bool _isMouseDown;
		private Point _lastLocation;
		private Form _form;

		public void RegisterFormDragableByControl(Form form, Control control)
		{
			_form = form;
			control.MouseDown += new MouseEventHandler(pnlTitle_MouseDown);
			control.MouseMove += new MouseEventHandler(pnlTitle_MouseMove);
			control.MouseUp += new MouseEventHandler(pnlTitle_MouseUp);
		}

		private void pnlTitle_MouseDown(object sender, MouseEventArgs e)
		{
			_isMouseDown = true;
			_lastLocation = e.Location;
		}

		private void pnlTitle_MouseMove(object sender, MouseEventArgs e)
		{
			if (_isMouseDown)
			{
				_form.Location = new Point((_form.Location.X - _lastLocation.X) + e.X, (_form.Location.Y - _lastLocation.Y) + e.Y);
				_form.Update();
			}
		}

		private void pnlTitle_MouseUp(object sender, MouseEventArgs e)
		{
			_isMouseDown = false;
		}
	}
}
