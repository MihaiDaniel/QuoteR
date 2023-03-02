namespace Quoter.Framework.Entities
{
	/// <summary>
	/// Represents a chapter of a book
	/// </summary>
	public class Chapter
	{
		public int ChapterId { get; set; }

		public string Name { get; set; }

		public string? Description { get; set; }

		public bool? IsFavourite { get; set; }

		/// <summary>
		/// The chapter index number in the book
		/// </summary>
		public int ChapterIndex { get; set; }

		#region FK

		public int BookId { get; set; }

		public Book Book { get; set; }

		public List<Quote> LstQuotes { get; set; }

		#endregion FK
	}
}
