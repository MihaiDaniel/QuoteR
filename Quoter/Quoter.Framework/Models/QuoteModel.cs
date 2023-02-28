using Quoter.Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Models
{
	public class QuoteModel
	{
		public long QuoteId { get; set; }

		public string Title { get; set; }

		public string Body { get; set; }

		public string Footer { get; set; }

		public EnumAnimation? OpenAnimation { get; set; }

		public EnumAnimation? CloseAnimation { get; set; }

		/// <summary>
		/// Indicates if user can see and is allowed to go to next or previous quotes
		/// </summary>
		public bool AllowNavigation { get; set; } = false;

		public QuoteModel()
		{
			Title = string.Empty;
			Body = string.Empty;
			Footer = string.Empty;
		}
	}
}
