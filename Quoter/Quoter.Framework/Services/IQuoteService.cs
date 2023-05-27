using Quoter.Framework.Entities;
using Quoter.Shared.Enums;

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
		Task<Quote?> GetRandQuoteFromFavsAsync(EnumLanguage language);

		/// <summary>
		/// Returns the next quote after the one with the QuoteId = <paramref name="quoteId"/>
		/// </summary>
		Task<Quote?> GetNextQuoteAsync(long quoteId);

		/// <summary>
		/// Returns the previous quote before the one with the QuoteId = <paramref name="quoteId"/>
		/// </summary>
		Task<Quote?> GetPreviousQuoteAsync(long quoteId);

		/// <summary>
		/// Returns a list of <see cref="Quote"/> depending on the parameters passed.
		/// If no <paramref name="bookId"/> is specified it will filter only by <paramref name="collectionId"/>.
		/// If no <paramref name="chapterId"/> is specified it will filter only by <paramref name="collectionId"/> and <paramref name="bookId"/>.
		/// If not <paramref name="bookId"/> or <paramref name="chapterId"/> is specified it will filter only by <paramref name="collectionId"/>
		/// </summary>
		Task<List<Quote>> GetQuotesAsync(int collectionId, int? bookId, int? chapterId);
	}
}
