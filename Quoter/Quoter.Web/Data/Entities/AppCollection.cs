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

		[Display(Name = "Name")]
		public string Name { get; set; }

		[Display(Name = "Description")]
		public string Description { get; set; }

		public string Path { get; set; }

		[Display(Name = "Language")]
		public EnumLanguage Language { get; set; }

		[Display(Name = "Upload date time")]
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
