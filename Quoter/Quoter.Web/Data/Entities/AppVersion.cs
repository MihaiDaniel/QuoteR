using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Quoter.Shared.Enums;

namespace Quoter.Web.Data.Entities
{
	/// <summary>
	/// These represents a Quoter desktop application update.
	/// This could be downloaded by the application for auto updates.
	/// </summary>
	public class AppVersion
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Version { get; set; }

		public string? Description { get; set; }

		public EnumOperatingSystem Os { get; set; }

		/// <summary>
		/// Path on disk of the update file
		/// </summary>
		public string Path { get; set; }

		public DateTime CreationDate { get; set; }

		#region FK

		public List<AppVersionDownload> LstAppVersionDownloads { get; set; }

		#endregion FK

		public AppVersion()
		{
			CreationDate = DateTime.UtcNow;
		}
	}
}
