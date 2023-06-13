namespace Quoter.App.Services.BackgroundJobs
{
	/// <summary>
	/// Simple background job service.
	/// </summary>
	public interface IBackgroundJobsFormsService
	{
		/// <summary>
		/// Start processing periodically background jobs.
		/// </summary>
		void Start();

		/// <summary>
		/// Enqueue a background job.
		/// </summary>
		void Enqueue(Func<Task> task, string jobName);
	}
}
