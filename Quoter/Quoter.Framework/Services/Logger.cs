using Quoter.Framework.Data;
using Quoter.Framework.Entities;
using System.Diagnostics;
using System.Text;

namespace Quoter.Framework.Services
{
	public class Logger : ILogger
	{
		private readonly QuoterContext _context;
		private readonly StringBuilder _stringBuilder;

		public Logger(QuoterContext context)
		{
			_context = context;
			_stringBuilder = new StringBuilder();
		}

		public void Error(Exception ex, string? message = default)
		{
			System.Diagnostics.Debug.WriteLine($"{DateTime.Now:G} {ex.Message}");
			_stringBuilder.Clear();
			if(message != default)
			{
				_stringBuilder.Append(message + Environment.NewLine);
			}
			
			_stringBuilder.Append(GetExceptionContent(ex));
			_context.Logs.Add(new Log()
			{
				DateTime = DateTime.Now,
				LogLevel = Enums.EnumLogLevel.Error,
				Message = _stringBuilder.ToString()
			});
			_context.SaveChanges();
		}

		private string GetExceptionContent(Exception ex)
		{
			if(ex.InnerException != null)
			{
				_stringBuilder.Append(GetExceptionContent(ex));
			}
			_stringBuilder.Append(ex.Message);
			_stringBuilder.Append(Environment.NewLine);
			_stringBuilder.Append(ex.Source);
			_stringBuilder.Append(Environment.NewLine);
			_stringBuilder.Append(ex.StackTrace);
			_stringBuilder.Append(Environment.NewLine);
			return _stringBuilder.ToString();
		}

		public void Info(string message)
		{
			System.Diagnostics.Debug.WriteLine($"{DateTime.Now:G} {message}");
			_context.Logs.Add(new Log()
			{
				DateTime = DateTime.Now,
				LogLevel = Enums.EnumLogLevel.Info,
				Message = message
			});
			_context.SaveChanges();
		}

		public void Warn(string message)
		{
			System.Diagnostics.Debug.WriteLine($"{DateTime.Now:G} {message}");
			_context.Logs.Add(new Log()
			{
				DateTime = DateTime.Now,
				LogLevel = Enums.EnumLogLevel.Warn,
				Message = message
			});
			_context.SaveChanges();
		}

		public void Debug(string message)
		{
			System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss,FFF")} Thread:{Thread.CurrentThread.ManagedThreadId} {message}");
#if DEBUG
			_context.Logs.Add(new Log()
			{
				DateTime = DateTime.Now,
				LogLevel = Enums.EnumLogLevel.Debug,
				Message = message
			});
			_context.SaveChanges();
#endif
		}
	}
}
