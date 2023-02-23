using Quoter.App.Models;
using Quoter.Framework.Services.DependencyInjection;

namespace Quoter.App.Services
{
	/// <summary>
	/// Default implementation of <see cref="IFormsManager"/>
	/// </summary>
	public class FormsManager : IFormsManager
	{
		private readonly List<FormStateModel> _lstOpenedForms;
		private readonly DependencyInjectionContainer _diContainer;

		public FormsManager(DependencyInjectionContainer diContainer)
		{
			_diContainer = diContainer;
			_lstOpenedForms = new List<FormStateModel>();
		}

		/// <inheritdoc/>
		public void Show<TForm>() where TForm : Form
		{
			Type formType = typeof(TForm);
			FormStateModel? formState = _lstOpenedForms.FirstOrDefault(f => f.Type == formType);

			if (formState is null)
			{
				// Close current opened non-modal form if any
				FormStateModel? currentForm = _lstOpenedForms.FirstOrDefault(f => !f.IsModal);
				if (currentForm is not null)
				{
					Close(currentForm.Form);
				}

				// Open the new form
				Form form = _diContainer.GetService<TForm>();
				_lstOpenedForms.Add(new FormStateModel(formType, form, isModal: false));
				ShowForm(form, false);
			}
			else
			{
				formState.Form.TopMost = true;
			}
		}

		/// <inheritdoc/>
		public void ShowDialog<TForm>(params object[] arrParameters) where TForm : Form
		{
			Form form = _diContainer.GetService<TForm>(arrParameters);
			_lstOpenedForms.Add(new FormStateModel(typeof(TForm), form, true));
			ShowForm(form, true);
		}

		/// <inheritdoc/>
		public void Close(Form form)
		{
			FormStateModel? formState = _lstOpenedForms.FirstOrDefault(f => f.Form == form);
			if (formState is not null)
			{
				CloseForm(formState.Form, formState.IsModal);
				_lstOpenedForms.Remove(formState);
			}
			else
			{
				throw new ArgumentException($"Form to close not opened with FormsManager: {form.GetType()}");
			}
		}

		private void ShowForm(Form form, bool isModal)
		{
			if (isModal)
			{
				form.ShowDialog();
			}
			else
			{
				form.Show();
			}
		}


		private void CloseForm(Form form, bool isModal)
		{
			if (isModal)
			{
				form.Close();
				form.Dispose();
			}
			else
			{
				form.Close();
			}
		}

	}
}
