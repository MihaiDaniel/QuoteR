using Quoter.Framework.Enums;
using Quoter.Framework.Models.ImportExport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
