using Quoter.App.Forms.Reader;
using Quoter.Framework.Entities;
using Quoter.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
