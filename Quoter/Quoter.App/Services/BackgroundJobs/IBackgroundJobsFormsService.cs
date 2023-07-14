namespace Quoter.App.Services.BackgroundJobs
{
	/// <summary>
	/// Simple background job service. This will periodically run any tasks enqueued.
	/// </summary>
	public interface IBackgroundJobsFormsService
	{
		/// <summary>
		/// Enqueue a background job that will run only once.
		/// </summary>
		void Enqueue(Func<Task> task, string jobName);
	}
}
