using Microsoft.EntityFrameworkCore;
using Quoter.Framework.Data;
using Quoter.Framework.Entities;
using Quoter.Framework.Models;
using Quoter.Framework.Models.ImportExport;
using Quoter.Framework.Services.Messaging;
using System.Text.Json;

namespace Quoter.Framework.Services.ImportExport
{
    public class ImportService : IImportService
	{
		private readonly object _lock = new object();
		private readonly QuoterContext _context;
		private readonly IMessagingService _messagingService;
		private readonly ILogger _logger;

		private bool _isImportInProgress;
		public bool IsImportInProgress
		{
			get
			{
				lock (_lock)
				{
					return _isImportInProgress;
				}
			}
			private set
			{
				lock (_lock)
				{
					_isImportInProgress = value;
				}
			}
		}

		public ImportService(QuoterContext context, IMessagingService messagingService, ILogger logger)
		{
			_context = context;
			_messagingService = messagingService;
			_logger = logger;
		}

		/// <inheritdoc/>
		public void QueueImportJob(ImportParameters importParameters)
		{
			Task.Run(async () =>
			{
				bool canStart = await WaitForCurrentJobToFinish();
				if (canStart)
				{
					await BeginImport(importParameters);
				}
			}).ConfigureAwait(false);
		}

		private async Task<bool> WaitForCurrentJobToFinish()
		{
			// We want to have a single import job at any one time
			// so we wait before starting another one
			DateTime startTime = DateTime.Now;
			while (IsImportInProgress)
			{
				if (DateTime.Now > startTime.AddHours(1))
				{
					// Something might be off, so reset the bool
					// and reject the pending job
					IsImportInProgress= false;
					return false;
				}
				await Task.Delay(3000);
			}
			return true;
		}

		private async Task BeginImport(ImportParameters importParameters)
		{
			PostedAnnouncement announcement = _messagingService.PostAnnouncement(Event.ImportInProgress, "");
			try
			{
				IsImportInProgress = true;
				string importedCollections = string.Empty;
				foreach (string file in importParameters.Files)
				{
					importedCollections += await ImportFile(file, importParameters);
				}
				announcement.Remove();
				_messagingService.SendMessage(Event.ImportSuccesfull, importedCollections);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
				announcement.Remove();
				_messagingService.SendMessage(Event.ImportFailed, ex.Message);
			}
			finally
			{
				IsImportInProgress = false;
			}
		}

		private async Task<string> ImportFile(string file, ImportParameters importParameters)
		{
			string fileContent = await File.ReadAllTextAsync(file);
			if (string.IsNullOrEmpty(fileContent))
			{
				return string.Empty;
			}
			ExportModel importModel = JsonSerializer.Deserialize<ExportModel>(fileContent);
			string importedCollections = string.Empty;

			foreach (CollectionExportModel collectionModel in importModel.Collections)
			{
				await ImportCollection(collectionModel, importModel, importParameters);
				importedCollections += collectionModel.Name + "; " + Environment.NewLine;
			}
			foreach (BookExportModel bookExportModel in importModel.Books)
			{
				await ImportBook(bookExportModel, importModel, importParameters);
			}
			foreach (ChapterExportModel chapterModel in importModel.Chapters)
			{
				await ImportChapter(chapterModel, importModel, importParameters);
			}
			foreach (QuoteExportModel quoteModel in importModel.Quotes)
			{
				await ImportQuote(quoteModel, importParameters);
			}
			await _context.SaveChangesAsync(); // For quotes, just save once at the end
			return importedCollections;
		}

		private async Task ImportCollection(CollectionExportModel collectionModel, ExportModel importModel, ImportParameters importParameters)
		{
			if (importParameters.IsIgnoreLanguage)
			{
				collectionModel.Language = importParameters.Language;
			}
			if (importParameters.IsMergeCollections)
			{
				Collection? existingCol = await _context.Collections.FirstOrDefaultAsync(c => c.Name == collectionModel.Name);
				if (existingCol == null)
				{
					Collection addedCollection = await AddCollection(collectionModel);
					UpdateImportDataReferencesToCollection(importModel, collectionModel.CollectionId, addedCollection.CollectionId);
				}
				else
				{
					UpdateImportDataReferencesToCollection(importModel, collectionModel.CollectionId, existingCol.CollectionId);
				}
			}
			else
			{
				await CheckForCollectionNamingConflicts(collectionModel, 0);

				Collection addedCollection = await AddCollection(collectionModel);
				UpdateImportDataReferencesToCollection(importModel, collectionModel.CollectionId, addedCollection.CollectionId);
			}
		}

		/// <summary>
		/// In case we import (without merging) collections with similar names, we might end up with collections with the same name.
		/// So we check for any existing collections with the same name as the imported one and if exists we append a number to
		/// the collection name to rename it
		/// </summary>
		/// <remarks>
		/// Books or chapters with similar names don't bother us since they will be in separate collections, or if
		/// they are merged and have similar names they will be just disregarded
		/// </remarks>
		private async Task CheckForCollectionNamingConflicts(CollectionExportModel collectionModel, int iteration)
		{
			if (iteration > 99)     // Odd situation, but just to be safe append a guid instead
			{
				collectionModel.Name = collectionModel.Name + " - " + Guid.NewGuid().ToString();
			}
			else if (iteration > 0) // For subsequent iteration we add an incremented number to the name
			{
				string nameToCheck = $"{collectionModel.Name} ({iteration})";
				bool isNameConflict = await _context.Collections.AnyAsync(c => c.Name == nameToCheck);
				if (isNameConflict)
				{
					iteration++;
					await CheckForCollectionNamingConflicts(collectionModel, iteration);
				}
				else
				{
					collectionModel.Name = $"{collectionModel.Name} ({iteration})";
				}
			}
			else                    // For first iteration we just check the name as it is
			{
				bool isNameConflict = await _context.Collections.AnyAsync(c => c.Name == collectionModel.Name);
				if (isNameConflict)
				{
					iteration++;
					await CheckForCollectionNamingConflicts(collectionModel, iteration);
				}
			}
		}

