using Quoter.App.Forms;
using Quoter.Framework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		void LoadCollections();
	}
}
