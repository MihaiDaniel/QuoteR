using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.App.Models
{
	public class QuoteSaveOptions
	{
		public bool TrimUntillFirstWhiteSpace { get; set; }
		public string ExcludeChars { get; set; }
		public string AppendTextToBegining { get; set; }
		public string AppendTextToEnd { get; set; }
		public string ReplaceChars { get; set; }
		public string ReplacedCharsReplacement { get; set; }
		public string TrimUntil { get; set; }
	}
}
