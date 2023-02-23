using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Models
{
	public class ChapterModel
	{
		public int Id { get; set; }
		
		public bool IsDefault { get; set; }

		public string Name { get; set; }

		public List<string> LstContent { get; set; }
	}
}
