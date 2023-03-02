﻿using Quoter.App.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.FormsControllers
{
	public interface IQuoteFormController : IFormController<IQuoteForm>
	{
		Task GetRandomQuote();
		Task GetNextQuote();
		Task GetPreviousQuote();
	}
}
