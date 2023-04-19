using Quoter.Framework.Enums;
using Quoter.Framework.Services;

namespace Quoter.App.Services.Forms
{
	/// <summary>
	/// Class responsible for monitoring an open form and closing it after a set period of time
	/// </summary>
	public class FormLifecycleService : IFormLifecycleService
	{
		class MonitoredForm
		{
			public Guid Id { get; set; }
			public IMonitoredForm Form { get; set; }
			public System.Timers.Timer Timer { get; set; }

			public MonitoredForm(IMonitoredForm monitoredForm, System.Timers.Timer timer)
			{
				Form = monitoredForm;
				Timer = timer;
				Id = Guid.NewGuid();
			}
		}

		private readonly List<MonitoredForm> _monitoredForms;
		private readonly ILogger _logger;

		public FormLifecycleService(ILogger logger)
		{
			_logger = logger;
			_monitoredForms = new List<MonitoredForm>();
		}

		public void CloseDelayed(IMonitoredForm form, int seconds)
		{
			_logger.Debug($"Monitoring for {seconds} seconds");

			System.Timers.Timer timerCloseDelay = new(seconds * 1000);
			timerCloseDelay.AutoReset = true;
			MonitoredForm monitoredForm = new(form, timerCloseDelay);
			
			timerCloseDelay.Elapsed += (sender, e) => ElapsedTimerEventCloseDelayed(monitoredForm);
			timerCloseDelay.Start();

			_monitoredForms.Add(monitoredForm);
		}

		public void EventFormClosing(IMonitoredForm form)
		{
			_logger.Debug($"Form is closing");

			MonitoredForm? monitoredForm = _monitoredForms.FirstOrDefault(m => m.Form == form);
			if (monitoredForm != null)
			{
				if (monitoredForm.Timer.Enabled)
				{
					monitoredForm.Timer.Stop();
					monitoredForm.Timer.Dispose();
					_monitoredForms.Remove(monitoredForm);
				}
			}
		}

		private void ElapsedTimerEventCloseDelayed(MonitoredForm monitoredForm)
		{
			_logger.Debug($"Try to autoclose form");

			EnumFormCloseState state = monitoredForm.Form.IsClosable();
			if (state == EnumFormCloseState.IsClosable)			// Can close
			{
				monitoredForm.Timer.Stop();
				monitoredForm.Timer.Dispose();
				_monitoredForms.Remove(monitoredForm);
				monitoredForm.Form.Close();
			}
			else if (state == EnumFormCloseState.NotClosable)	// Retry close every 1 second
			{
				monitoredForm.Timer.Interval = 1000; 
			}
			else												// Disposed
			{
				monitoredForm.Timer.Stop();
				monitoredForm.Timer.Dispose();
				_monitoredForms.Remove(monitoredForm);
			}
		}
	}
}
