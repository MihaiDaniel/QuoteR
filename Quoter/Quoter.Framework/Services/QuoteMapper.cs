using Quoter.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Services
{
	public class QuoteMapper : IQuoteMapper
	{
		public List<QuoteModel> GetQuotes(List<string> lstString, string title)
		{
			List<QuoteModel> lstQuotes = new List<QuoteModel>();
			foreach (string line in lstString)
			{
				QuoteModel? quote = TryGetQuoteFromLine(line);
				if (quote != null)
				{
					lstQuotes.Add(quote);
				}
			}
			return lstQuotes;
		}

		private QuoteModel? TryGetQuoteFromLine(string line)
		{
			string[] arrContent = line.Split(new char[] { '\r', '\n', '\t' });
			if (arrContent.Length == 3) // Title, footer, body
			{
				return new QuoteModel()
				{
					Chapter = arrContent[0],
					Subchapter = arrContent[1],
					Body = arrContent[2]
				};
			}
			else if (arrContent.Length == 2) // Title, body
			{
				return new QuoteModel()
				{
					Chapter = arrContent[0],
					Body = arrContent[2]
				};
			}
			else if (arrContent.Length == 1) // body
			{
				return new QuoteModel()
				{
					Body = arrContent[0],
				};
			}
			return null;
		}
	}
}
