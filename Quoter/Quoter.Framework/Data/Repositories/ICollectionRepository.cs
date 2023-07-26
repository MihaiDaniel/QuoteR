namespace Quoter.Framework.Data.Repositories
{
	/// <summary>
	/// Interface for the collection repository, dealing with collections and it's data
	/// </summary>
	public interface ICollectionRepository
	{
		/// <summary>
		/// Deletes a collection and all of it's associated books, chapters, quotes
		/// </summary>
		Task DeleteCollectionAsync(int collectionId);

		/// <summary>
		/// Deletes all quotes for a particular collection, book and chapter.
		/// <paramref name="bookId"/> and <paramref name="chapterId"/> are optional to narrow
		/// down the quotes to delete. To delete all for a collection you can specify only the <paramref name="collectionId"/>
		/// </summary>
		Task DeleteQuotesAsync(int collectionId, int? bookId, int? chapterId);

		/// <summary>
		/// This method deletes all ImportId's which hold the original primary keys of imported collection's books or chapters.
		/// </summary>
		/// <remarks>
		/// In case the user imports data from other users the ImportIds can be duplicated across collections
		/// depending on circumstances, so as a safe measure we should delete all this data at the end of each import.
		/// </remarks>
		Task DeleteImportIdsAsync();

		/// <summary>
		/// Returns an unique collection name by checking against the database for duplicates.
		/// </summary>
		Task<string> GetUniqueCollectionNameAsync(string collectionName);
	}
}
