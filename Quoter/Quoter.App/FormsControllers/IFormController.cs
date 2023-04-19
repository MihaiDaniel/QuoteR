namespace Quoter.App.FormsControllers
{
	public interface IFormController<in TForm>
	{
		/// <summary>
		/// Mandatory step before using other methods.
		/// Due to DI we would have a circular dependency between the 
		/// controller and the form, so we use this method to avoid this.
		/// </summary>
		void RegisterForm(TForm form);

		/// <summary>
		/// Event that should fire when the form is loaded
		/// </summary>
		Task EventFormLoadedAsync();

		/// <summary>
		/// Event that should fire when the form's Close() method is called
		/// </summary>
		Task EventFormClosingAsync();
	}
}
