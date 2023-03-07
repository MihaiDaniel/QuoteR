using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Services
{
	public interface ILogger
	{
		void Error(Exception ex, string? message = default);

		void Warn(string message);

		void Info(string message);

		void Debug(string message);
	}
}
