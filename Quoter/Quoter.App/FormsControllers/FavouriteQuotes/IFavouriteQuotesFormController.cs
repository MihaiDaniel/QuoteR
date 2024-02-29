using Quoter.App.Forms.Manage;
using Quoter.Framework.Data.Entities;
using System.ComponentModel;

namespace Quoter.App.FormsControllers.FavouriteQuotes
{
	public interface IFavouriteQuotesFormController : IFormController<IFavouriteQuotesForm>
	{

		BindingList<Collection> Collections { get; }

		BindingList<Book> Books { get; }

		BindingList<Chapter> Chapters { get; }

		void CollectionCheckChanged(Collection collection, bool isChecked);
		void BookCheckChanged(Book book, bool isChecked);
		void ChapterCheckChanged(Chapter chapter, bool isChecked);

		void CollectionSelected(Collection collection);
		void BookSelected(Book book);
		void ChapterSelected(Chapter chapter);

		Task LoadCollections();

		void ReadCollection(Collection? selectedCollection);

		void Export(bool isExportOnlyFavourites);

		void Import(bool isImportMerge, bool isImportIgnoreLang);

	}
}
