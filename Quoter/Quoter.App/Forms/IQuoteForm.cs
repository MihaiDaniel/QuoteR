using Quoter.Framework.Models;

namespace Quoter.App.Forms
{
	/// <summary>
	/// Interface for the Quote form.
	/// Implementation: <see cref="QuoteForm"/>
	/// </summary>
	public interface IQuoteForm : IForm
	{
		void Close();
		Form GetForm();
		void SetQuote(QuoteFormOptions quoteModel);

	}
}
