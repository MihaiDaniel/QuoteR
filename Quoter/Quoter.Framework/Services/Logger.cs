using Quoter.Framework.Data;
using Quoter.Framework.Data.Entities;
using System.Runtime.CompilerServices;
using System.Text;

namespace Quoter.Framework.Services
{
	/// <summary>
	/// Just a simple logger, nothing fancy or complex
	/// </summary>
	public class Logger : ILogger
	{
		/// <summary>
		/// Although this normally will be registered as transient, if we use it in a singleton instance,
		/// we might have concurrency issues on dbContext. So to be safe we lock all the dbContext operations.
		/// Update: No longer necessary, as dbContext is transient, no longer singleton
		/// </summary>
		private static object _lock = new object();

		private readonly QuoterContext _context;
		private readonly StringBuilder _stringBuilder;

		public Logger(QuoterContext context)
		{
			_context = context;
			_stringBuilder = new StringBuilder();
			System.Diagnostics.Debug.WriteLine($"Logger Context: {_context.InstanceID}");
		}

		public void Error(Exception ex, string? message = default)
		{
			System.Diagnostics.Debug.WriteLine($"{DateTime.Now:G} {ex.Message}");
			_stringBuilder.Clear();
			if (message != default)
			{
				_stringBuilder.Append(message + Environment.NewLine);
			}

			_stringBuilder.Append(GetExceptionContent(ex));
			lock (_lock)
			{
				_context.Logs.Add(new Log()
				{
					DateTime = DateTime.Now,
					LogLevel = Enums.EnumLogLevel.Error,
					Message = _stringBuilder.ToString()
				});
				_context.SaveChanges();
			}
		}

		public void Error(string message)
		{
			System.Diagnostics.Debug.WriteLine($"{DateTime.Now:G} {message}");

			lock (_lock)
			{
				_context.Logs.Add(new Log()
				{
					DateTime = DateTime.Now,
					LogLevel = Enums.EnumLogLevel.Error,
					Message = message
				});
				_context.SaveChanges();
			}
		}

		private string GetExceptionContent(Exception ex)
		{
			if (ex.InnerException != null)
			{
				_stringBuilder.Append(GetExceptionContent(ex.InnerException));
			}
			_stringBuilder.Append(ex.Message);
			_stringBuilder.Append(Environment.NewLine);
			_stringBuilder.Append(ex.Source);
			_stringBuilder.Append(Environment.NewLine);
			_stringBuilder.Append(ex.TargetSite);
			_stringBuilder.Append(Environment.NewLine);
			_stringBuilder.Append(ex.StackTrace);
			_stringBuilder.Append(Environment.NewLine);
			return _stringBuilder.ToString();
		}

		public void Info(string message)
		{
			System.Diagnostics.Debug.WriteLine($"{DateTime.Now:G} {message}");
			lock (_lock)
			{
				_context.Logs.Add(new Log()
				{
					DateTime = DateTime.Now,
					LogLevel = Enums.EnumLogLevel.Info,
					Message = message
				});
				_context.SaveChanges();
			}
		}

		public void Warn(string message)
		{
			System.Diagnostics.Debug.WriteLine($"{DateTime.Now:G} {message}");
			lock (_lock)
			{
				_context.Logs.Add(new Log()
				{
					DateTime = DateTime.Now,
					LogLevel = Enums.EnumLogLevel.Warn,
					Message = message
				});
				_context.SaveChanges();
			}
		}

		public void Debug(string message, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
		{
#if DEBUG
			string callerFile = Path.GetFileNameWithoutExtension(callerFilePath);
			string dateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss,FFF");
			string thread = Thread.CurrentThread.ManagedThreadId.ToString();

			string fullMessage = $"{dateTime} Th:{thread} {callerFile}.{callerMemberName} {message}";
			System.Diagnostics.Debug.WriteLine(fullMessage);

			lock (_lock)
			{
				_context.Logs.Add(new Log()
				{
					DateTime = DateTime.Now,
					LogLevel = Enums.EnumLogLevel.Debug,
					Message = $"{callerFile}.{callerMemberName} [Thread:{thread}] {message}"
				});
				_context.SaveChanges();

			}
#endif
		}

	}
}
