using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Quoter.Web.Data.Entities;
using Quoter.Web.Data;
using Quoter.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace Quoter.Web.Pages
{
	public class DownloadModel : PageModel
	{
		private readonly ILogger _logger;
		private readonly ApplicationDbContext _context;
		private readonly IMemoryCache _memoryCache;

		public DownloadModel(
			ILoggerFactory loggerFactory,
			ApplicationDbContext context,
			IMemoryCache memoryCache)
		{
			_logger = loggerFactory.CreateLogger("Download");
			_context = context;
			_memoryCache = memoryCache;
		}

		public void OnGet()
		{
		}

		/// <summary>
		/// Returns the file for the latest version of the application installer
		/// </summary>
		public async Task<ActionResult> OnGetDownloadApp()
		{
			try
			{
				AppVersion latestVersion = await GetLatestVersionAsync();
				byte[] bytes = System.IO.File.ReadAllBytes(latestVersion.Path);

				_context.AppVersionDownloads
					.Add(new AppVersionDownload
					{
						AppVersionId = latestVersion.Id,
						DownloadDateTime = DateTime.UtcNow,
					});
				await _context.SaveChangesAsync();

				return File(bytes, "application/octet-stream", latestVersion.OriginalFileName);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occured while a user tried to download latest version from website.");
				return BadRequest();
			}
		}

		/// <summary>
		/// Retrieves the latest released installer version and verifies file existence.
		/// </summary>
		private async Task<AppVersion> GetLatestVersionAsync()
		{
			AppVersion? latestVersion = await _memoryCache.GetOrCreateAsync(Constants.MemoryCacheKeys.LatestAppVersionForDownload, async entry =>
			{
				entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60);
				entry.SlidingExpiration = TimeSpan.FromMinutes(5);
				return await _context.AppVersions
					.Where(v => v.IsReleased && (v.Type == EnumVersionType.Installer || v.Type == EnumVersionType.ZipArchive))
					.OrderBy(v => v.Id)
					.LastOrDefaultAsync();
			});

			if (latestVersion == null)
			{
				throw new ArgumentException("Latest version is null.");
			}
			if (!System.IO.File.Exists(latestVersion.Path))
			{
				throw new ArgumentException($"File does not exist {latestVersion.Path}");
			}
			return latestVersion;
		}
	}
}
