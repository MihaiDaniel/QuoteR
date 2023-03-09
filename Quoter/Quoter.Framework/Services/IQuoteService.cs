using Quoter.Framework.Entities;
using Quoter.Framework.Enums;
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
		/// <summary>
		/// Returns a random quote from any of the collections marked as favourites. If <paramref name="language"/> is specified (any other than None)
		/// it will also filter for quotes where the parent collection has the specified language
		/// </summary>
		/// <param name="language"></param>
		/// <returns></returns>
		Task<Quote?> GetRandomQuote(EnumLanguage language);

		/// <summary>
		/// Returns the next quote after the one with the QuoteId = <paramref name="quoteId"/>
		/// </summary>
		Task<Quote?> GetNextQuote(long quoteId);

		/// <summary>
		/// Returns the previous quote before the one with the QuoteId = <paramref name="quoteId"/>
		/// </summary>
		Task<Quote?> GetPreviousQuote(long quoteId);
	}
}
