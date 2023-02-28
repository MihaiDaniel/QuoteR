using Microsoft.EntityFrameworkCore;
using Quoter.Framework.Data;
using Quoter.Framework.Entities;
using Quoter.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Services
{
	public class QuoteService : IQuoteService
	{
		private readonly QuoterContext _context;
		private Random _random;

		public QuoteService(QuoterContext context)
		{
			_context = context;
			_random = new Random();
		}

		public async Task<QuoteModel?> GetNextQuote(long quoteId)
		{
			Quote? currentQuote = await _context.Quotes.FirstOrDefaultAsync(q => q.QuoteId == quoteId);
			if (currentQuote == null)
			{
				return null;
			}
			IQueryable<Quote> query = GetQuoteQueryable(currentQuote.CollectionId, currentQuote.BookId, currentQuote.ChapterId);
			Quote? nextQuote = await query.FirstOrDefaultAsync(q => q.QuoteIndex == (currentQuote.QuoteIndex + 1));

			if (nextQuote != null)
			{
				return GetQuoteModel(nextQuote);
			}
			// Try to see if another chapter/book exists after the one of the current quote and get from there
			return null;
		}

		public async Task<QuoteModel?> GetPreviousQuote(long quoteId)
		{
			Quote? currentQuote = await _context.Quotes.FirstOrDefaultAsync(q => q.QuoteId == quoteId);
			if (currentQuote == null)
			{
				return null;
			}
			IQueryable<Quote> query = GetQuoteQueryable(currentQuote.CollectionId, currentQuote.BookId, currentQuote.ChapterId);
			Quote? previousQuote = await query.FirstOrDefaultAsync(q => q.QuoteIndex == (currentQuote.QuoteIndex - 1));

			if (previousQuote != null)
			{
				return GetQuoteModel(previousQuote);
			}
			// Try to see if another chapter/book exists before the one of the current quote and get from there
			return null;
		}

		public async Task<QuoteModel?> GetRandomQuote()
		{
			List<long> idQuotes = await _context.Quotes.Select(q => q.QuoteId).ToListAsync();
			if (idQuotes is null || idQuotes.Count == 0)
			{
				return null;
			}

			int quoteIndexToShow = _random.Next(idQuotes.Count);
			long quoteIdToShow = idQuotes[quoteIndexToShow];

			Quote quote = await _context.Quotes
										.Include(q => q.Book)
										.Include(q => q.Chapter)
										.Include(q => q.Collection)
										.FirstAsync(q => q.QuoteId == quoteIdToShow);
			return GetQuoteModel(quote);
		}

		private IQueryable<Quote> GetQuoteQueryable(int collectionId, int? bookId, int? chapterId)
		{
			IQueryable<Quote> quotes = _context.Quotes.Where(q => q.CollectionId == collectionId);
			if (bookId.HasValue)
			{
				quotes = quotes.Where(q => q.BookId == bookId);
			}
			if (chapterId.HasValue)
			{
				quotes = quotes.Where(q => q.ChapterId == chapterId);
			}
			return quotes;
		}

		private QuoteModel GetQuoteModel(Quote quote)
		{
			string title = "";
			string footer = "";

			if (quote.Book != null && quote.Chapter != null)
			{
				title = quote.Book.Name;
				footer = quote.Chapter.Name + ":" + quote.QuoteIndex;
			}
			else if (quote.Book != null)
			{
				title = quote.Book.Name;
			}
			else
			{
				title = quote.Collection.Name;
			}

			return new QuoteModel()
			{
				QuoteId = quote.QuoteId,
				Title = title,
				Footer = footer,
				Body = quote.Content,
				OpenAnimation = Framework.Enums.EnumAnimation.FadeInFromBottomRight,
				CloseAnimation = Framework.Enums.EnumAnimation.FadeOut,
				AllowNavigation = true
			};
		}
	}
}
