using Quoter.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quoter.Web.Data.Entities
{
	/// <summary>
	/// Represents the available uploaded collections.
	/// These can be downloaded by the Quoter desktop application to import
	/// new collections
	/// </summary>
	public class AppCollection
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string Path { get; set; }

		public EnumLanguage Language { get; set; }

		public DateTime UploadDateTime { get; set; }

		#region FK

		public List<AppCollectionDownload> AppCollectionDownloads { get; set; }

		#endregion FK

		public AppCollection()
		{
			UploadDateTime = DateTime.UtcNow;
		}
	}
}
