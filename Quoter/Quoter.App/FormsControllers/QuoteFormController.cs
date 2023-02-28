using Quoter.App.Forms;
using Quoter.Framework.Models;
using Quoter.Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.FormsControllers
{
	public class QuoteFormController : IQuoteFormController
	{
		private readonly IQuoteService _quoteService;
		private IQuoteForm _form;

		public QuoteFormController(IQuoteService quoteService)
		{
			_quoteService = quoteService;
		}

		public string Title => throw new NotImplementedException();

		public string Body => throw new NotImplementedException();

		public string Footer => throw new NotImplementedException();

		public async Task GetNextQuote(long currentQuoteId)
		{
			QuoteModel? quote = await _quoteService.GetNextQuote(currentQuoteId);
			if (quote != null)
			{
				_form.SetQuote(quote);
			}
		}

		public async Task GetPreviousQuote(long currentQuoteId)
		{
			QuoteModel? quote = await _quoteService.GetPreviousQuote(currentQuoteId);
			if(quote != null)
			{
				_form.SetQuote(quote);
			}
			
		}

		public void RegisterForm(IQuoteForm quoteForm)
		{
			_form = quoteForm;
		}

	}
}
