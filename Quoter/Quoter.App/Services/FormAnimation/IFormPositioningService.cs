namespace Quoter.App.Services.FormAnimation
{
	/// <summary>
	/// Interface responsible for allowing modifications to the form position
	/// </summary>
	public interface IFormPositioningService
	{
		/// <summary>
		/// Allow the user to click and drag the form to another location on screen
		/// by clicking on a particular control
		/// </summary>
		/// <param name="form">Form on which this behaviour will be applied</param>
		/// <param name="control">Control on which the user can click to apply behaviour</param>
		void RegisterFormDragableByControl(Form form, Control control);
	}
}
