using Quoter.App.Forms;
using Quoter.Framework.Entities;
using System.ComponentModel;

namespace Quoter.App.FormsControllers
{
	public interface IEditQuotesFormController
	{
		void RegisterForm(IEditQuotesForm editQuotesForm);

		Collection? SelectedCollection { get; set; }
		BindingList<Collection> Collections { get; }

		Book? SelectedBook { get; set; }
		BindingList<Book> Books { get; }

		Chapter? SelectedChapter { get; set; }
		BindingList<Chapter> Chapters { get; }

		void AddCollection();
		void EditCollection();
		void DeleteCollection();

		void AddBook();
		void EditBook();
		void DeleteBook();

		void AddChapter();
		void EditChapter();
		void DeleteChapter();

		string Quotes { get; set; }

		void Save();
	}
}
