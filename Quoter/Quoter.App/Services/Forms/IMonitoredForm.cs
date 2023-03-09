using Quoter.Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Services.Forms
{
	public interface IMonitoredForm
	{
		void RegisterFormMonitor(IFormMonitor formMonitor);

		EnumFormCloseState IsClosable();

		void Close();
	}
}
