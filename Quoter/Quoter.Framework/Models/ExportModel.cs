using Quoter.Framework.Entities;

namespace Quoter.Framework.Models
{
	public class ExportModel
	{
		public Guid Id { get; set; }

		public List<CollectionExportModel> Collections { get; set; }

		public List<BookExportModel> Books { get; set; }

		public List<ChapterExportModel> Chapters { get; set; }

		public List<QuoteExportModel> Quotes { get; set; }
	}
}
