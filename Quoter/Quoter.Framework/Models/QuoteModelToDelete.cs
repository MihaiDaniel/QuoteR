using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Models
{
	public class QuoteModelToDelete
	{
		public int Id { get; set; }

		public string File { get; set; }

		public string Title { get; set; }

		public string Chapter { get; set; }

		public string Body { get; set; }

		public string Subchapter { get; set; }
	}
}
