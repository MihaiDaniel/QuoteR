using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Entities
{
	public class Chapter
	{
		public int ChapterId { get; set; }

		public string Name { get; set; }

		#region FK

		public int BookId { get; set; }

		public Book Book { get; set; }

		public List<Quote> LstQuotes { get; set; }

		#endregion FK
	}
}
