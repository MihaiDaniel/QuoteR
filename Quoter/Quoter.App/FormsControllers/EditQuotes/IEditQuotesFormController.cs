using Quoter.App.Forms.Manage;
using Quoter.App.Models;
using Quoter.Framework.Entities;
using System.ComponentModel;

namespace Quoter.App.FormsControllers.EditQuotes
{
    public interface IEditQuotesFormController : IFormController<IEditQuotesForm>
	{
		Collection? SelectedCollection { get; set; }
		BindingList<Collection> Collections { get; }

		Book? SelectedBook { get; set; }
		BindingList<Book> Books { get; }

		Chapter? SelectedChapter { get; set; }
		BindingList<Chapter> Chapters { get; }

		string Quotes { get; set; }

		Task LoadCollections();
		Task LoadCollectionBooksOrQuotes();
		Task LoadBookChaptersOrQuotes();
		Task LoadQuotes();


		Task AddCollection();
		Task EditCollection();
		Task DeleteCollection();

		Task AddBook();
		Task EditBook();
		Task DeleteBook();

		Task AddChapter();
		Task EditChapter();
		Task DeleteChapter();

		Task AddQuotes(QuoteSaveOptions saveOptions);
		Task QuickAdd();
	}
}
