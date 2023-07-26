using Quoter.Framework.Data;
using Quoter.Framework.Data.Repositories;
using Quoter.Framework.Entities;
using Quoter.Framework.Models.ImportExport;

namespace Quoter.Framework.Services.ImportExport.ImportStrategies
{
	/// <summary>
	/// Default strategy import service implementation. This import service simply imports collections 
	/// without checking for duplicate collections books, etc. If another collection with the same name
	/// is found, it simply inserts with the name + a number.
	/// </summary>
	public class DefaultImportStrategyService : IImportStrategyService
	{
		private readonly QuoterContext _context;
		private readonly ILogger _logger;
		private readonly ICollectionRepository _collectionRepo;
		private readonly ICommonImportService _commonStrategyService;

		public DefaultImportStrategyService(
			QuoterContext context,
			ILogger logger,
			ICollectionRepository collectionRepo,
			ICommonImportService commonStrategyService)
		{
			_context = context;
			_logger = logger;
			_collectionRepo = collectionRepo;
			_commonStrategyService = commonStrategyService;
		}

		public async Task ImportAsync(ImportExportModel importModel, ImportParameters importParameters)
		{
			try
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
			finally
			{
				await _collectionRepo.DeleteImportIdsAsync();
				_context.ChangeTracker.Clear();
			}
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
				collectionModel.Name = await _collectionRepo.GetUniqueCollectionNameAsync(collectionModel.Name);
				Collection addedCollection = await AddCollectionAsync(collectionModel);
				_commonStrategyService.UpdateBooksCollectionIdReferences(importModel.Books, collectionModel.CollectionId, addedCollection.CollectionId);
				_commonStrategyService.UpdateQuotesCollectionIdReferences(importModel.Quotes, collectionModel.CollectionId, addedCollection.CollectionId);
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
			List<Book> booksToAdd = importModel.Books
				.Select(bookModel => new Book
				{
					Name = bookModel.Name,
					Description = bookModel.Description,
					CollectionId = bookModel.CollectionId,
					IsFavourite = importParameters.IsFavourite ? true : bookModel.IsFavourite,
					ImportBookId = bookModel.BookId
				})
				.ToList();
			_context.Books.AddRange(booksToAdd);
			await _context.SaveChangesAsync();
			await _commonStrategyService.UpdateChaptersReferencesToBooksIdsFromImportIds(importModel.Chapters);
			await _commonStrategyService.UpdateQuotesReferencesToBooksIdsFromImportIds(importModel.Quotes);
		}

		private async Task ImportChaptersAsync(ImportExportModel importModel, ImportParameters importParameters)
		{
			List<Chapter> chaptersToAdd = importModel.Chapters
				.Select(chapterModel => new Chapter()
				{
					Name = chapterModel.Name,
					Description = chapterModel.Description,
					BookId = chapterModel.BookId,
					IsFavourite = importParameters.IsFavourite ? true : chapterModel.IsFavourite,
					ImportChapterId = chapterModel.ChapterId
				})
				.ToList();
			_context.Chapters.AddRange(chaptersToAdd);
			await _context.SaveChangesAsync();
			await _commonStrategyService.UpdateQuotesReferencesToChaptersIdsFromImportIds(importModel.Quotes);
		}

		private async Task ImportQuotesAsync(ImportExportModel importModel)
		{
			List<Quote> lstQuotesToAdd = importModel.Quotes
				.Select(quoteModel => new Quote()
				{
					CollectionId = quoteModel.CollectionId,
					BookId = quoteModel.BookId,
					ChapterId = quoteModel.ChapterId,
					QuoteIndex = quoteModel.QuoteIndex,
					Description = quoteModel.Description,
					Content = quoteModel.Content,
				}).ToList();
			_context.Quotes.AddRange(lstQuotesToAdd);
			await _context.SaveChangesAsync();
		}
	}
}
