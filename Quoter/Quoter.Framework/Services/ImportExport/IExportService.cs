using Quoter.Framework.Models.ImportExport;

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
		void QueueExportJob(ExportParameters exportParameters);
	}
}
