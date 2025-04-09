using Quoter.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Quoter.Web.ViewModels.AppVersions
{
	/// <summary>
	/// View model for <see cref="Pages.AppVersions.EditModel"/>
	/// </summary>
	public class EditViewModel
	{
		[Required]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Version { get; set; }

		public bool IsAvailable { get; set; }

		public string? Description { get; set; }

		public EnumOperatingSystem Os { get; set; }

		public string Path { get; set; }
	}
}
