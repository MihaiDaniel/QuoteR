using Quoter.App.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.FormsControllers
{
	public interface IQuoteFormController
	{
		string Title { get; }
		string Body { get; }
		string Footer { get; } 

		void RegisterForm(IQuoteForm quoteForm);
		Task GetNextQuote(long currentQuoteId);
		Task GetPreviousQuote(long currentQuoteId);
	}
}
