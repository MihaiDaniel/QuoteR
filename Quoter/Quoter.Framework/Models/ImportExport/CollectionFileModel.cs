using Quoter.Framework.Enums;

namespace Quoter.Framework.Models.ImportExport
{
	public class CollectionFileModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public EnumLanguage Language { get; set; }
		public string FilePath { get; set; }
	}
}
