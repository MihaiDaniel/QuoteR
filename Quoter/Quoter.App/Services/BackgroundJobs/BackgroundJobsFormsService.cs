﻿using Quoter.Framework.Services;

namespace Quoter.App.Services.BackgroundJobs
{
	/// <summary>
	/// Simple implementation of a background jobs handler. This will run on a Windows.Forms timer
	/// so we can update the UI if needed when a job finishes. Will periodically run and execute all jobs 
	/// that are currently in queue.
	/// </summary>
	public class BackgroundJobsFormsService : IBackgroundJobsFormsService
	{
		private readonly ILogger _logger;

		private object _lock = new object();
		private bool _isRunningJobs;
		private System.Windows.Forms.Timer _timerBackgroundJobs;

		private Queue<KeyValuePair<string, Func<Task>>> _queueJobs;

		public BackgroundJobsFormsService(ILogger logger)
		{
			_logger = logger;

			_timerBackgroundJobs = new System.Windows.Forms.Timer();
			_timerBackgroundJobs.Interval = 10000; // 10 sec
			_timerBackgroundJobs.Tick += (sender, e) => EventBackgroundJobTimerTick(sender, e);
			_queueJobs = new Queue<KeyValuePair<string, Func<Task>>>();
			_isRunningJobs = false;
		}

		public void Enqueue(Func<Task> job, string jobName)
		{
			lock (_lock)
			{
				_queueJobs.Enqueue(new KeyValuePair<string, Func<Task>>(jobName, job));
			}
		}

		public void Start()
		{
			_timerBackgroundJobs.Start();
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
				int maxJobs = 10; // Just for safety to have a counter so we can exist the while.
				while(_queueJobs.Any())
				{
					if (IsMaxJobsExceeded(ref maxJobs))
					{
						break;
					}
					KeyValuePair<string, Func<Task>> kvpJob;
					lock (_lock)
					{
						kvpJob = _queueJobs.Dequeue();
					}
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
				_logger.Error(ex, $"An error occured while processing background job {jobName}.");
			}
		}

		private bool IsMaxJobsExceeded(ref int maxTasks)
		{
			if (maxTasks == 0)
			{
				return true;
			}
			else
			{
				maxTasks--;
				return false;
			}
		}
	}
}
