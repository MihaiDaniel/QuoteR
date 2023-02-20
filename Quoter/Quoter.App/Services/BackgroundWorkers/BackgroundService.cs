using Quoter.App.Helpers.Extensions;
using Quoter.App.Views;
using Quoter.Framework.Enums;
using Quoter.Framework.Models;
using Quoter.Framework.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Services.BackgroundWorkers
{
	public class BackgroundService
	{
		private System.Timers.Timer _timerNotifications;
		private System.Timers.Timer _timerScanner;

		private object _lock = new object();

		private bool __isScanInProgress = false;
		private bool _isScanInProgress
		{
			get
			{
				lock (_lock)
				{
					return __isScanInProgress;
				}
			}
			set
			{
				lock (_lock)
				{
					__isScanInProgress = value;
				}
			}
		}

		private bool __isShowingNotification = false;
		private bool _isShowingNotification
		{
			get
			{
				lock (_lock)
				{
					return __isShowingNotification;
				}
			}
			set
			{
				lock (_lock)
				{
					__isShowingNotification = value;
				}
			}
		}

		private readonly ConcurrentBag<QuoteModel> _concurrentBagQuotes;
		private readonly Dictionary<string, DateTime> _filesScanned;
		private readonly IFileReader _fileReader;

		public BackgroundService()
		{
			_fileReader = new FileReader();
			_concurrentBagQuotes = new ConcurrentBag<QuoteModel>();
			_filesScanned = new Dictionary<string, DateTime>();
		}

		public async Task Start()
		{
			Debug.WriteLine($"Start BackgroundService.{nameof(Start)} Thread: {Thread.CurrentThread.ManagedThreadId}");
			int notificationIntervalSec = (int)Properties.Settings.Default["NotificationIntervalSeconds"];
			int scanFolderIntervalSec = (int)Properties.Settings.Default["ScanFolderInterval"];

			if (_timerNotifications is not null && _timerNotifications.Enabled)
			{
				_timerNotifications.Stop();
				_timerNotifications.Dispose();
			}
			_timerNotifications = new(notificationIntervalSec * 1000);
			_timerNotifications.Elapsed += async (sender, e) => await ElapsedTimerEventNotifications();
			_timerNotifications.Start();

			if (_timerScanner is not null && _timerScanner.Enabled)
			{
				_timerScanner.Stop();
				_timerScanner.Dispose();
			}
			_timerScanner = new(scanFolderIntervalSec * 1000);
			_timerScanner.Elapsed += async (sender, e) => await ScanWorkDirectory();
			_timerScanner.Start();
			await ScanWorkDirectory();
		}

		private async Task ElapsedTimerEventNotifications()
		{
			Debug.WriteLine($"Start {nameof(ElapsedTimerEventNotifications)} Thread: {Thread.CurrentThread.ManagedThreadId}");
			if (_isShowingNotification)
			{
				return;
			}
			try
			{
				bool isPaused = (bool)Properties.Settings.Default["IsPaused"];
				if (isPaused)
				{
					return;
				}

				_isShowingNotification = true;
				QuoteModel? quote = null;
				int tryAttempt = 10;
				while (tryAttempt > 0 && quote is null)
				{
					_concurrentBagQuotes.TryTake(out quote);
					Thread.Sleep(10);
					tryAttempt--;
				}
				if (quote is null)
				{
					return;
				}

				MessageModel messageModel = new()
				{
					Title = quote.File,
					Body = quote.Body,
					Footer = quote.Chapter + ":" + quote.Subchapter
				};

				MessageForm messageForm = new(messageModel);
				//messageForm.InvokeIfRequired(() =>
				//{
				messageForm.ShowDialog();
				//});
				//await Task.Run(() => { messageForm.Show(); });
			}
			finally
			{
				_isShowingNotification = false;
			}
			Debug.WriteLine($"End {nameof(ElapsedTimerEventNotifications)} Thread: {Thread.CurrentThread.ManagedThreadId}");
		}

		private async Task ScanWorkDirectory()
		{
			//Debug.WriteLine($"Start {nameof(ScanWorkDirectory)} Thread: {Thread.CurrentThread.ManagedThreadId}");
			//if (_isScanInProgress)
			//{
			//	return;
			//}
			//try
			//{
			//	_isScanInProgress = true;
			//	string workFolder = Properties.Settings.Default["ScannedFolder"].ToString();
			//	if (!Directory.Exists(workFolder))
			//	{
			//		return;
			//	}

			//	//_concurrentBagQuotes.Clear();

			//	List<string> lstFileName = Directory.GetFiles(workFolder).ToList();

			//	foreach (string fileName in lstFileName)
			//	{
			//		bool canScanFile = false;
			//		string path = Path.Combine(workFolder, fileName);
			//		DateTime lastWriteDateTime = File.GetLastWriteTime(path);

			//		if (_filesScanned.ContainsKey(path))
			//		{
			//			if (_filesScanned[path] < lastWriteDateTime)
			//			{
			//				canScanFile = true;
			//				_filesScanned[path] = lastWriteDateTime;
			//			};
			//		}
			//		else
			//		{
			//			canScanFile = true;
			//			_filesScanned.Add(path, lastWriteDateTime);
			//		}
			//		// Force rescan because we take out of the bag quotes so we can refill the bag
			//		if (canScanFile == false && _concurrentBagQuotes.Count == 0)
			//		{
			//			canScanFile = true;
			//		}

			//		if (canScanFile)
			//		{
			//			List<QuoteModel> lstQuote = await _fileReader.ReadAsync(path);
			//			foreach (QuoteModel quote in lstQuote)
			//			{
			//				_concurrentBagQuotes.Add(quote);
			//			}
			//		}
			//	}
			//}
			//catch (Exception ex)
			//{

			//}
			//finally
			//{
			//	_isScanInProgress = false;
			//}

		}
	}
}
