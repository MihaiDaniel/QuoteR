using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;
using Quoter.Web.Services;

namespace Quoter.Web.Pages.AppVersions
{
	[Authorize]
	public class IndexModel : PageModel
	{
		private readonly ApplicationDbContext _context;
		private readonly IFileVersionsService _fileVersionsService;
		private readonly ILogger _logger;

		public IList<AppVersion> AppVersion { get; set; }

		public int TotalRecords { get; set; } = 0;

		[BindProperty(SupportsGet = true)]
		public int PageNo { get; set; } = 1;

		[BindProperty(SupportsGet = true)]
		public int PageSize { get; set; } = 10;

		public IndexModel(ApplicationDbContext context, IFileVersionsService fileVersionsService, ILoggerFactory loggerFactory)
		{
			_context = context;
			_fileVersionsService = fileVersionsService;
			AppVersion = new List<AppVersion>();
			_logger = loggerFactory.CreateLogger<IndexModel>();
		}

		public async Task OnGetAsync()
		{
			TotalRecords = await _context.AppVersions.CountAsync();

			AppVersion = await _context.AppVersions
				.AsNoTracking()
				.Include(v => v.LstAppVersionDownloads)
				.Skip((PageNo - 1) * PageSize)
				.Take(PageSize)
				.OrderByDescending(v => v.CreationDate)
				.ToListAsync();
		}

		public async Task<IActionResult> OnPostDeleteAsync(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			try
			{
				AppVersion? appVersion = await _context.AppVersions.FirstOrDefaultAsync(v => v.Id == id);

				if (appVersion != null)
				{
					//_context.AppVersionDownloads

					_fileVersionsService.Delete(appVersion.Path);
					_context.AppVersions.Remove(appVersion);
					await _context.SaveChangesAsync();
				}

				return RedirectToPage();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Could not delete AppVersion {0}", id);
				throw;
			}

		}
	}
}
