namespace Quoter.Framework.Models.ImportExport
{
	public class ChapterExportModel
	{
		public int ChapterId { get; set; }

		public string Name { get; set; }

		public string? Description { get; set; }

		public bool? IsFavourite { get; set; }

		/// <summary>
		/// The chapter index number in the book
		/// </summary>
		public int ChapterIndex { get; set; }

		public int BookId { get; set; }
	}
}
