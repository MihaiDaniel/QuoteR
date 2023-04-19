namespace Quoter.App.Services.Forms
{
	/// <summary>
	/// Interface for handling the lifecycle of forms, it is used to automatically close a form
	/// after a period of time
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
