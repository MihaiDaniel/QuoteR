﻿using Microsoft.EntityFrameworkCore;
using Quoter.Framework.Data;
using Quoter.Framework.Entities;
using Quoter.Framework.Models.ImportExport;
using Quoter.Framework.Services.Messaging;
using System.Diagnostics;
using System.Text.Json;

namespace Quoter.Framework.Services.ImportExport
{
	/// <summary>
	/// Service used to import collection of books,chapters,quotes
	/// </summary>
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
					IsImportInProgress = false;
					return false;
				}
				await Task.Delay(3000);
			}
			return true;
		}

		public async Task BeginImport(ImportParameters importParameters)
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

				_messagingService.SendMessage(Event.ImportSuccesfull, new ImportResult()
				{
					ImportedFilesMessage = importedCollections,
					NotifyUser = importParameters.NotifyUser
				});
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
				announcement.Remove();
				_messagingService.SendMessage(Event.ImportFailed, ex.Message);
			}
			finally
			{
				announcement.Remove();
				IsImportInProgress = false;
			}
		}

		private async Task<string> ImportFile(string file, ImportParameters importParameters)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			_logger.Info($"Starting Import on file: {file}");

			string fileContent = await File.ReadAllTextAsync(file);
			if (string.IsNullOrEmpty(fileContent))
			{
				return string.Empty;
			}
			_logger.Info($"{stopwatch.ElapsedMilliseconds} FileRead");
			stopwatch.Restart();

			ExportModel importModel = JsonSerializer.Deserialize<ExportModel>(fileContent);
			string importedCollections = string.Empty;
			_logger.Info($"{stopwatch.ElapsedMilliseconds} model deserialized");
			stopwatch.Restart();


			foreach (CollectionExportModel collectionModel in importModel.Collections)
			{
				await ImportCollectionAsync(collectionModel, importModel, importParameters);
				importedCollections += Environment.NewLine + collectionModel.Name + "; ";
			}
			_logger.Info($"{stopwatch.ElapsedMilliseconds} imported collections");

			await _context.Database.BeginTransactionAsync();
			foreach (BookExportModel bookExportModel in importModel.Books)
			{
				await ImportBookAsync(bookExportModel, importModel, importParameters);
			}
			await _context.Database.CommitTransactionAsync();
			_logger.Info($"{stopwatch.ElapsedMilliseconds} imported books");

			foreach (ChapterExportModel chapterModel in importModel.Chapters)
			{
				await ImportChapterAsync(chapterModel, importModel, importParameters);
			}
			_logger.Info($"{stopwatch.ElapsedMilliseconds} imported chapters");

			await ImportQuotesAsync(importModel, importParameters);

			_logger.Info($"{stopwatch.ElapsedMilliseconds} imported quotes");

			await _context.SaveChangesAsync(); // For quotes, just save once at the end
			_logger.Info($"{stopwatch.ElapsedMilliseconds} finished saving changes to database");

			return importedCollections;
		}

		private async Task ImportCollectionAsync(CollectionExportModel collectionModel, ExportModel importModel, ImportParameters importParameters)
		{
			if (importParameters.IsIgnoreLanguage)
			{
				collectionModel.Language = importParameters.Language;
			}
			if (importParameters.IsFavourite)
			{
				collectionModel.IsFavourite = true;
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
				IsFavourite = model.IsFavourite,
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

		private async Task ImportBookAsync(BookExportModel bookModel, ExportModel importModel, ImportParameters importParameters)
		{
			if (importParameters.IsFavourite)
			{
				bookModel.IsFavourite = true;
			}
			if (importParameters.IsMergeCollections)
			{
				int existingBookId = await _context.Books
					.Where(b => b.Name == bookModel.Name && b.CollectionId == bookModel.CollectionId)
					.Select(b => b.BookId)
					.FirstOrDefaultAsync();
				if (existingBookId == 0)
				{
					Book addedBook = await AddBook(bookModel);
					UpdateImportDataReferencesToBook(importModel, bookModel.BookId, addedBook.BookId);
				}
				else
				{
					UpdateImportDataReferencesToBook(importModel, bookModel.BookId, existingBookId);
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
				IsFavourite = bookModel.IsFavourite,
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

		private async Task ImportChapterAsync(ChapterExportModel chapterModel, ExportModel importModel, ImportParameters importParameters)
		{
			if (importParameters.IsFavourite)
			{
				chapterModel.IsFavourite = true;
			}
			if (importParameters.IsMergeCollections)
			{
				int existingChapterId = await _context.Chapters
					.Where(c => c.Name == chapterModel.Name && c.BookId == chapterModel.BookId)
					.Select(c => c.ChapterId)
					.FirstOrDefaultAsync();
				if (existingChapterId == 0)
				{
					Chapter addedChapter = await AddChapter(chapterModel);
					UpdateImportDataReferencesToChapter(importModel, chapterModel.ChapterId, addedChapter.ChapterId);
				}
				else
				{
					UpdateImportDataReferencesToChapter(importModel, chapterModel.ChapterId, existingChapterId);
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
				IsFavourite = chapterModel.IsFavourite
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

		private async Task ImportQuotesAsync(ExportModel importModel, ImportParameters importParameters)
		{
			List<List<QuoteExportModel>> groupingByCollBookChapter = importModel.Quotes
				.GroupBy(q => new
				{
					q.CollectionId,
					q.BookId,
					q.ChapterId
				})
				.Select(grp => grp.ToList())
				.ToList();

			foreach(List<QuoteExportModel> lstQuoteModels in groupingByCollBookChapter)
			{
				List<Quote> lstQuotesToAdd = lstQuoteModels
					.Select(quoteModel => new Quote()
					{
						BookId = quoteModel.BookId,
						CollectionId = quoteModel.CollectionId,
						ChapterId = quoteModel.ChapterId,
						QuoteIndex = quoteModel.QuoteIndex,
						Description = quoteModel.Description,
						Content = quoteModel.Content,
					}).ToList();

				if (importParameters.IsMergeCollections)
				{
					int currentLargestQuoteIndex = (await _context.Quotes
						.Where(q => q.CollectionId == lstQuotesToAdd[0].CollectionId
								&& q.BookId == lstQuotesToAdd[0].BookId
								&& q.ChapterId == lstQuotesToAdd[0].ChapterId)
						.Select(q => q.QuoteIndex)
						.ToListAsync())
						.DefaultIfEmpty(0)
						.Max();

					lstQuotesToAdd.ForEach(q => q.QuoteIndex += currentLargestQuoteIndex);
				}
				_context.Quotes.AddRange(lstQuotesToAdd);
			}
		}

	}
}
