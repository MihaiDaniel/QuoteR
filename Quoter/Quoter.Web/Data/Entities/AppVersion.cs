using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Quoter.Shared.Enums;

namespace Quoter.Web.Data.Entities
{
	/// <summary>
	/// This entity holds information about application versions of Quoter desktop application (update or installer).
	/// </summary>
	/// <remarks>
	/// This could be downloaded by the application for auto updates or through the website.
	/// </remarks>
	public class AppVersion
	{
		/// <summary>
		/// PK
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		/// <summary>
		/// A name describing this version
		/// </summary>
		[Required]
		public string Name { get; set; }

		/// <summary>
		/// The version in format Major.Minor.Build.Revision (eg: 1.2.6.0)
		/// </summary>
		[Required]
		public string Version { get; set; }

		/// <summary>
		/// A short description of the version (eg. change log)
		/// </summary>
		public string? Description { get; set; }
		
		/// <summary>
		/// In case of different installers / updates for different Operating systems, use this
		/// property to differentiate
		/// </summary>
		public EnumOperatingSystem Os { get; set; }

		/// <summary>
		/// Version / FileType of this AppVersion.
		/// </summary>
		[Required]
		public EnumVersionType Type { get; set; }

		/// <summary>
		/// Indicates if the update is available.
		/// App versions that are not available will not be downloadable by the
		/// desktop application
		/// </summary>
		public bool IsAvailable { get; set; }

		/// <summary>
		/// Path on disk of the file
		/// </summary>
		public string Path { get; set; }

		/// <summary>
		/// DateTime when the version was created
		/// </summary>
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
