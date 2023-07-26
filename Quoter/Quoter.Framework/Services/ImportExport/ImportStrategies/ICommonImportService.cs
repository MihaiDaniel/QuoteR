using Quoter.Framework.Models.ImportExport;

namespace Quoter.Framework.Services.ImportExport.ImportStrategies
{
	public interface ICommonImportService
	{
		void UpdateBooksCollectionIdReferences(List<BookModel> lstBookModels, int currentCollectionId, int newCollectionId);

		void UpdateQuotesCollectionIdReferences(List<QuoteModel> lstQuoteModels, int currentCollectionId, int newCollectionId);

		Task UpdateChaptersReferencesToBooksIdsFromImportIds(List<ChapterModel> lstChapterModels);

		Task UpdateQuotesReferencesToBooksIdsFromImportIds(List<QuoteModel> lstQuotesModels);

		Task UpdateQuotesReferencesToChaptersIdsFromImportIds(List<QuoteModel> lstQuotesModels);

	}
}
