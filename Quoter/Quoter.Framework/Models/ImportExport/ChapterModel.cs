using Newtonsoft.Json;

namespace Quoter.Framework.Models.ImportExport
{
	/// <summary>
	/// Model used for exporting and importing chapters
	/// </summary>
	public class ChapterModel
	{
		[JsonProperty("ChapterId")]
		public int ChapterId { get; set; }

		[JsonProperty("Name")]
		public string Name { get; set; }

		[JsonProperty("Description")]
		public string? Description { get; set; }

		[JsonProperty("IsFavourite")]
		public bool? IsFavourite { get; set; }

		/// <summary>
		/// The chapter index number in the book
		/// </summary>
		[JsonProperty("ChapterIndex")]
		public int ChapterIndex { get; set; }

		[JsonProperty("BookId")]
		public int BookId { get; set; }
	}
}
