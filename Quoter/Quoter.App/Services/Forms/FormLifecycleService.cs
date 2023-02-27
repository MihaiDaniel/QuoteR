using Quoter.Framework.Enums;

namespace Quoter.App.Services.Forms
{
	public class FormLifecycleService : IFormLifecycleService
	{
		class MonitoredForm
		{
			public IMonitoredForm Form { get; set; }
			public System.Timers.Timer Timer { get; set; }

			public MonitoredForm(IMonitoredForm monitoredForm, System.Timers.Timer timer)
			{
				Form = monitoredForm;
				Timer = timer;
			}
		}

		private readonly List<MonitoredForm> _monitoredForms;

		public FormLifecycleService()
		{
			_monitoredForms = new List<MonitoredForm>();
		}

		public void CloseDelayed(IMonitoredForm form, int seconds)
		{
			System.Timers.Timer timerCloseDelay = new(seconds * 1000);
			timerCloseDelay.AutoReset = true;
			MonitoredForm monitoredForm = new(form, timerCloseDelay);
			timerCloseDelay.Elapsed += (sender, e) => ElapsedTimerEventCloseDelayed(monitoredForm);
			timerCloseDelay.Start();

			_monitoredForms.Add(monitoredForm);
		}

		public void EventFormClosing(IMonitoredForm form)
		{
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
			EnumFormCloseState state = monitoredForm.Form.CanClose();
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
