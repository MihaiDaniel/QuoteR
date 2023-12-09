using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Quoter.Shared.Enums;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;
using System.IO;

namespace Quoter.Web.Pages
{
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
			catch(Exception ex)
			{
				_logger.LogError(ex, "An error occured while user tried to download latest version from website.");
				return BadRequest();
			}
		}
	}
}