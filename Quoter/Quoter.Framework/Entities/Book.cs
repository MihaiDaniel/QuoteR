﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Entities
{
	public class Book
	{
		public int BookId { get; set; }

		public string Name { get; set; }

		#region FK

		public int CollectionId { get; set; }

		public Collection Collection { get; set; }

		public List<Chapter> LstChapters { get; set; }

		public List<Quote> LstQuotes { get; set; }

		#endregion FK
	}
}
