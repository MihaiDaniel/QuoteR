using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Entities
{
	public class Quote
	{
		public long QuoteId { get; set; }

		public string Content { get; set; }

		#region FK

		public int CollectionId { get; set; }

		public Collection Collection { get; set; }

		public int? BookId { get; set; }

		public Book? Book { get; set; }


		public int? ChapterId { get; set; }

		public Chapter? Chapter { get; set; }

		#endregion FX
	}
}
