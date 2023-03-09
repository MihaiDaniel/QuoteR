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
		public void Show<TForm>(params object[] arrParameters) where TForm : Form
		{
			_logger.Debug("FormsManager.Show() " + typeof(TForm));

			try
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
					Form form = _diContainer.GetService<TForm>(arrParameters);
					_lstOpenedForms.Add(new FormStateModel(formType, form, isModal: false));
					form.Show();
				}
				else
				{
					formState.Form.TopMost = true;
				}
				FormsManagerOptions options = new FormsManagerOptions()
				{
					Type= formType,
					Parameters = arrParameters
				};
				_messagingService.SendMessage(Event.OpeningForm, options);
			}
			catch(Exception ex)
			{
				_logger.Error(ex);
			}
		}

		/// <inheritdoc/>
		public IDialogReturnable ShowDialog<TForm>(params object[] arrParameters) where TForm : Form, IDialogReturnable
		{
			_logger.Debug("FormsManager.ShowDialog() " + typeof(TForm));
			try
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
			catch (Exception ex)
			{
				_logger.Error(ex);
				return new DialogReturnable(DialogResult.Cancel, ex.Message);
			}
		}

		/// <inheritdoc/>
		public void ShowDialog<TForm>(int autoCloseSeconds, params object[] arrParameters) where TForm : Form, IMonitoredForm
		{
			_logger.Debug($"FormsManager.ShowDialog() {typeof(TForm)} autoClose: {autoCloseSeconds}");

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
				_logger.Error(ex);
			}
		}

		/// <inheritdoc/>
		public void Close(Form form)
		{
			_logger.Debug($"FormsManager.Close() {form.GetType()}");
			try
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

	}
}
