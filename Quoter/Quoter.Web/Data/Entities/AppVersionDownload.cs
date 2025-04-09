using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quoter.Web.Data.Entities
{
	public class AppVersionDownload
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }

		#region FK

		public int? AppRegistrationId { get; set; }

		public AppRegistration? AppRegistration { get; set; }

		public int? AppVersionId { get; set; }

		public AppVersion? AppVersion { get; set; }

		#endregion FK

		public DateTime DownloadDateTime { get; set; }

		public AppVersionDownload()
		{
			DownloadDateTime = DateTime.UtcNow;
		}
	}
}
