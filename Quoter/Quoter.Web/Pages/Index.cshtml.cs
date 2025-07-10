using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Quoter.Shared.Enums;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;

namespace Quoter.Web.Pages
{
	/// <summary>
	/// Home page of the website
	/// </summary>
	public class IndexModel : PageModel
	{
		private readonly ILogger _logger;
		private readonly ApplicationDbContext _context;
		private readonly IMemoryCache _memoryCache;
		private readonly SignInManager<ApplicationUser> _signInManager;

		/// <summary>
		/// Indicates if any setup file is available for download
		/// <see cref="AppVersion.IsReleased"/> = True and <see cref="AppVersion.Type"/> is <see cref="EnumVersionType.Installer"/>
		/// </summary>
		public bool IsDownloadAvailable { get; set; } = false;

		/// <summary>
		/// The total number of downloads of any of the versions
		/// </summary>
		public int DownloadsCount { get; set; } = 0;

		public string CurrentCulture { get; set; } = "en-US";

		public IndexModel(
			ILoggerFactory loggerFactory,
			ApplicationDbContext context,
			IMemoryCache memoryCache,
			SignInManager<ApplicationUser> signInManager)
		{
			_logger = loggerFactory.CreateLogger("Index");
			_context = context;
			_memoryCache = memoryCache;
			_signInManager = signInManager;
		}

		public async Task OnGet()
		{
			CurrentCulture = Request.HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.Name ?? "en-US";
			IsDownloadAvailable = await _memoryCache.GetOrCreateAsync("IsDownloadAvailable",
				async entry =>
			{
				entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15);
				return await _context.AppVersions
					.Where(v => v.IsReleased && v.Type == EnumVersionType.Installer)
					.AnyAsync();
			});

			if(!IsDownloadAvailable)
			{
				_logger.LogInformation("No download available for the latest version.");
			}

			DownloadsCount = await _memoryCache.GetOrCreateAsync("DownloadsCount",
				async entry =>
				{
					entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60);
					return await _context.AppVersionDownloads.CountAsync();
				});


			_context.Visits.Add(new Visit()
			{
				IpAddress = Request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
				Url = Request.Path,
				VisitDate = DateTime.UtcNow,
			});
			await _context.SaveChangesAsync();
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
						AppVersion = latestVersion,
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
		/// Sets the culture of the client in a cookie. Used to chance the website language
		/// </summary>
		/// <param name="culture">Culture to change language to (ex: en-US)</param>
		/// <param name="returnUrl">In case we are on different page to return to the same page</param>
		public IActionResult OnPostSetCulture(string culture, string returnUrl)
		{
			Response.Cookies.Append(
				CookieRequestCultureProvider.DefaultCookieName,
				CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
				new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

			return LocalRedirect(returnUrl);
		}

		public async Task<IActionResult> OnPostLogoutAsync(string returnUrl)
		{
			await _signInManager.SignOutAsync();
			_logger.LogInformation("User logged out.");
			if (returnUrl != null)
			{
				return LocalRedirect(returnUrl);
			}
			else
			{
				// This needs to be a redirect so that the browser performs a new
				// request and the identity for the user gets updated.
				return RedirectToPage();
			}
		}

		/// <summary>
		/// Retrieves the latest released installer version and verifies file existence.
		/// </summary>
		private async Task<AppVersion> GetLatestVersionAsync()
		{
			AppVersion? latestVersion = await _memoryCache.GetOrCreateAsync("WebLatestVersion", async entry =>
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