using Quoter.Framework.Enums;

namespace Quoter.Framework.Models
{
	public class ImportParameters
	{
		public string[] Files { get; set; } 
		public bool IsIgnoreLanguage { get; set; }
		public bool IsMergeCollections { get; set; }
		public EnumLanguage Language { get; set; }
	}
}
