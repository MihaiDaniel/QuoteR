using Quoter.Shared.Models;

namespace Quoter.Framework.Services.Api
{
	public interface IWebApiService
	{
		/// <summary>
		/// Register the application and get back an registrationId as a Guid from the web server
		/// </summary>
		Task<Guid> RegisterAsync(string installId, string applicationKey, string localWinRegionCode);

		/// <summary>
		/// Request the info about the latest version of the application from the web server
		/// </summary>
		Task<QuoterVersionInfo> GetLatestVersion();

		/// <summary>
		/// Downloads a specific version file from the web server locally.
		/// </summary>
		Task<ActionResult> DownloadVersionAsync(Guid versionId);
	}
}
