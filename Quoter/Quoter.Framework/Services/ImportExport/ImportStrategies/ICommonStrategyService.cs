using Quoter.Framework.Models.ImportExport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Services.ImportExport.ImportStrategies
{
	public interface ICommonStrategyService
	{
		void UpdateBooksCollectionIdReferences(List<BookModel> lstBookModels, int currentCollectionId, int newCollectionId);

		void UpdateQuotesCollectionIdReferences(List<QuoteModel> lstQuoteModels, int currentCollectionId, int newCollectionId);

		Task UpdateChaptersReferencesToBooksIdsFromImportIds(List<ChapterModel> lstChapterModels);

		Task UpdateQuotesReferencesToBooksIdsFromImportIds(List<QuoteModel> lstQuotesModels);

		Task UpdateQuotesReferencesToChaptersIdsFromImportIds(List<QuoteModel> lstQuotesModels);

	}
}
