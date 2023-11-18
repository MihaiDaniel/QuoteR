using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;
using Quoter.Web.Services;
using Quoter.Web.ViewModels.AppVersions;

namespace Quoter.Web.Pages.AppVersions
{
	[Authorize]
	public class EditModel : PageModel
	{
		private readonly ApplicationDbContext _context;
		private readonly IAppVersionService _appVersionService;

		[BindProperty]
		public EditViewModel ViewModel { get; set; } = default!;

		public EditModel(ApplicationDbContext context, IAppVersionService appVersionService)
		{
			_context = context;
			_appVersionService = appVersionService;
		}

		public async Task<IActionResult> OnGetAsync(Guid? id)
		{
			if (!await _appVersionService.IsAppVersionIdValid(id))
			{
				return BadRequest();
			}

			ViewModel = await _context.AppVersions
				.Where(appVersion => appVersion.Id == id)
				.Select(appVersion => new EditViewModel()
				{
					Id = appVersion.Id,
					Name = appVersion.Name,
					Version = appVersion.Version,
					Description = appVersion.Description,
					Os = appVersion.Os,
					Path = appVersion.Path,
					IsAvailable = appVersion.IsAvailable,
				})
				.FirstAsync();

			return Page();
		}

		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see https://aka.ms/RazorPagesCRUD.
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			try
			{
				AppVersion? appVersion = _context.AppVersions.FirstOrDefault(a => a.Id == ViewModel.Id);
				if (appVersion == null)
				{
					return BadRequest();
				}

				appVersion.Name = ViewModel.Name;
				appVersion.Version = ViewModel.Version;
				appVersion.IsAvailable = ViewModel.IsAvailable;
				appVersion.Os = ViewModel.Os;
				appVersion.Description = ViewModel.Description;

				await _context.SaveChangesAsync();

				return RedirectToPage("./Index");
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
