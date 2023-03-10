using Quoter.Framework.Entities;

namespace Quoter.Framework.Models.ImportExport
{
	public class ExportModel
	{
		/// <summary>
		/// Unique identifier of an exported collection
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Product version under which the export was made
		/// </summary>
		public string Version { get; set; }

		public List<CollectionExportModel> Collections { get; set; }

		public List<BookExportModel> Books { get; set; }

		public List<ChapterExportModel> Chapters { get; set; }

		public List<QuoteExportModel> Quotes { get; set; }
	}
}
