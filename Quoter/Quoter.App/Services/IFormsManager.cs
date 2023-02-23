﻿namespace Quoter.App.Services
{
	/// <summary>
	/// Interface for handling forms and their opened or closed states.
	/// </summary>
	public interface IFormsManager
	{
		/// <summary>
		/// Shows a form non modally. Closes any other opened non-modal form.
		/// </summary>
		/// <typeparam name="TForm">Type of form to show</typeparam>
		void Show<TForm>() where TForm : Form;

		/// <summary>
		/// Shows a form modally as a dialog. Allows for opening of multiple forms of same type.
		/// </summary>
		/// <typeparam name="TForm">Type of form to show</typeparam>
		/// <param name="arrParameters">Optional parameters to pass to the form's constructor if needed</param>
		void ShowDialog<TForm>(params object[] arrParameters) where TForm : Form;

		/// <summary>
		/// Closes a form opened with the formsManager
		/// </summary>
		/// <param name="form">Form to close</param>
		void Close(Form form);

	}
}
