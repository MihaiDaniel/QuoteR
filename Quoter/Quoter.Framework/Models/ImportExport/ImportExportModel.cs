using Newtonsoft.Json;

namespace Quoter.Framework.Models.ImportExport
{
	/// <summary>
	/// Model that is used for exporting and importing collections 
	/// and all the associated books, chapters and quotes
	/// </summary>
	public class ImportExportModel
	{
		/// <summary>
		/// Unique identifier of an exported collection
		/// </summary>
		[JsonProperty("Id")]
		public Guid Id { get; set; }

		/// <summary>
		/// Product version under which the export was made
		/// </summary>
		[JsonProperty("Version")]
		public string Version { get; set; }

		[JsonProperty("Collections")]
		public List<CollectionModel> Collections { get; set; }

		[JsonProperty("Books")]
		public List<BookModel> Books { get; set; }

		[JsonProperty("Chapters")]
		public List<ChapterModel> Chapters { get; set; }

		[JsonProperty("Quotes")]
		public List<QuoteModel> Quotes { get; set; }
	}
}
