using Quoter.Framework.Entities;

namespace Quoter.App.Forms
{
	public interface IReaderForm : IResizableForm, IForm
	{
		void SetQuotesContent(string content);
		void BuildTreeNavigation(Collection collection);
		void SetLocationInCollection(string location);
		void ScrollToQuote(string quoteContent);
	}
}
