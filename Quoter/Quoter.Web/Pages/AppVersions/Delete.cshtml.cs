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
	public class DeleteModel : PageModel
	{
		private readonly IFileVersionsService _fileVersionsService;
		private readonly ApplicationDbContext _context;

		[BindProperty]
		public AppVersion AppVersion { get; set; } = default!;

		public DeleteModel(ApplicationDbContext context, IFileVersionsService fileVersionsService)
		{
			_context = context;
			_fileVersionsService = fileVersionsService;
		}

		public async Task<IActionResult> OnGetAsync(Guid? id)
		{
			if (id == null || _context.AppVersions == null)
			{
				return NotFound();
			}

			AppVersion? appVersion = await _context.AppVersions.FirstOrDefaultAsync(m => m.Id == id);

			if (appVersion == null)
			{
				return NotFound();
			}
			else
			{
				AppVersion = appVersion;
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(Guid? id)
		{
			if (id == null || _context.AppVersions == null)
			{
				return BadRequest();
			}
			try
			{
				AppVersion? appVersion = await _context.AppVersions.FindAsync(id);

				if (appVersion != null)
				{
					_fileVersionsService.Delete(appVersion.Path);

					AppVersion = appVersion;
					_context.AppVersions.Remove(AppVersion);
					await _context.SaveChangesAsync();
				}

				return RedirectToPage("./Index");
			}
			catch(Exception ex)
			{
				throw;
			}
			
		}
	}
}
