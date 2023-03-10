namespace Quoter.Framework.Models.ImportExport
{
    public class QuoteExportModel
    {
        public long QuoteId { get; set; }

        /// <summary>
        /// The quote index number in the chapter / book or collection
        /// </summary>
        public int QuoteIndex { get; set; }

        public string Content { get; set; }

        public string? Description { get; set; }

        public int CollectionId { get; set; }

        public int? BookId { get; set; }

        public int? ChapterId { get; set; }
    }
}
