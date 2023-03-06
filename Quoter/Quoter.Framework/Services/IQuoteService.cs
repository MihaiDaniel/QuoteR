using Quoter.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Services
{
	/// <summary>
	/// Interface for a service responsible for getting quotes to be shown to the user
	/// </summary>
	public interface IQuoteService
	{
		Task<QuoteFormOptions?> GetRandomQuote();

		Task<QuoteFormOptions?> GetNextQuote(long quoteId);

		Task<QuoteFormOptions?> GetPreviousQuote(long quoteId);
	}
}
