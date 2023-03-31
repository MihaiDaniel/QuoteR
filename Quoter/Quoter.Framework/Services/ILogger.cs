using System.Runtime.CompilerServices;

namespace Quoter.Framework.Services
{
	public interface ILogger
	{
		void Error(Exception ex, string? message = default);

		void Warn(string message);

		void Info(string message);

		void Debug(string message, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "");
	}
}
