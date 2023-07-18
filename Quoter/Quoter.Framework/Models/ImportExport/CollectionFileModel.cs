using Quoter.Shared.Enums;

namespace Quoter.Framework.Models.ImportExport
{
	/// <summary>
	/// Model that holds information about a file containing collections for import
	/// </summary>
	public class CollectionFileModel
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public EnumLanguage Language { get; set; }

		public string FilePath { get; set; }
	}
}
