using Quoter.Framework.Enums;

namespace Quoter.App.Services.FormAnimation
{
	public interface IFormAnimationService
	{
		/// <summary>
		/// Animate the form with one of the available <see cref="EnumAnimation"/>
		/// </summary>
		Task AnimateAsync(Form form, EnumAnimation enumAnimation);

		/// <summary>
		/// Animation used for the status of the ManageForm.
		/// Makes the status go up, change, then appear from the bottom.
		/// Use the <paramref name="action"/> to apply changes to the status midway through
		/// the animation
		/// </summary>
		Task AnimateStatusAsync(Control control, Action action);
	}
}
