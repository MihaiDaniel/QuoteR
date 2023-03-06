using Quoter.App.Forms;
using Quoter.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.FormsControllers
{
	public interface IQuoteFormController : IFormController<IQuoteForm>
	{
		void RegisterForm(IQuoteForm form, QuoteFormOptions quoteModel);

		Task GetRandomQuote();
		Task GetNextQuote();
		Task GetPreviousQuote();
	}
}
