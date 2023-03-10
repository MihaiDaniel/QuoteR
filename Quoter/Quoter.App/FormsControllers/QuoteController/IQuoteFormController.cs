using Quoter.App.Forms;
using Quoter.Framework.Models;

namespace Quoter.App.FormsControllers.QuoteController
{
	public interface IQuoteFormController : IFormController<IQuoteForm>
	{
		void RegisterForm(IQuoteForm form, QuoteFormOptions quoteModel);

		Task GetRandomQuote();
		Task GetNextQuote();
		Task GetPreviousQuote();
	}
}
