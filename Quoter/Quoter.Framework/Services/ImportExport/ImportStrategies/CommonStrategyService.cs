using Microsoft.EntityFrameworkCore;
using Quoter.Framework.Data;
using Quoter.Framework.Models.ImportExport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Services.ImportExport.ImportStrategies
{
	public class CommonStrategyService : ICommonStrategyService
	{
		private readonly QuoterContext _context;
		private readonly ILogger _logger;
		private readonly ICollectionService _collectionService;

		public CommonStrategyService(QuoterContext context, ILogger logger,
			ICollectionService collectionService)
		{
			_context = context;
			_logger = logger;
			_collectionService = collectionService;
		}

		public void UpdateBooksCollectionIdReferences(List<BookModel> lstBookModels, int currentCollectionId, int newCollectionId)
		{
			lstBookModels
				.Where(b => b.CollectionId == currentCollectionId)
				.ToList()
				.ForEach(book => { book.CollectionId = newCollectionId; });
		}

		public void UpdateQuotesCollectionIdReferences(List<QuoteModel> lstQuoteModels, int currentCollectionId, int newCollectionId)
		{
			lstQuoteModels
				.Where(q => q.CollectionId == currentCollectionId)
				.ToList()
				.ForEach(q => { q.CollectionId = newCollectionId; });
		}

		public async Task UpdateChaptersReferencesToBooksIdsFromImportIds(List<ChapterModel> lstChapterModels)
		{
			// group chapters by bookId to reduce the number of queries
			List<List<ChapterModel>> groupingByBook = lstChapterModels
				.GroupBy(q => new
				{
					q.BookId,
				})
				.Select(grp => grp.ToList())
				.ToList();

			foreach (List<ChapterModel> chapterModels in groupingByBook)
			{
				int bookNewId = await GetBookIdFromImportId(chapterModels.First().BookId);
				chapterModels.ForEach(chapterModel => { chapterModel.BookId = bookNewId; });
			}
		}

		public async Task UpdateQuotesReferencesToBooksIdsFromImportIds(List<QuoteModel> lstQuotesModels)
		{
			// group quotes by bookId to reduce the number of queries
			List<List<QuoteModel>> groupingByBook = lstQuotesModels
				.GroupBy(q => new
				{
					q.BookId,
				})
				.Select(grp => grp.ToList())
				.ToList();

			foreach (List<QuoteModel> quoteModels in groupingByBook)
			{
				int? groupingBookId = quoteModels.First().BookId;
				if (groupingBookId.HasValue && groupingBookId != 0)
				{
					int bookNewId = await GetBookIdFromImportId(groupingBookId.Value);
					quoteModels.ForEach(quoteModel => { quoteModel.BookId = bookNewId; });
				}
			}
		}

		public async Task UpdateQuotesReferencesToChaptersIdsFromImportIds(List<QuoteModel> lstQuotesModels)
		{
			// group quotes by chapterId to reduce the number of queries
			List<List<QuoteModel>> groupingByChapters = lstQuotesModels
				.GroupBy(q => new
				{
					q.ChapterId,
				})
				.Select(grp => grp.ToList())
				.ToList();

			foreach (List<QuoteModel> quoteModels in groupingByChapters)
			{
				int? groupingChapterId = quoteModels.First().ChapterId;
				if (groupingChapterId.HasValue && groupingChapterId != 0)
				{
					int? chapterNewId = await GetChapterIdFromImportId(groupingChapterId);
					quoteModels.ForEach(quoteModel => { quoteModel.ChapterId = chapterNewId; });
				}
			}
		}

		private async Task<int> GetBookIdFromImportId(int importId)
		{
			int bookId = await _context.Books
					.Where(b => b.ImportBookId == importId)
					.Select(b => b.BookId)
					.FirstOrDefaultAsync();
			return bookId;
		}

		private async Task<int?> GetChapterIdFromImportId(int? importId)
		{
			int? chapterId = await _context.Chapters
					.Where(c => c.ImportChapterId == importId)
					.Select(c => c.ChapterId)
					.FirstOrDefaultAsync();
			if (chapterId == 0)
			{
				chapterId = null;
			}
			return chapterId;
		}
	}
}
