using Quoter.Framework.Services;
using System.Collections.Concurrent;

namespace Quoter.App.Services.BackgroundJobs
{
	/// <summary>
	/// Simple implementation of a background jobs handler. This will run on a Windows.Forms timer
	/// so we can update the UI if needed when a job finishes. Will periodically run and execute all jobs 
	/// that are currently in queue.
	/// </summary>
	/// <remarks>
	/// This uses the <see cref="System.Windows.Forms.Timer"/> for periodically running jobs
	/// </remarks>
	public class BackgroundJobsFormsService : IBackgroundJobsFormsService
	{
		private readonly ILogger _logger;

		private bool _isRunningJobs;
		private System.Windows.Forms.Timer _timerBackgroundJobs;
		private ConcurrentQueue<KeyValuePair<string, Func<Task>>> _queueJobs;

		public BackgroundJobsFormsService(ILogger logger)
		{
			_logger = logger;
			_queueJobs = new ConcurrentQueue<KeyValuePair<string, Func<Task>>>();
			
			_isRunningJobs = false;

			_timerBackgroundJobs = new System.Windows.Forms.Timer();
			_timerBackgroundJobs.Interval = 10000; // 10 sec by default
			_timerBackgroundJobs.Tick += (sender, e) => EventBackgroundJobTimerTick(sender, e);
			_timerBackgroundJobs.Start();
		}

		public void Enqueue(Func<Task> job, string jobName)
		{
			_queueJobs.Enqueue(new KeyValuePair<string, Func<Task>>(jobName, job));
		}

		private async void EventBackgroundJobTimerTick(object sender, EventArgs e)
		{
			if( _isRunningJobs)
			{
				return;
			}
			try
			{
				_isRunningJobs = true;
				int maxJobsThisRun = 10; // Just for safety to have a counter so we can exist the while.
				while(_queueJobs.Any())
				{
					if (IsMaxJobsExceededForCurrentRun(ref maxJobsThisRun))
					{
						break;
					}
					_queueJobs.TryDequeue(out KeyValuePair<string, Func<Task>>  kvpJob);
					await TryRunJob(kvpJob.Key, kvpJob.Value);
				}
			}
			catch(Exception ex)
			{
				_logger.Error(ex, $"An error occured while processing background jobs");
			}
			finally
			{
				_isRunningJobs = false;
			}
		}

		private async Task TryRunJob(string jobName, Func<Task> job)
		{
			try
			{
				_logger.Debug($"Starting background job: {jobName}");
				await job();
				_logger.Debug($"Finished background job: {jobName}");
			}
			catch (Exception ex)
			{
				_logger.Error(ex, $"An error occured while processing background job: {jobName}.");
			}
		}

		private bool IsMaxJobsExceededForCurrentRun(ref int maxJobsThisRun)
		{
			if (maxJobsThisRun == 0)
			{
				return true;
			}
			else
			{
				maxJobsThisRun--;
				return false;
			}
		}
	}
}
