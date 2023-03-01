﻿using Quoter.App.Helpers;
using Quoter.App.Helpers.Extensions;
using Quoter.App.Models;
using Quoter.Framework.Services.DependencyInjection;

namespace Quoter.App.Services.Forms
{
	/// <summary>
	/// Default implementation of <see cref="IFormsManager"/>
	/// </summary>
	public class FormsManager : IFormsManager
	{
		private readonly List<FormStateModel> _lstOpenedForms;
		private readonly DependencyInjectionContainer _diContainer;
		private readonly IFormLifecycleService _lifecycleService;

		public FormsManager(DependencyInjectionContainer diContainer, IFormLifecycleService lifecycleService)
		{
			_diContainer = diContainer;
			_lifecycleService = lifecycleService;
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
				form.Show();
			}
			else
			{
				formState.Form.TopMost = true;
			}
		}

		/// <inheritdoc/>
		public IDialogReturnable ShowDialog<TForm>(params object[] arrParameters) where TForm : Form, IDialogReturnable
		{
			Form form = _diContainer.GetService<TForm>(arrParameters);
			_lstOpenedForms.Add(new FormStateModel(typeof(TForm), form, true));
			form.ShowDialog();

			IDialogReturnable dialogReturnable = form as IDialogReturnable;
			DialogReturnable result = new(dialogReturnable.DialogResult, dialogReturnable.StringResult);
			if (!form.IsDisposed)
			{
				form.Dispose();
			}
			return result;
		}

		/// <inheritdoc/>
		public void ShowDialog<TForm>(int autoCloseSeconds, params object[] arrParameters) where TForm : Form, IMonitoredForm
		{
			try
			{
				Form form = _diContainer.GetService<TForm>(arrParameters);
				_lstOpenedForms.Add(new FormStateModel(typeof(TForm), form, true));

				if (autoCloseSeconds > 0)
				{
					_lifecycleService.CloseDelayed((IMonitoredForm)form, autoCloseSeconds);
				}

				form.ShowDialog();

				if (!form.IsDisposed)
				{
					form.Dispose();
				}
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		/// <inheritdoc/>
		public void Close(Form form)
		{
			FormStateModel? formState = _lstOpenedForms.FirstOrDefault(f => f.Form == form);
			if (formState is not null)
			{
				form.InvokeIfRequired(() =>
				{
					form.Close();
				});
				_lstOpenedForms.Remove(formState);
			}
			else
			{
				throw new ArgumentException($"Form to close not opened with FormsManager: {form.GetType()}");
			}
		}

		/// <inheritdoc/>
		public bool IsOpen<TForm>() where TForm : Form
		{
			return _lstOpenedForms.Any(f => f.Type == typeof(TForm));
		}

	}
}
