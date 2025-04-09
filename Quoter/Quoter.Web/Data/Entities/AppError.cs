using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Quoter.Web.Data.Entities
{
	public class AppError
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }

		public string Message { get; set; }

		public string Version { get; set; }

		public DateTime ReportedDateTime { get; set; }

		#region FK

		public int AppRegistrationId { get; set; }

		public AppRegistration AppRegistration { get; set; }

		#endregion FK

		public AppError()
		{
			ReportedDateTime = DateTime.UtcNow;
		}
	}
}
