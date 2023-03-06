namespace Quoter.Framework.Services.ImportExport
{
	/// <summary>
	/// Interface for the service responsible for exporting collections
	/// </summary>
	public interface IExportService
	{
		/// <summary>
		/// Begin an export job on a separate thread for collections (books, chapters, quotes)
		/// </summary>
		/// <remarks>
		/// <paramref name="isExportOnlyFavourites"/> Does not take into account if books or chapters are favourites or not
		/// </remarks>
		/// <param name="isExportOnlyFavourites">If True will only export collections marked as Favourites</param>
		/// <param name="filePath">Destination file path</param>
		void QueueExportJob(bool isExportOnlyFavourites, string filePath);
	}
}
