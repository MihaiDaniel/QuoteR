using Quoter.Framework.Enums;
using Quoter.Framework.Services;

namespace Quoter.App.Services.Forms
{
	/// <summary>
	/// Class responsible for monitoring an open form and closing it after a set period of time
	/// </summary>
	public class FormLifecycleService : IFormLifecycleService
	{
		private readonly object _lock = new object();

		private readonly ILogger _logger;
		private readonly Dictionary<IMonitoredForm, DateTime> _dicMonitoredForms;

		public FormLifecycleService(ILogger logger)
		{
			_logger = logger;
			_dicMonitoredForms = new Dictionary<IMonitoredForm, DateTime>();

			System.Timers.Timer timer = new System.Timers.Timer(1000);
			timer.AutoReset = true;
			timer.Elapsed += (sender, e) => ElapsedTimerEvent();
			timer.Start();
		}


		void IFormLifecycleService.CloseDelayed(IMonitoredForm form, int seconds)
		{
			_logger.Debug($"Monitoring {form.GetType()} for {seconds} seconds");
			lock (_lock)
			{
				_dicMonitoredForms.Add(form, DateTime.Now.AddSeconds(seconds));
			}
		}

		void IFormMonitor.EventFormClosing(IMonitoredForm form)
		{
			_logger.Debug($"{form.GetType()}");
			lock (_lock)
			{
				
				_dicMonitoredForms.Remove(form);
			}
		}

		private void ElapsedTimerEvent()
		{
			lock (_lock)
			{
				IEnumerable<IMonitoredForm> lstFormsToClose = _dicMonitoredForms.Where(d => d.Value <= DateTime.Now).Select(d => d.Key);
				foreach (IMonitoredForm form in lstFormsToClose)
				{
					TryCloseForm(form);
				}
			}
		}

		private void TryCloseForm(IMonitoredForm form)
		{
			_logger.Debug($"{form.GetType()}");
			EnumFormCloseState state = form.IsClosable();
			switch (state)
			{
				case EnumFormCloseState.IsClosable:
					_dicMonitoredForms.Remove(form);
					form.Close();
					break;
				case EnumFormCloseState.NotClosable:
					// Nothing to do, just delay the close
					_dicMonitoredForms[form].AddSeconds(2);
					break;
				case EnumFormCloseState.Disposed:
					_dicMonitoredForms.Remove(form);
					break;
			}
		}
	}
}
