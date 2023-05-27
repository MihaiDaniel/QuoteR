using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Shared.Enums;
using Quoter.Web.Data.Entities;

namespace Quoter.Web.Pages.AppVersions
{
	[Authorize]
	public class EditModel : PageModel
	{
		private readonly Quoter.Web.Data.ApplicationDbContext _context;

		public EditModel(Quoter.Web.Data.ApplicationDbContext context)
		{
			_context = context;
		}

		[BindProperty]
		public AppVersionModel ViewModel { get; set; } = default!;

		public class AppVersionModel
		{
			[Required]
			public Guid Id { get; set; }

			[Required]
			public string Name { get; set; }

			[Required]
			public string Version { get; set; }

			public string? Description { get; set; }

			public EnumOperatingSystem Os { get; set; }

			public string Path { get; set; }
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
			ViewModel = new()
			{
				Id = appVersion.Id,
				Name = appVersion.Name,
				Version = appVersion.Version,
				Description = appVersion.Description,
				Os = appVersion.Os,
				Path = appVersion.Path
			};
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

				if(appVersion == null)
				{
					return NotFound();
				}

				appVersion.Name = ViewModel.Name;
				appVersion.Version = ViewModel.Version;
				appVersion.Os = ViewModel.Os;
				appVersion.Description = ViewModel.Description;

				await _context.SaveChangesAsync();

				return RedirectToPage("./Index");
			}
			catch(Exception ex)
			{
				throw;
			}
		}
	}
}
