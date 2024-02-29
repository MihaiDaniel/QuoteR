namespace Quoter.Framework.Data.Entities
{
    /// <summary>
    /// Represents the quote content.
    /// The quote must be contained in a <see cref="Collection"/>, but it can also be contained
    /// in a <see cref="Book"/> inside a <see cref="Chapter"/>
    /// </summary>
    public class Quote
    {
        /// <summary>
        /// PK
        /// </summary>
        public long QuoteId { get; set; }

        /// <summary>
        /// The quote index number in the chapter / book or collection.
        /// This is an integer that must be incremented by 1 starting at 1 for each quote
        /// in a chapter/book or collection
        /// </summary>
        public int QuoteIndex { get; set; }

        /// <summary>
        /// The actual string content of the quote
        /// </summary>
        public string Content { get; set; }

        public string? Description { get; set; }

        #region FK

        public int CollectionId { get; set; }

        public Collection Collection { get; set; }

        public int? BookId { get; set; }

        public Book? Book { get; set; }


        public int? ChapterId { get; set; }

        public Chapter? Chapter { get; set; }

        #endregion FK
    }
}
