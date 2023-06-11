using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Services.Api
{
	public interface IUpdateService
	{
		Task<bool> IsNewVersionAvailable();

		Task TryUpdate();
	}
}
