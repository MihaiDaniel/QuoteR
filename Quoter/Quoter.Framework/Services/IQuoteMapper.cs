using Quoter.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Services
{
	public interface IQuoteMapper
	{
		List<QuoteModel> GetQuotes(List<string> lstString, string title);
	}
}
