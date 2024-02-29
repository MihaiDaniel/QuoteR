using Quoter.App.Forms.Reader;
using Quoter.Framework.Data.Entities;
using Quoter.Framework.Models;

namespace Quoter.App.FormsControllers.Reader
{
	public interface IReaderFormController : IFormControllerWithOptions<IReaderForm, ReaderFormOptions>
	{
		string Quotes { get; set; }

		Task SetNextChapterAsync();

		Task SetPreviousChapterAsync();

		Task SetSelectedBookAsync(Book book);

		Task SetSelectedChapterAsync(Chapter chapter);
	}
}
