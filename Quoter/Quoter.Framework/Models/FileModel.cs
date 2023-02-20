using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Models
{
	public class FileModel
	{
		public string Name { get; set; }

		public string NameWithNoExtension { get; set; }

		public string Path { get; set; }

		public List<string> LstLines { get; set; }
	}
}
