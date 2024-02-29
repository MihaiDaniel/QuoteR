using Quoter.App.Forms.Common;
using Quoter.Framework.Data.Entities;

namespace Quoter.App.Forms.Reader
{
	public interface IReaderForm : IResizableForm, IForm
	{
		void SetQuotesContent(string content);
		void BuildTreeNavigation(Collection collection);
		void SetLocationInCollection(string location);
		void ScrollToQuote(string quoteContent);
	}
}
