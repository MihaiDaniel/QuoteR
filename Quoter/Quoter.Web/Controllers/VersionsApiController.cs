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
		private readonly IMemoryCache _memoryCache;
		private readonly IFileVersionsService _fileVersionsService;

		public VersionsApiController(ApplicationDbContext context, IMemoryCache memoryCache, IFileVersionsService fileVersionsService) : base(context)
		{
			_context = context;
			_memoryCache = memoryCache;
			_fileVersionsService = fileVersionsService;
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
					List<QuoterVersionInfo> lstQuoterVersions = await _context.AppVersions
						.Where(v => v.IsAvailable && v.Type == EnumVersionType.UpdateZip)
						.OrderBy(v => v.CreationDate)
						.Select(v => new QuoterVersionInfo(v.Id, v.Version))
						.ToListAsync();

					QuoterVersionInfo latest = lstQuoterVersions.Last();
					foreach (var quoterVersion in lstQuoterVersions)
					{
						if (latest.CompareWith(quoterVersion) == EnumVersionCompare.Older)
						{
							latest = quoterVersion;
						}
					}
					return Ok(new LatestVersionInfoGetResponse(latest.Id, latest.ToString()));
				}
				else
				{
					return BadRequest("No registration specified");
				}
			}
			catch (Exception ex)
			{
				return GetInternalServerErrorResponse();
			}
		}

		/// <summary>
		/// Download a version file based on it's id.
		/// </summary>
		[Route("downloadVersion")]
		[HttpGet]
		public async Task<IActionResult> DownloadVersion([FromQuery] Guid versionId)
		{
			try
			{
				if (!await IsClientRegistered())
				{
					return BadRequest("No registration specified");
				}

				AppVersion? appVersion = await _context.AppVersions
					.FirstOrDefaultAsync(v => v.Id == versionId);

				if (appVersion != null)
				{
					// Log a new version download by the client
					AppVersionDownload appVersionDownload = new()
					{
						AppRegistrationId = GetClientRegistrationId(),
						AppVersionId = appVersion.Id,
						DownloadDateTime = DateTime.UtcNow,
					};
					_context.AppVersionDownloads.Add(appVersionDownload);
					await _context.SaveChangesAsync();

					// Get from cache or by reading from file the content
					VersionFile file = await _memoryCache.GetOrCreateAsync(appVersion.Version,
						async entry =>
						{
							entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);

							return await _fileVersionsService.GetVersionFileAsync(appVersion);
						});
					return File(new MemoryStream(file.Content), "application/zip", file.FileName);
				}
				else
				{
					return BadRequest("Invalid versionId");
				}
			}
			catch (Exception ex)
			{
				return GetInternalServerErrorResponse();
			}
		}
	}
}
