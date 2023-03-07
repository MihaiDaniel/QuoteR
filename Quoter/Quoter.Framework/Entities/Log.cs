using Quoter.Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Entities
{
	public class Log
	{
		public int LogId { get; set; }

		public EnumLogLevel LogLevel { get; set; }

		public string Message { get; set; }

		public DateTime DateTime { get; set; }
	}
}
