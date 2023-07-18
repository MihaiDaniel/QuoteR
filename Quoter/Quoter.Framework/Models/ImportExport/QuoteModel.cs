using Newtonsoft.Json;

namespace Quoter.Framework.Models.ImportExport
{
	/// <summary>
	/// Model used for exporting and importing quotes
	/// </summary>
	public class QuoteModel
	{
		[JsonProperty("QuoteId")]
		public long QuoteId { get; set; }

		/// <summary>
		/// The quote index number in the chapter / book or collection
		/// </summary>
		[JsonProperty("QuoteIndex")]
		public int QuoteIndex { get; set; }

		[JsonProperty("Content")]
		public string Content { get; set; }

		[JsonProperty("Description")]
		public string? Description { get; set; }

		[JsonProperty("CollectionId")]
		public int CollectionId { get; set; }

		[JsonProperty("BookId")]
		public int? BookId { get; set; }

		[JsonProperty("ChapterId")]
		public int? ChapterId { get; set; }
	}
}
