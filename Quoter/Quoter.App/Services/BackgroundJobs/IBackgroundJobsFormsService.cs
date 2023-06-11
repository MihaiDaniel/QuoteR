namespace Quoter.App.Services.BackgroundJobs
{
	public interface IBackgroundJobsFormsService
	{
		void Start();

		void Enqueue(Func<Task> task, string name);
	}
}
