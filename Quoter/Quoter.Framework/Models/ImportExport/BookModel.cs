using Newtonsoft.Json;

namespace Quoter.Framework.Models.ImportExport
{
	/// <summary>
	/// Model used for exporting and importing books
	/// </summary>
	public class BookModel
	{
		[JsonProperty("BookId")]
		public int BookId { get; set; }

		[JsonProperty("Name")]
		public string Name { get; set; }

		[JsonProperty("Description")]
		public string? Description { get; set; }

		[JsonProperty("IsFavourite")]
		public bool? IsFavourite { get; set; }

		[JsonProperty("CollectionId")]
		public int CollectionId { get; set; }

	}
}
