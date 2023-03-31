using Quoter.Framework.Entities;
using System.Text;

namespace Quoter.Framework.Helpers
{
	public static class QuoteHelper
	{
		/// <summary>
		/// Returns a list of <see cref="Quote"/> as a single string. Quotes are separated by new lines.
		/// </summary>
		public static string? GetString(List<Quote> lstQuotes)
		{
			if (lstQuotes.Any())
			{
				StringBuilder stringBuilder = new();
				foreach (Quote quote in lstQuotes)
				{
					stringBuilder.Append(quote.Content);
					stringBuilder.Append(Environment.NewLine);
				}
				return stringBuilder.ToString();
			}
			else
			{
				return default;
			}
		}
	}
}
