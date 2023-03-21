using Quoter.Framework.Enums;

namespace Quoter.Framework.Models.ImportExport
{
	public class CollectionExportModel
	{
		public int CollectionId { get; set; }

		public string Name { get; set; }

		public string? Description { get; set; }

		public bool? IsFavourite { get; set; }

		public EnumLanguage? Language { get; set; }
	}
}
