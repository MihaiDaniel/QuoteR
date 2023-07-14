using Quoter.Framework.Models.ImportExport;

namespace Quoter.Framework.Services.ImportExport
{
	/// <summary>
	/// Interface for a service used to import collection of books,chapters,quotes
	/// </summary>
	public interface IImportService
	{
		/// <summary>
		/// Import a collection into the application database. Begin an import job if no other job is in progress.
		/// If there is another import job in progress this will be put on hold untill the other one finishes.
		/// </summary>
		void QueueImportJob(ImportParameters importParameters);

		Task BeginImport(ImportParameters importParameters);
	}
}
