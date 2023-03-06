using Quoter.Framework.Enums;
using Quoter.Framework.Models;

namespace Quoter.Framework.Services.ImportExport
{
	public interface IImportService
	{
		void QueueImportJob(ImportParameters importParameters);
	}
}
