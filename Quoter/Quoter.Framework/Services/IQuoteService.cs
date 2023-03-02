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
		Task<QuoteModel?> GetRandomQuote();

		Task<QuoteModel?> GetNextQuote(long quoteId);

		Task<QuoteModel?> GetPreviousQuote(long quoteId);
	}
}
