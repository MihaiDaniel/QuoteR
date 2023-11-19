using Quoter.Shared.Enums;

namespace Quoter.Web.ViewModels.AppVersions
{
	/// <summary>
	/// View model for <see cref="Pages.AppVersions.DetailsModel"/>
	/// </summary>
	public class DetailsViewModel
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Version { get; set; }

		public string? Description { get; set; }

		public EnumOperatingSystem Os { get; set; }

		public EnumVersionType VersionType { get; set; }

		public string Path { get; set; }

		public bool IsAvailable { get; set; }

		public DateTime CreationDate { get; set; }

		public int VersionDownloads { get; set; }
	}
}
