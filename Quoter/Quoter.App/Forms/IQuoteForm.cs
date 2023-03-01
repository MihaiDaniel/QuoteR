using Quoter.App.Helpers;
using Quoter.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Forms
{
	public interface IQuoteForm
	{
		void Close();
		Form GetForm();
		void SetQuote(QuoteModel quoteModel);
		void SetTheme(Theme theme);
	}
}
