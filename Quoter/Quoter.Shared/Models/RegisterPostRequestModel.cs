using System.ComponentModel.DataAnnotations;

namespace Quoter.Shared.Models
{
	public class RegisterPostRequestModel
	{
		[Required]
		public string InstallId { get; set; }

		[Required]
		public string ApplicationKey { get; set; }
	}
}
