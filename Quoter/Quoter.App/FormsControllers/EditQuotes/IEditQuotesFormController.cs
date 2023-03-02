using Quoter.App.Forms;
using Quoter.App.Models;
using Quoter.Framework.Entities;
using System.ComponentModel;

namespace Quoter.App.FormsControllers.EditQuotes
{
    public interface IEditQuotesFormController : IFormController<IEditQuotesForm>
    {

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

        void AddQuotes(QuoteSaveOptions saveOptions);
    }
}
