using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Entities
{
	public class Collection
	{
		public int CollectionId { get; set; }

		public string Name { get; set; }

		#region FK

		public List<Book> LstBooks { get; set; }

		public List<Quote> LstQuotes { get; set; }

		#endregion FK

	}
}
