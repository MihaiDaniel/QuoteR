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

		public Guid? AppRegistrationId { get; set; }

		public AppRegistration? AppRegistration { get; set; }

		public Guid? AppVersionId { get; set; }

		public AppVersion? AppVersion { get; set; }

		public DateTime DownloadDateTime { get; set; }

		#endregion FK

		public AppVersionDownload()
		{
			DownloadDateTime = DateTime.UtcNow;
		}
	}
}
