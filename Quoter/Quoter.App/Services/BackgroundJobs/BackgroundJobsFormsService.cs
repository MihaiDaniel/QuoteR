using Quoter.Framework.Services;

namespace Quoter.App.Services.BackgroundJobs
{
	public class BackgroundJobsFormsService : IBackgroundJobsFormsService
	{
		private readonly ILogger _logger;

		private object _lock = new object();
		private System.Windows.Forms.Timer _timerBackgroundJobs;

		private Queue<KeyValuePair<string, Func<Task>>> _queueActions;

		public BackgroundJobsFormsService(ILogger logger)
		{
			_logger = logger;

			_timerBackgroundJobs = new System.Windows.Forms.Timer();
			_timerBackgroundJobs.Interval = 1000; // 1 minute
			_timerBackgroundJobs.Tick += (sender, e) => EventBackgroundJobTimerTick(sender, e);
			_queueActions = new Queue<KeyValuePair<string, Func<Task>>>();
		}

		public void Enqueue(Func<Task> task, string name)
		{
			lock (_lock)
			{
				_queueActions.Enqueue(new KeyValuePair<string, Func<Task>>(name, task));
			}
		}

		public void Start()
		{
			_timerBackgroundJobs.Start();
		}

		private async void EventBackgroundJobTimerTick(object sender, EventArgs e)
		{
			string taskName = string.Empty;
			try
			{
				int maxTasks = 10;
				while(_queueActions.Any())
				{
					if (!CanRunTasks(ref maxTasks))
					{
						break;
					}
					KeyValuePair<string, Func<Task>> kvpTask;
					lock (_lock)
					{
						kvpTask = _queueActions.Dequeue();
					}
					_logger.Debug($"Running background job: {kvpTask.Key}");
					taskName = kvpTask.Key;
					await kvpTask.Value();
				}
			}
			catch(Exception ex)
			{
				_logger.Error(ex, $"An error occured while processing background job {taskName}.");
			}
		}

		private bool CanRunTasks(ref int maxTasks)
		{
			if (maxTasks == 0)
			{
				return false;
			}
			else
			{
				maxTasks--;
				return true;
			}
		}
	}
}
