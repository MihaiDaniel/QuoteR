namespace Quoter.App.Services.Forms
{
	/// <summary>
	/// Interface for services that monitor form states or events
	/// </summary>
	public interface IFormMonitor
	{
		/// <summary>
		/// A monitored form should call this methof to alert any monitoring forms when it's closing
		/// by a user event
		/// </summary>
		/// <param name="form">The form itself</param>
		void EventFormClosing(IMonitoredForm form);
	}
}
