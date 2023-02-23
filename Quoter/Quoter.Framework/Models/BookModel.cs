using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Models
{
	public class BookModel
	{
		public bool IsDefault { get; set; }

		public string Name { get; set; }

		public List<ChapterModel> LstChapters { get; set; }

	}
}
