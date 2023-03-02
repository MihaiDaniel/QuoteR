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
			IQueryable<Quote> query = GetQuoteQueryableByBookAndChapter(currentQuote);
			Quote? nextQuote = await query.FirstOrDefaultAsync(q => q.QuoteIndex > (currentQuote.QuoteIndex));

			if (nextQuote == null)
			{
				query = GetQuoteQueryableByBookNext(currentQuote);
				nextQuote = await query.FirstOrDefaultAsync();
				if (nextQuote != null)
				{
					return GetQuoteModel(nextQuote);
				}
			}
			else
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
			IQueryable<Quote> query = GetQuoteQueryableByBookAndChapter(currentQuote);
			Quote? previousQuote = await query.OrderBy(q => q.QuoteId).LastOrDefaultAsync(q => q.QuoteIndex < (currentQuote.QuoteIndex));

			if (previousQuote == null)
			{
				query = GetQuoteQueryableByBookPrevious(currentQuote);
				previousQuote = await query.OrderBy(q => q.QuoteId).LastOrDefaultAsync();
				if (previousQuote != null)
				{
					return GetQuoteModel(previousQuote);
				}
			}
			else
			{
				return GetQuoteModel(previousQuote);
			}
			// Try to see if another chapter/book exists before the one of the current quote and get from there
			return null;
		}

		public async Task<QuoteModel?> GetRandomQuote()
		{
			List<long> idQuotes = await _context.Quotes
				.Where(q => (q.ChapterId != null && q.Chapter.IsFavourite == true)
							|| (q.ChapterId == null && q.BookId != null && q.Book.IsFavourite == true)
							|| (q.ChapterId == null && q.BookId == null && q.Collection.IsFavourite == true))
				.Select(q => q.QuoteId).ToListAsync();

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
