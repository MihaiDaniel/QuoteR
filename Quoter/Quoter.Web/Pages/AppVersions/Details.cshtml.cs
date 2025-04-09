using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Web.Data;
using Quoter.Web.Services;
using Quoter.Web.ViewModels.AppVersions;

namespace Quoter.Web.Pages.AppVersions
{
	[Authorize]
	public class DetailsModel : PageModel
	{
		private readonly ApplicationDbContext _context;
		private readonly IAppVersionService _appVersionService;

		public DetailsViewModel ViewModel { get; set; } = default!;

		public DetailsModel(ApplicationDbContext context, IAppVersionService appVersionService)
		{
			_context = context;
			_appVersionService = appVersionService;
		}

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (!await _context.AppVersions.AnyAsync(av => av.Id == id))
			{
				return BadRequest();
			}

			ViewModel = await _context.AppVersions
				.Include(v => v.LstAppVersionDownloads)
				.Where(v => v.Id == id)
				.Select(v => new DetailsViewModel()
				{
					Id = v.Id,
					Name = v.Name,
					Version = v.Version,
					VersionType = v.Type,
					Description = v.Description,
					Os = v.Os,
					CreationDate = v.CreationDate,
					IsAvailable = v.IsReleased,
					Path = v.Path,
					VersionDownloads = v.LstAppVersionDownloads.Count
				})
				.FirstAsync(m => m.Id == id);

			return Page();
		}

	}
}
