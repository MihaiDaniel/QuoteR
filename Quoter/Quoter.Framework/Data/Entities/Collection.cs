using Quoter.Shared.Enums;

namespace Quoter.Framework.Data.Entities
{
    /// <summary>
    /// Represents a collection that holds books or quotes
    /// </summary>
    public class Collection
    {
        public int CollectionId { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public bool? IsFavourite { get; set; }

        /// <summary>
        /// The language of the collection. Normally it would be the app language
        /// under which the collection was added.
        /// </summary>
        public EnumLanguage? Language { get; set; }

        #region FK

        public List<Book> LstBooks { get; set; }

        public List<Quote> LstQuotes { get; set; }

        #endregion FK

    }
}
