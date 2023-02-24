﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Services.Forms
{
	public interface IFormLifecycleService : IFormMonitor
	{
		void CloseDelayed(IMonitoredForm form, int seconds);
	}
}
