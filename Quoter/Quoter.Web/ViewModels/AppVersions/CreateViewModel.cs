using Quoter.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Quoter.Web.ViewModels.AppVersions
{
	/// <summary>
	/// View model for <see cref="Pages.AppVersions.CreateModel"/>
	/// </summary>
	public class CreateViewModel
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public string Version { get; set; }

		public string? Description { get; set; }

		public EnumOperatingSystem Os { get; set; }

		[Required]
		public EnumVersionType VersionType { get; set; }

		[Required]
		public IFormFile File { get; set; }
	}
}
