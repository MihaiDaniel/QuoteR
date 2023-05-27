using Quoter.Framework.Enums;
using Quoter.Framework.Models.ImportExport;
using Quoter.Shared.Enums;

namespace Quoter.App.Forms.Welcome
{
	public interface IWelcomeForm
	{
		void SetTab(EnumWelcomeTab tab);
		void SetImportableCollections(List<CollectionFileModel> collectionFiles);
		List<CollectionFileModel> GetSelectedCollections();
		void SetSelectedLanguage(EnumLanguage language);
	}
}
