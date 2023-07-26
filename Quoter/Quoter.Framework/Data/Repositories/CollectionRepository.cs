using Microsoft.EntityFrameworkCore;
using Quoter.Framework.Entities;

namespace Quoter.Framework.Data.Repositories
{
	public class CollectionRepository : ICollectionRepository
	{
		private readonly QuoterContext _context;

		public CollectionRepository(QuoterContext context)
		{
			_context = context;
		}

		public async Task DeleteQuotesAsync(int collectionId, int? bookId, int? chapterId)
		{
			if (bookId == null && chapterId == null)
			{
				await _context.Database.ExecuteSqlRawAsync(
						$"DELETE FROM {nameof(_context.Quotes)} WHERE {nameof(Quote.CollectionId)} = {collectionId}");
			}
			else if (chapterId == null && bookId != null)
			{
				await _context.Database.ExecuteSqlRawAsync(
						$"DELETE FROM {nameof(_context.Quotes)} WHERE {nameof(Quote.CollectionId)} = {collectionId} AND {nameof(Quote.BookId)} = {bookId}");
			}
			else if (chapterId != null && bookId != null)
			{
				await _context.Database.ExecuteSqlRawAsync(
							$"DELETE FROM {nameof(_context.Quotes)} WHERE {nameof(Quote.CollectionId)} = {collectionId} AND {nameof(Quote.BookId)} = {bookId} AND {nameof(Quote.ChapterId)} = {chapterId}");
			}
		}

		public async Task DeleteCollectionAsync(int collectionId)
		{
			await _context.Database.ExecuteSqlRawAsync(
						$"DELETE FROM {nameof(_context.Collections)} WHERE {nameof(Collection.CollectionId)} = {collectionId}");
		}

		public async Task DeleteImportIdsAsync()
		{
			await _context.Database.ExecuteSqlRawAsync($"UPDATE {nameof(_context.Books)} SET {nameof(Book.ImportBookId)} = null ");
			await _context.Database.ExecuteSqlRawAsync($"UPDATE {nameof(_context.Chapters)} SET {nameof(Chapter.ImportChapterId)} = null ");
		}

		public async Task<string> GetUniqueCollectionNameAsync(string collectionName)
		{
			return await GetUniqueCollectionNameRecursivelyAsync(collectionName, 0);
		}

		private async Task<string> GetUniqueCollectionNameRecursivelyAsync(string name, int iteration)
		{
			string nameToCheck;
			if (iteration > 99)     // Odd situation, but just to be safe append a guid instead
			{
				return name + " - " + Guid.NewGuid().ToString();
			}
			else if (iteration > 0)
			{
				nameToCheck = $"{name} ({iteration})";
			}
			else
			{
				nameToCheck = name;
			}

			bool isNameConflict = await _context.Collections.AnyAsync(c => c.Name == nameToCheck);
			if (isNameConflict)
			{
				iteration++;
				return await GetUniqueCollectionNameRecursivelyAsync(name, iteration);
			}
			else
			{
				return nameToCheck;
			}
		}
	}
}
