using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Shared.Enums;

namespace Quoter.Web.Pages.AppVersions
{
	[Authorize]
	public class DetailsModel : PageModel
	{
		private readonly Quoter.Web.Data.ApplicationDbContext _context;

		public DetailsModel(Quoter.Web.Data.ApplicationDbContext context)
		{
			_context = context;
		}

		public AppVersionModel ViewModel { get; set; } = default!;

		public class AppVersionModel
		{
			public Guid Id { get; set; }

			public string Name { get; set; }

			public string Version { get; set; }

			public string? Description { get; set; }

			public EnumOperatingSystem Os { get; set; }

			public string Path { get; set; }

			public DateTime CreationDate { get; set; }

			public int VersionDownloads { get; set; }

		}


		public async Task<IActionResult> OnGetAsync(Guid? id)
		{
			if (id == null || _context.AppVersions == null)
			{
				return NotFound();
			}

			ViewModel = await _context.AppVersions
				.Include(v => v.LstAppVersionDownloads)
				.Where(v => v.Id == id)
				.Select(v => new AppVersionModel()
				{
					Id = v.Id,
					Name = v.Name,
					Version = v.Version,
					Description = v.Description,
					Os = v.Os,
					CreationDate = v.CreationDate,
					Path = v.Path,
					VersionDownloads = v.LstAppVersionDownloads.Count
				})
				.FirstOrDefaultAsync(m => m.Id == id);
			if (ViewModel == null)
			{
				return NotFound();
			}
			return Page();
		}
	}
}
