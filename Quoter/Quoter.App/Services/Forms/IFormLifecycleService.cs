using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Services.Forms
{
	/// <summary>
	/// Interface for handling the lifecycle of forms
	/// </summary>
	public interface IFormLifecycleService : IFormMonitor
	{
		/// <summary>
		/// Close a <see cref="IMonitoredForm"/> after a number of seconds. Starts a timer
		/// and tries to close the form if it's closable after the expired period.
		/// </summary>
		void CloseDelayed(IMonitoredForm form, int seconds);
	}
}
