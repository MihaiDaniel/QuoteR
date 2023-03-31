namespace Quoter.Framework.Models
{
	/// <summary>
	/// Options when opening the reader form for reading quotes form a collection
	/// </summary>
	public class ReaderFormOptions
	{
		public int CollectionId { get; set; }

		public int? BookId { get; set; }

		public int? ChapterId { get; set; }

		public long? QuoteId { get; set; }
	}
}
