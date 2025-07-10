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
		private readonly IFileUploadService fileUploadService;

		public VersionsApiController(ApplicationDbContext context, ILoggerFactory loggerFactory, IMemoryCache memoryCache, IFileUploadService fileVersionsService) : base(context)
		{
			_context = context;
			_memoryCache = memoryCache;
			fileUploadService = fileVersionsService;
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
						.Where(v => v.IsReleased && v.Type == EnumVersionType.UpdaterZipPackage)
						.OrderBy(v => v.Id)
						.Select(v => new QuoterVersionInfo(v.PublicId, v.Version))
						.ToListAsync();

					if(lstAllReleasedUpdates.Count == 0)
					{
						_logger.LogWarning("An attempt was made to get the latest version info, but no updates were found.");
						return NotFound("No updates available");
					}

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
					_logger.LogWarning("An attempt was made to get the latest version info without any registration specified. Registration:{0}", GetRequestRegistration());
					return BadRequest("No registration specified");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An exception occured when getting the latest version info");
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
					_logger.LogWarning("An attempt was made to download a version without any registration specified. Registration:{0}", GetRequestRegistration());
					return BadRequest("No registration specified");
				}

				AppVersion? appVersion = await _context.AppVersions
					.FirstOrDefaultAsync(v => v.PublicId == versionId);

				if (appVersion is not null)
				{
					// Log a new version download by the client
					AppVersionDownload newDownload = new()
					{
						AppRegistrationId = await GetAppRegistrationId(),
						AppVersionId = appVersion.Id
					};
					_context.AppVersionDownloads.Add(newDownload);
					await _context.SaveChangesAsync();

					// Get from cache or by reading from file the content
					VersionFileContent? fileContent = await _memoryCache.GetOrCreateAsync(appVersion.Version,
						async entry =>
						{
							entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2);

							return await fileUploadService.GetVersionFileContentAsync(appVersion);
						});
					
					ArgumentNullException.ThrowIfNull(fileContent);
					return File(new MemoryStream(fileContent.Content), "application/zip", fileContent.FileName);
				}
				else
				{
					_logger.LogWarning("An attempt was made to download a version with an invalid id. PublicId:{0}", versionId);
					return BadRequest("Invalid versionId");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An exception occured while a client attepted to download version with PublicId:{0}", versionId);
				return GetInternalServerErrorResponse();
			}
		}
	}
}
