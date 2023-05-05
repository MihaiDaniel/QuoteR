using Quoter.Framework.Models;

namespace Quoter.App.Forms.Quote
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
