using Quoter.App.Forms;
using Quoter.Framework.Models;

namespace Quoter.App.FormsControllers.QuoteController
{
	public interface IQuoteFormController : IFormController<IQuoteForm>
	{
		void RegisterForm(IQuoteForm form, QuoteFormOptions quoteModel);

		Task GetRandomQuoteAsync();
		Task GetNextQuoteAsync();
		Task GetPreviousQuoteAsync();
		Task OpenReaderForm();
	}
}
