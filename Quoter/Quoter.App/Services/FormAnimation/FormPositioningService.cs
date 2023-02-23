namespace Quoter.App.Services.FormAnimation
{
	public class FormPositioningService : IFormPositioningService
	{
		private bool _isMouseDown;
		private Point _lastLocation;
		private Form _form;

		/// <inheritdoc/>
		public void RegisterFormDragableByControl(Form form, Control control)
		{
			_form = form;
			control.MouseDown += new MouseEventHandler(control_MouseDown);
			control.MouseMove += new MouseEventHandler(control_MouseMove);
			control.MouseUp += new MouseEventHandler(control_MouseUp);
		}

		private void control_MouseDown(object sender, MouseEventArgs e)
		{
			_isMouseDown = true;
			_lastLocation = e.Location;
		}

		private void control_MouseMove(object sender, MouseEventArgs e)
		{
			if (_isMouseDown)
			{
				_form.Location = new Point((_form.Location.X - _lastLocation.X) + e.X, (_form.Location.Y - _lastLocation.Y) + e.Y);
				_form.Update();
			}
		}

		private void control_MouseUp(object sender, MouseEventArgs e)
		{
			_isMouseDown = false;
		}
	}
}
