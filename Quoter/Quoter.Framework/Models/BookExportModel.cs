namespace Quoter.Framework.Models
{
	public class BookExportModel
	{
		public int BookId { get; set; }

		public string Name { get; set; }

		public string? Description { get; set; }

		public bool? IsFavourite { get; set; }

		public int CollectionId { get; set; }

	}
}
