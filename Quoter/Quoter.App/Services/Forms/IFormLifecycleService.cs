namespace Quoter.App.Services.Forms
{
	/// <summary>
	/// Interface for handling the lifecycle of forms, it is used to automatically close a form
	/// after a period of time
	/// </summary>
	/// <remarks>
	/// <see cref="FormLifecycleService"/>
	/// </remarks>
	public interface IFormLifecycleService : IFormMonitor
	{
		/// <summary>
		/// Close a <see cref="IMonitoredForm"/> after a number of seconds.
		/// Tries to close the form if it's closable after the expired period.
		/// If form is not closable it delays the close time (by 2 sec) untill it's closable
		/// </summary>
		void CloseDelayed(IMonitoredForm form, int seconds);
	}
}
