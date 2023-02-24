using Quoter.App.Helpers.Extensions;
using Quoter.Framework.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			if (monitoredForm.Form.CanClose())
			{
				monitoredForm.Timer.Stop();
				monitoredForm.Timer.Dispose();
				_monitoredForms.Remove(monitoredForm);
				monitoredForm.Form.Close();
			}
			else
			{
				monitoredForm.Timer.Interval = 1000; // retry close every 1 second
			}
		}
	}
}
