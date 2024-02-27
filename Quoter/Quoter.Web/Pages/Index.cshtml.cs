﻿using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
		private readonly ILogger<IndexModel> _logger;
		private readonly ApplicationDbContext _context;
		private readonly IMemoryCache _memoryCache;

		public IndexModel(
			ILogger<IndexModel> logger,
			ApplicationDbContext context,
			IMemoryCache memoryCache)
		{
			_logger = logger;
			_context = context;
			_memoryCache = memoryCache;
		}

		public void OnGet()
		{
		}

		/// <summary>
		/// Returns the file for the latest version of the application installer
		/// </summary>
		public ActionResult OnGetDownloadApp()
		{
			try
			{
				AppVersion latestVersion = _memoryCache.GetOrCreate("WebLatestVersion",
				entry =>
				{
					entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);

					return _context.AppVersions
						.Where(v => v.IsAvailable && v.Type == EnumVersionType.Installer)
						.OrderBy(v => v.CreationDate)
						.First();
				})!;

				if (!System.IO.File.Exists(latestVersion.Path))
				{
					throw new ArgumentException($"File does not exist {latestVersion.Path}");
				}

				byte[] bytes = System.IO.File.ReadAllBytes(latestVersion.Path);
				return File(bytes, "application/octet-stream", latestVersion.Name);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occured while user tried to download latest version from website.");
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
	}
}