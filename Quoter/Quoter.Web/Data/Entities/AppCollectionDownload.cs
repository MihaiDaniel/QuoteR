using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Quoter.Web.Data.Entities
{
	public class AppCollectionDownload
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }

		#region FK

		public Guid? AppRegistrationId { get; set; }

		public AppRegistration? AppRegistration { get; set; }

		public int? AppCollectionId { get; set; }

		public AppCollection? AppCollection { get; set; }

		public DateTime DownloadDateTime { get; set; }

		#endregion FK

		public AppCollectionDownload()
		{
			DownloadDateTime = DateTime.UtcNow;
		}
	}
}
