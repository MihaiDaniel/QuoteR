using Quoter.App.Forms;
using Quoter.Framework.Entities;
using Quoter.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.FormsControllers.Reader
{
	public interface IReaderFormController : IFormController<IReaderForm>
	{

		void SetFormOptions(ReaderFormOptions options);

		string Quotes { get; set; }

		Task SetNextChapterAsync();

		Task SetPreviousChapterAsync();

		Task SetSelectedBookAsync(Book book);

		Task SetSelectedChapterAsync(Chapter chapter);
	}
}
