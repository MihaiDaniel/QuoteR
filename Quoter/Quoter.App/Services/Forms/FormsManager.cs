using Quoter.App.Helpers;
using Quoter.App.Helpers.Extensions;
using Quoter.App.Models;
using Quoter.Framework.Models;
using Quoter.Framework.Services;
using Quoter.Framework.Services.DependencyInjection;
using Quoter.Framework.Services.Messaging;
namespace Quoter.App.Services.Forms
{
	/// <summary>
	/// Handles opening and closing of forms. All forms should be opened using this class.
	/// Default implementation of <see cref="IFormsManager"/>
	/// </summary>
	public class FormsManager : IFormsManager
	{
		private readonly List<FormStateModel> _lstOpenedForms;
		private readonly DependencyInjectionContainer _diContainer;
		private readonly IFormLifecycleService _lifecycleService;
		private readonly IMessagingService _messagingService;
		private readonly ILogger _logger;

		public FormsManager(DependencyInjectionContainer diContainer, 
							IFormLifecycleService lifecycleService,
							ILogger logger,
							IMessagingService messagingService)
		{
			_diContainer = diContainer;
			_lifecycleService = lifecycleService;
			_logger = logger;
			_messagingService = messagingService;
			_lstOpenedForms = new List<FormStateModel>();
		}

		/// <inheritdoc/>
		public void ShowAndCloseOthers<TForm>(params object[] arrParameters) where TForm : Form
		{
			_logger.Debug(typeof(TForm).ToString());
			try
			{
				Type formType = typeof(TForm);
				FormStateModel? formState = _lstOpenedForms.FirstOrDefault(f => f.Type == formType);

				if (formState is null)
				{
					FormStateModel? currentForm = _lstOpenedForms.FirstOrDefault(f => !f.IsModal);

					// Open the new form
					Form newForm = _diContainer.GetService<TForm>(arrParameters);
					_lstOpenedForms.Add(new FormStateModel(formType, newForm, isModal: false));
					newForm.FormClosing += EventFormClosing;
					newForm.Show();

					// Close previous opened non-modal form if any
					if (currentForm is not null)
					{
						Close(currentForm.Form);
					}
				}
				else
				{
					formState.Form.InvokeIfRequired(() => formState.Form.TopMost = true);
				}
				SendMessageOpeningForm(formType, arrParameters);
			}
			catch(Exception ex)
			{
				_logger.Error(ex);
			}
		}

		/// <inheritdoc/>
		public IDialogReturnable ShowDialog<TForm>(params object[] arrParameters) where TForm : Form, IDialogReturnable
		{
			_logger.Debug(typeof(TForm).ToString());
			try
			{
				Form form = _diContainer.GetService<TForm>(arrParameters);
				_lstOpenedForms.Add(new FormStateModel(typeof(TForm), form, true));
				form.FormClosing += EventFormClosing;
				form.ShowDialog();

				IDialogReturnable dialogReturnable = form as IDialogReturnable;
				DialogReturnable result = new(dialogReturnable.DialogResult, dialogReturnable.StringResult);
				if (!form.IsDisposed)
				{
					// Dialog forms are not disposed, so we have to dispose of it manually:
					// see: https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.form.showdialog?view=windowsdesktop-8.0#system-windows-forms-form-showdialog
					form.Dispose();
				}
				return result;
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
				return new DialogReturnable(DialogResult.Cancel, ex.Message);
			}
		}

		/// <inheritdoc/>
		public void Show<TForm>(int autoCloseSeconds, params object[] arrParameters) where TForm : Form, IMonitoredForm
		{
			_logger.Debug($"{typeof(TForm)}, autoClose: {autoCloseSeconds}");
			try
			{
				Form form = _diContainer.GetService<TForm>(arrParameters);
				_lstOpenedForms.Add(new FormStateModel(typeof(TForm), form, true));

				if (autoCloseSeconds > 0)
				{
					_lifecycleService.CloseDelayed((IMonitoredForm)form, autoCloseSeconds);
				}
				form.FormClosing += EventFormClosing;
				form.Show();
			}
			catch(Exception ex)
			{
				_logger.Error(ex);
			}
		}

		/// <inheritdoc/>
		public void Close(Form form)
		{
			_logger.Debug($"{form.GetType()}");
			try
			{
				FormStateModel? formState = _lstOpenedForms.FirstOrDefault(f => f.Form == form);
				if (formState is not null)
				{
					form.InvokeIfRequired(() =>
					{
						if (!form.IsDisposed)
						{
							form.Close();
						}
					});
				}
				else
				{
					throw new ArgumentException($"Form to close not opened with FormsManager: {form.GetType()}");
				}
			}
			catch(Exception ex)
			{
				_logger.Error(ex);
			}
		}

		/// <inheritdoc/>
		public bool IsOpen<TForm>() where TForm : Form
		{
			return _lstOpenedForms.Any(f => f.Type == typeof(TForm));
		}

		private void SendMessageOpeningForm(Type formType, object[] arrParameters)
		{
			OpeningFormArgs openingFormArgs = new OpeningFormArgs()
			{
				Type = formType,
				Parameters = arrParameters
			};
			_messagingService.SendMessage(Event.OpeningForm, openingFormArgs);
		}

		/// <summary>
		/// Event handler that should be registered to the Form's close event, so we can remove
		/// it from the opened forms list
		/// </summary>
		private void EventFormClosing(object? sender, FormClosingEventArgs e)
		{
			// Normally sender should always be Form. Remove from the opened forms list
			if (sender is Form)
			{
				FormStateModel? formState = _lstOpenedForms.FirstOrDefault(f => f.Form == sender);
				if (formState != null)
				{
					_lstOpenedForms.Remove(formState);
				}
			}
			if(sender is IMonitoredForm)
			{
				_lifecycleService.EventFormClosing(sender as IMonitoredForm);
			}
		}

	}
}
