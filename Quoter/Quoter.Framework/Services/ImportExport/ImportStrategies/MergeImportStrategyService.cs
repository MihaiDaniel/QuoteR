using Microsoft.EntityFrameworkCore;
using Quoter.Framework.Entities;
using Quoter.Framework.Models.ImportExport;
using Quoter.Framework.Data;

namespace Quoter.Framework.Services.ImportExport.ImportStrategies
{
	/// <summary>
	/// Importing strategy service that will add new collections or merge data into an existing collection if a collection
	/// with the same name already exists.
	/// This will also merge books and chapters. 
	/// Quotes will be replaced instead.
	/// </summary>
	/// <remarks>
	/// For the following existing collection:
	/// Collection1
	///		Book1
	///			Chapter1
	///				Quote1
	///				Quote2
	///				Quote3
	///			Chapter2
	///				Quote1
	/// 
	/// If we import the following:
	/// Collection1
	///		Book1
	///			Chapter2
	///				QuoteX
	///				QuoteY
	///			Chapter3
	///				QuoteZ
	///		Book2
	///			Chapter1
	///				QuoteA
	///				
	/// The final result will be:
	/// Collection1					=
	///		Book1					=
	///			Chapter1			=
	///				Quote1			=
	///				Quote2			=
	///				Quote3			=
	///			Chapter2			~ (chapter stays the same, but it's content is new)
	///				QuoteX			+
	///				QuoteY			+
	///			Chapter3			+
	///				QuoteZ			+
	///		Book2					+
	///			Chapter1			+
	///				QuoteA			+
	/// </remarks>
	public class MergeImportStrategyService : IImportStrategyService
	{
		private readonly QuoterContext _context;
		private readonly ILogger _logger;
		private readonly ICollectionService _collectionService;
		private readonly ICommonStrategyService _commonStrategyService;

		public MergeImportStrategyService(
			QuoterContext context,
			ILogger logger,
			ICollectionService collectionService,
			ICommonStrategyService commonStrategyService)
		{
			_context = context;
			_logger = logger;
			_collectionService = collectionService;
			_commonStrategyService = commonStrategyService;
		}

		public async Task ImportAsync(ImportExportModel importModel, ImportParameters importParameters)
		{
			// Import collections
			await ImportCollectionsAsync(importModel, importParameters);

			// Import books
			await ImportBooksAsync(importModel, importParameters);

			// Import chapters
			await ImportChaptersAsync(importModel, importParameters);

			// Import quotes
			await ImportQuotesAsync(importModel);
		}

		private async Task ImportCollectionsAsync(ImportExportModel importModel, ImportParameters importParameters)
		{
			foreach (CollectionModel collectionModel in importModel.Collections)
			{
				if (importParameters.IsIgnoreLanguage)
				{
					collectionModel.Language = importParameters.Language;
				}
				if (importParameters.IsFavourite)
				{
					collectionModel.IsFavourite = true;
				}
				Collection? existingCollection = await _context.Collections.FirstOrDefaultAsync(c => c.Name == collectionModel.Name);
				if (existingCollection is not null)
				{
					_commonStrategyService.UpdateBooksCollectionIdReferences(importModel.Books, collectionModel.CollectionId, existingCollection.CollectionId);
					_commonStrategyService.UpdateQuotesCollectionIdReferences(importModel.Quotes, collectionModel.CollectionId, existingCollection.CollectionId);
				}
				else
				{
					Collection addedCollection = await AddCollectionAsync(collectionModel);
					_commonStrategyService.UpdateBooksCollectionIdReferences(importModel.Books, collectionModel.CollectionId, addedCollection.CollectionId);
					_commonStrategyService.UpdateQuotesCollectionIdReferences(importModel.Quotes, collectionModel.CollectionId, addedCollection.CollectionId);
				}
			}
		}

		private async Task<Collection> AddCollectionAsync(CollectionModel model)
		{
			Collection addedCollection = new()
			{
				Description = model.Description,
				Name = model.Name,
				Language = model.Language,
				IsFavourite = model.IsFavourite,
			};
			_context.Collections.Add(addedCollection);
			await _context.SaveChangesAsync();
			return addedCollection;
		}

		private async Task ImportBooksAsync(ImportExportModel importModel, ImportParameters importParameters)
		{
			List<Book> booksToAdd = new();
			foreach (BookModel bookModel in importModel.Books)
			{
				Book? existingBook = await _context.Books
					.Where(b => b.CollectionId == bookModel.CollectionId && b.Name == bookModel.Name)
					.FirstOrDefaultAsync();

				if(existingBook != null)
				{
					existingBook.ImportBookId = bookModel.BookId;
				}
				else
				{
					booksToAdd.Add(new Book()
					{
						Name = bookModel.Name,
						Description = bookModel.Description,
						CollectionId = bookModel.CollectionId,
						IsFavourite = importParameters.IsFavourite ? true : bookModel.IsFavourite,
						ImportBookId = bookModel.BookId
					});
				}
			}
			_context.Books.AddRange(booksToAdd);
			await _context.SaveChangesAsync();
			await _commonStrategyService.UpdateChaptersReferencesToBooksIdsFromImportIds(importModel.Chapters);
			await _commonStrategyService.UpdateQuotesReferencesToBooksIdsFromImportIds(importModel.Quotes);
		}

		private async Task ImportChaptersAsync(ImportExportModel importModel, ImportParameters importParameters)
		{
			List<Chapter> chaptersToAdd = new();
			foreach(ChapterModel chapterModel in importModel.Chapters)
			{
				Chapter? existingChapter = await _context.Chapters
					.Where(c => c.BookId == chapterModel.BookId && c.Name == chapterModel.Name)
					.FirstOrDefaultAsync();
				if(existingChapter != null)
				{
					existingChapter.ImportChapterId = chapterModel.ChapterId;
				}
				else
				{
					chaptersToAdd.Add(new Chapter()
					{
						Name = chapterModel.Name,
						Description = chapterModel.Description,
						BookId = chapterModel.BookId,
						IsFavourite = importParameters.IsFavourite ? true : chapterModel.IsFavourite,
						ImportChapterId = chapterModel.ChapterId
					});
				}
			}
			_context.Chapters.AddRange(chaptersToAdd);
			await _context.SaveChangesAsync();
			await _commonStrategyService.UpdateQuotesReferencesToChaptersIdsFromImportIds(importModel.Quotes);
		}

		private async Task ImportQuotesAsync(ImportExportModel importModel)
		{
			List<List<QuoteModel>> grouping = importModel.Quotes
				.GroupBy(q => new
				{
					q.CollectionId,
					q.BookId,
					q.ChapterId
				})
				.Select(grp => grp.ToList())
				.ToList();

			foreach (List<QuoteModel> quoteModels in grouping)
			{
				int collectionId = quoteModels.First().CollectionId;
				int? bookId = quoteModels.First().BookId;
				int? chapterId = quoteModels.First().ChapterId;

				await _collectionService.DeleteQuotesAsync(collectionId, bookId, chapterId);
				_context.Quotes.AddRange(quoteModels
					.Select(quoteModel => new Quote()
					{
						CollectionId = quoteModel.CollectionId,
						BookId = quoteModel.BookId,
						ChapterId = quoteModel.ChapterId,
						QuoteIndex = quoteModel.QuoteIndex,
						Description = quoteModel.Description,
						Content = quoteModel.Content,
					})
					.ToList());
			}
			await _context.SaveChangesAsync();
		}
	}
}
