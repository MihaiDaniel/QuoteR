using Quoter.Framework.Enums;
using Quoter.Framework.Models.ImportExport;

namespace Quoter.App.Forms
{
	public interface IWelcomeForm
	{
		void SetTab(EnumWelcomeTab tab);
		void SetImportableCollections(List<CollectionFileModel> collectionFiles);
		List<CollectionFileModel> GetSelectedCollections();
		void SetSelectedLanguage(EnumLanguage language);
	}
}