		private async Task<Collection> AddCollection(CollectionExportModel model)
		{
			Collection addedCollection = new Collection()
			{
				Description = model.Description,
				Name = model.Name,
				Language = model.Language,
				IsFavourite = false,
			};
			_context.Collections.Add(addedCollection);
			await _context.SaveChangesAsync();
			return addedCollection;
		}

		/// <summary>
		/// After adding a collection to the database it's CollectionId most likely has changed, so we must update all other
		/// object's references to the Collection
		/// </summary>
		private void UpdateImportDataReferencesToCollection(ExportModel importModel, int currentCollectionId, int newCollectionId)
		{
			importModel.Books.Where(b => b.CollectionId == currentCollectionId).ToList()
				.ForEach(book => { book.CollectionId = newCollectionId; });

			importModel.Quotes.Where(q => q.CollectionId == currentCollectionId).ToList()
				.ForEach(q => { q.CollectionId = newCollectionId; });
		}

		private async Task ImportBook(BookExportModel bookModel, ExportModel importModel, ImportParameters importParameters)
		{
			if (importParameters.IsMergeCollections)
			{
				Book? existingBook = await _context.Books.FirstOrDefaultAsync(b => b.Name == bookModel.Name
																				&& b.CollectionId == bookModel.CollectionId);
				if (existingBook == null)
				{
					Book addedBook = await AddBook(bookModel);
					UpdateImportDataReferencesToBook(importModel, bookModel.BookId, addedBook.BookId);
				}
				else
				{
					UpdateImportDataReferencesToBook(importModel, bookModel.BookId, existingBook.BookId);
				}
			}
			else
			{
				Book addedBook = await AddBook(bookModel);
				UpdateImportDataReferencesToBook(importModel, bookModel.BookId, addedBook.BookId);
			}
		}

		private async Task<Book> AddBook(BookExportModel bookModel)
		{
			Book addedBook = new Book()
			{
				Name = bookModel.Name,
				Description = bookModel.Description,
				CollectionId = bookModel.CollectionId,
				IsFavourite = false,
			};
			_context.Books.Add(addedBook);
			await _context.SaveChangesAsync();
			return addedBook;
		}

		/// <summary>
		/// After adding a book to the database it's BookId most likely has changed, so we must update all other
		/// object's references to the Book
		/// </summary>
		private void UpdateImportDataReferencesToBook(ExportModel importModel, int currentBookId, int newBookId)
		{
			importModel.Chapters.Where(c => c.BookId == currentBookId).ToList()
				.ForEach(c => { c.BookId = newBookId; });

			importModel.Quotes.Where(q => q.BookId == currentBookId).ToList()
				.ForEach(q => { q.BookId = newBookId; });
		}

		private async Task ImportChapter(ChapterExportModel chapterModel, ExportModel importModel, ImportParameters importParameters)
		{
			if (importParameters.IsMergeCollections)
			{
				Chapter? existingChapter = await _context.Chapters.FirstOrDefaultAsync(c => c.Name == chapterModel.Name
																						&& c.BookId == chapterModel.BookId);
				if (existingChapter == null)
				{
					Chapter addedChapter = await AddChapter(chapterModel);
					UpdateImportDataReferencesToChapter(importModel, chapterModel.ChapterId, addedChapter.ChapterId);
				}
				else
				{
					UpdateImportDataReferencesToChapter(importModel, chapterModel.ChapterId, existingChapter.ChapterId);
				}
			}
			else
			{
				Chapter addedChapter = await AddChapter(chapterModel);
				UpdateImportDataReferencesToChapter(importModel, chapterModel.ChapterId, addedChapter.ChapterId);
			}
		}

		private async Task<Chapter> AddChapter(ChapterExportModel chapterModel)
		{
			Chapter addedChapter = new Chapter()
			{
				Name = chapterModel.Name,
				Description = chapterModel.Description,
				BookId = chapterModel.BookId,
				IsFavourite = false
			};
			_context.Chapters.Add(addedChapter);
			await _context.SaveChangesAsync();
			return addedChapter;
		}

		/// <summary>
		/// After adding a chapter to the database it's ChapterId most likely has changed, so we must update all other
		/// object's (quotes) references to the chapter
		/// </summary>
		private void UpdateImportDataReferencesToChapter(ExportModel importModel, int currentChapterId, int newChapterId)
		{
			importModel.Quotes.Where(q => q.ChapterId == currentChapterId).ToList()
				.ForEach(q => { q.ChapterId = newChapterId; });
		}

		private async Task ImportQuote(QuoteExportModel quoteModel, ImportParameters importParameters)
		{
			int quoteIndex = 0;
			if (importParameters.IsMergeCollections)
			{
				int lastIndex = await _context.Quotes
				.Where(q => q.CollectionId == quoteModel.CollectionId
						&& q.BookId == quoteModel.BookId
						&& q.ChapterId == quoteModel.BookId)
				.Select(q => q.QuoteIndex)
				.MaxAsync();
				quoteIndex = lastIndex + 1;
			}
			else
			{
				quoteIndex = quoteModel.QuoteIndex;
			}

			Quote quote = new Quote()
			{
				BookId = quoteModel.BookId,
				CollectionId = quoteModel.CollectionId,
				ChapterId = quoteModel.ChapterId,
				QuoteIndex = quoteIndex,
				Description = quoteModel.Description,
				Content = quoteModel.Content,
			};
			_context.Quotes.Add(quote);
		}
	}
}
