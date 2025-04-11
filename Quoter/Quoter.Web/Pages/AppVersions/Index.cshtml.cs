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
		private readonly IFileUploadService _fileUploadService;
		private readonly ILogger _logger;

		public IList<AppVersion> AppVersions { get; set; }

		public int TotalRecords { get; set; } = 0;

		[BindProperty(SupportsGet = true)]
		public int PageNo { get; set; } = 1;

		[BindProperty(SupportsGet = true)]
		public int PageSize { get; set; } = 10;

		public IndexModel(ApplicationDbContext context, IFileUploadService fileVersionsService, ILoggerFactory loggerFactory)
		{
			_context = context;
			_fileUploadService = fileVersionsService;
			_logger = loggerFactory.CreateLogger("AppVersions.Index");
			AppVersions = new List<AppVersion>();
		}

		public async Task OnGetAsync()
		{
			TotalRecords = await _context.AppVersions.CountAsync();

			AppVersions = await _context.AppVersions
				.AsNoTracking()
				.Include(v => v.LstAppVersionDownloads)
				.OrderByDescending(v => v.CreationDate)
				.Skip((PageNo - 1) * PageSize)
				.Take(PageSize)
				.ToListAsync();
		}

		public async Task<IActionResult> OnPostDeleteAsync(int id)
		{
			try
			{
				AppVersion? appVersion = await _context.AppVersions.FirstOrDefaultAsync(v => v.Id == id);

				if (appVersion is null)
				{
					_logger.LogWarning("An attempt was made to delete {entity} with an invalid {property}={value}", nameof(AppVersion), nameof(AppVersion.Id), id);
					return BadRequest();
				}
				else
				{
					_fileUploadService.Delete(appVersion.Path);
					_context.AppVersions.Remove(appVersion);
					await _context.SaveChangesAsync();
				}
				return RedirectToPage();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An exception occured while trying to delete {entity} with {property}={value}", nameof(AppVersion), nameof(AppVersion.Id), id);
				return StatusCode(500);
			}
		}
	}
}
