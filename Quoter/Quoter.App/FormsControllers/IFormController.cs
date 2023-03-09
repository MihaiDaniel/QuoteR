namespace Quoter.App.FormsControllers
{
	public interface IFormController<in IForm>
	{
		/// <summary>
		/// Mandatory step before using other methods.
		/// Due to DI we would have a circular dependency between the 
		/// controller and the form, so we use this method to avoid this.
		/// </summary>
		void RegisterForm(IForm form);

		/// <summary>
		/// Event that should fire when the form is loaded
		/// </summary>
		Task EventFormLoaded();

		/// <summary>
		/// Event that should fire when the form's Close() method is called
		/// </summary>
		Task EventFormClosing();
	}
}
