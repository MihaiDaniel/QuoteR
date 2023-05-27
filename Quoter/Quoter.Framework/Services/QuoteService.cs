using Microsoft.EntityFrameworkCore;
using Quoter.Framework.Data;
using Quoter.Framework.Entities;
using Quoter.Shared.Enums;
using Quoter.Framework.Models;
using System.Text;

namespace Quoter.Framework.Services
{
	public class QuoteService : IQuoteService
	{
		private readonly QuoterContext _context;
		private readonly Random _random;

		public QuoteService(QuoterContext context)
		{
			_context = context;
			_random = new Random();
		}

		/// <inheritdoc/>
		public async Task<Quote?> GetRandQuoteFromFavsAsync(EnumLanguage language)
		{
			IQueryable<Quote> queryableQuoteIds = _context.Quotes
				.Where(q => (q.ChapterId != null && q.Chapter.IsFavourite == true)
							|| (q.ChapterId == null && q.BookId != null && q.Book.IsFavourite == true)
							|| (q.ChapterId == null && q.BookId == null && q.Collection.IsFavourite == true));

			if (language != EnumLanguage.None)
			{
				queryableQuoteIds = queryableQuoteIds.Where(q => q.Collection.Language == language);
			}

			List<long> idQuotes = await queryableQuoteIds.Select(q => q.QuoteId).ToListAsync();

			if (idQuotes is null || idQuotes.Count == 0)
			{
				return null;
			}

			int quoteIndexToShow = _random.Next(idQuotes.Count);
			long quoteIdToShow = idQuotes[quoteIndexToShow];

			Quote? quote = await _context.Quotes
										.Include(q => q.Book)
										.Include(q => q.Chapter)
										.Include(q => q.Collection)
										.FirstOrDefaultAsync(q => q.QuoteId == quoteIdToShow);
			return quote;
		}

		/// <inheritdoc/>
		public async Task<Quote?> GetNextQuoteAsync(long quoteId)
		{
			Quote? currentQuote = await _context.Quotes.FirstOrDefaultAsync(q => q.QuoteId == quoteId);
			if (currentQuote == null)
			{
				return null;
			}
			IQueryable<Quote> query = GetQuoteQueryableByBookAndChapter(currentQuote);
			Quote? nextQuote = await query.FirstOrDefaultAsync(q => q.QuoteIndex > (currentQuote.QuoteIndex));

			if (nextQuote == null)
			{
				query = GetQuoteQueryableByBookNext(currentQuote);
				nextQuote = await query.FirstOrDefaultAsync();
				if (nextQuote != null)
				{
					return nextQuote;
				}
			}
			else
			{
				return nextQuote;
			}
			// Try to see if another chapter/book exists after the one of the current quote and get from there
			return null;
		}

		/// <inheritdoc/>
		public async Task<Quote?> GetPreviousQuoteAsync(long quoteId)
		{
			Quote? currentQuote = await _context.Quotes.FirstOrDefaultAsync(q => q.QuoteId == quoteId);
			if (currentQuote == null)
			{
				return null;
			}
			IQueryable<Quote> query = GetQuoteQueryableByBookAndChapter(currentQuote);
			Quote? previousQuote = await query.OrderBy(q => q.QuoteId).LastOrDefaultAsync(q => q.QuoteIndex < (currentQuote.QuoteIndex));

			if (previousQuote == null)
			{
				query = GetQuoteQueryableByBookPrevious(currentQuote);
				previousQuote = await query.OrderBy(q => q.QuoteId).LastOrDefaultAsync();
				if (previousQuote != null)
				{
					return previousQuote;
				}
			}
			else
			{
				return previousQuote;
			}
			return null;
		}

		/// <inheritdoc/>
		public async Task<List<Quote>> GetQuotesAsync(int collectionId, int? bookId, int? chapterId)
		{
			IQueryable<Quote> queryQuotes = _context.Quotes.Where(q => q.CollectionId == collectionId);

			if (bookId.HasValue && chapterId.HasValue)
			{
				queryQuotes = queryQuotes.Where(q => q.BookId == bookId && q.ChapterId == chapterId);
			}
			else if (bookId.HasValue)
			{
				queryQuotes = queryQuotes.Where(q => q.BookId == bookId);
			}

			return await queryQuotes.ToListAsync();
		}

		private IQueryable<Quote> GetQuoteQueryableByBookAndChapter(Quote currentQuote)
		{
			IQueryable<Quote> quotes = _context.Quotes.Where(q => q.CollectionId == currentQuote.CollectionId);
			if (currentQuote.BookId.HasValue)
			{
				quotes = quotes.Where(q => q.BookId == currentQuote.BookId);
			}
			if (currentQuote.ChapterId.HasValue)
			{
				quotes = quotes.Where(q => q.ChapterId == currentQuote.ChapterId);
			}
			quotes = quotes.Where(q => (q.ChapterId != null && q.Chapter.IsFavourite == true)
							|| (q.ChapterId == null && q.BookId != null && q.Book.IsFavourite == true)
							|| (q.ChapterId == null && q.BookId == null && q.Collection.IsFavourite == true));
			return quotes;
		}

		private IQueryable<Quote> GetQuoteQueryableByBookPrevious(Quote currentQuote)
		{
			IQueryable<Quote> quotes = _context.Quotes
				.Include(q => q.Chapter)
				.Include(q => q.Book)
				.Where(q => q.CollectionId == currentQuote.CollectionId);
			if (currentQuote.BookId.HasValue)
			{
				quotes = quotes.Where(q => q.BookId == currentQuote.BookId);
			}
			quotes = quotes.Where(q => (q.ChapterId != null && q.ChapterId < currentQuote.ChapterId && q.Chapter.IsFavourite == true)
							|| (q.ChapterId == null && q.BookId != null && q.Book.IsFavourite == true)
							|| (q.ChapterId == null && q.BookId == null && q.Collection.IsFavourite == true));
			return quotes;
		}

		private IQueryable<Quote> GetQuoteQueryableByBookNext(Quote currentQuote)
		{
			IQueryable<Quote> quotes = _context.Quotes
				.Include(q => q.Chapter)
				.Include(q => q.Book)
				.Where(q => q.CollectionId == currentQuote.CollectionId);
			if (currentQuote.BookId.HasValue)
			{
				quotes = quotes.Where(q => q.BookId == currentQuote.BookId);
			}
			quotes = quotes.Where(q => (q.ChapterId != null && q.ChapterId > currentQuote.ChapterId && q.Chapter.IsFavourite == true)
							|| (q.ChapterId == null && q.BookId != null && q.Book.IsFavourite == true)
							|| (q.ChapterId == null && q.BookId == null && q.Collection.IsFavourite == true));
			return quotes;
		}

	}
}
