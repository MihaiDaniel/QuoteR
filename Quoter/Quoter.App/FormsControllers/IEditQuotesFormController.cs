using Quoter.App.Forms;
using Quoter.Framework.Entities;
using System.ComponentModel;

namespace Quoter.App.FormsControllers
{
	public interface IEditQuotesFormController
	{
		/// <summary>
		/// Mandatory step before using other methods.
		/// Due to DI we would have a circular dependency between the 
		/// controller and the form, so we use this method to avoid this.
		/// </summary>
		void RegisterForm(IEditQuotesForm editQuotesForm);

		void OnClose();

		Collection? SelectedCollection { get; set; }
		BindingList<Collection> Collections { get; }

		Book? SelectedBook { get; set; }
		BindingList<Book> Books { get; }

		Chapter? SelectedChapter { get; set; }
		BindingList<Chapter> Chapters { get; }

		string Quotes { get; set; }

		void AddCollection();
		void EditCollection();
		void DeleteCollection();

		void AddBook();
		void EditBook();
		void DeleteBook();

		void AddChapter();
		void EditChapter();
		void DeleteChapter();

		void AddQuotes();
	}
}
