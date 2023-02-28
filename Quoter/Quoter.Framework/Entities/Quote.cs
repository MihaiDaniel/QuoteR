using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Entities
{
	/// <summary>
	/// Represents the quote content.
	/// </summary>
	public class Quote
	{
		/// <summary>
		/// PK
		/// </summary>
		public long QuoteId { get; set; }

		/// <summary>
		/// The quote index number in the chapter / book or collection
		/// </summary>
		public int QuoteIndex { get; set; }

		public string Content { get; set; }

		public string? Description { get; set; }

		#region FK

		public int CollectionId { get; set; }

		public Collection Collection { get; set; }

		public int? BookId { get; set; }

		public Book? Book { get; set; }


		public int? ChapterId { get; set; }

		public Chapter? Chapter { get; set; }

		#endregion FK
	}
}
