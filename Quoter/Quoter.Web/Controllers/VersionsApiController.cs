using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Quoter.Shared.Enums;
using Quoter.Shared.Models;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;
using Quoter.Web.Models;
using Quoter.Web.Services;

namespace Quoter.Web.Controllers
{
	[Route("api/versions")]
	[ApiController]
	public class VersionsApiController : BaseApiController
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger _logger;
		private readonly IMemoryCache _memoryCache;
		private readonly IFileVersionsService _fileVersionsService;

		public VersionsApiController(ApplicationDbContext context, ILoggerFactory loggerFactory, IMemoryCache memoryCache, IFileVersionsService fileVersionsService) : base(context)
		{
			_context = context;
			_memoryCache = memoryCache;
			_fileVersionsService = fileVersionsService;
			_logger = loggerFactory.CreateLogger<VersionsApiController>();
		}

		/// <summary>
		/// Returns information about the latest version available for download.
		/// The version information is only for updates of already installed application.
		/// </summary>
		/// <remarks>
		/// Normally this is used by the app to verify if it's up to date.
		/// </remarks>
		[Route("getLatestVersionInfo")]
		[HttpGet]
		public async Task<IActionResult> GetLatestVersion()
		{
			try
			{
				if (await IsClientRegistered())
				{
					List<QuoterVersionInfo> lstAllReleasedUpdates = await _context.AppVersions
						.Where(v => v.IsReleased && v.Type == EnumVersionType.UpdateZip)
						.OrderBy(v => v.Id)
						.Select(v => new QuoterVersionInfo(v.PublicId, v.Version))
						.ToListAsync();

					// Normally newer versions should be added lastly, but just in case we compare them
					QuoterVersionInfo latestVersion = lstAllReleasedUpdates.Last();
					foreach (var versionToCompare in lstAllReleasedUpdates)
					{
						if (latestVersion.IsOlderThan(versionToCompare))
						{
							latestVersion = versionToCompare;
						}
					}
					return Ok(new LatestVersionInfoGetResponse(latestVersion.PublicId, latestVersion.ToString()));
				}
				else
				{
					return BadRequest("No registration specified");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occured on GetLatestVersion");
				return GetInternalServerErrorResponse();
			}
		}

		/// <summary>
		/// Download a version file based on it's public id.
		/// </summary>
		[Route("downloadVersion")]
		[HttpGet]
		public async Task<IActionResult> DownloadVersion([FromQuery] string versionId)
		{
			try
			{
				if (!await IsClientRegistered())
				{
					return BadRequest("No registration specified");
				}

				AppVersion? appVersion = await _context.AppVersions
					.FirstOrDefaultAsync(v => v.PublicId == versionId);

				if (appVersion is not null)
				{
					// Log a new version download by the client
					AppVersionDownload appVersionDownload = new()
					{
						AppRegistrationId = await GetAppRegistrationId(),
						AppVersionId = appVersion.Id
					};
					_context.AppVersionDownloads.Add(appVersionDownload);
					await _context.SaveChangesAsync();

					// Get from cache or by reading from file the content
					VersionFileContent? fileContent = await _memoryCache.GetOrCreateAsync(appVersion.Version,
						async entry =>
						{
							entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2);

							return await _fileVersionsService.GetVersionFileContentAsync(appVersion);
						});
					
					ArgumentNullException.ThrowIfNull(fileContent);
					return File(new MemoryStream(fileContent.Content), "application/zip", fileContent.FileName);
				}
				else
				{
					return BadRequest("Invalid versionId");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occured on DownloadVersion");
				return GetInternalServerErrorResponse();
			}
		}
	}
}
