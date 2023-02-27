using Quoter.Framework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Forms
{
	public interface IEditQuotesForm
	{
		void SetStatus(string message, Color color);
	}
}
