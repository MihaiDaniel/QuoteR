using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quoter.Web.Data.Entities
{
	/// <summary>
	/// Editable values displayed on the home page. Only one row is used.
	/// </summary>
	public class HomePageParameter
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }

		/// <summary>
		/// Microsoft Store listing URL. Null or empty when not set yet.
		/// </summary>
		public string? StoreUrl { get; set; }

		/// <summary>
		/// Number of downloads shown on the home page. Null or 0 means not available.
		/// </summary>
		public int? DownloadsCount { get; set; }

		/// <summary>
		/// Number of bible translations shown on the home page. Null or 0 means not available.
		/// </summary>
		public int? BibleTranslationsCount { get; set; }
	}
}
