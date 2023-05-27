using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Shared.Enums;
using Quoter.Web.Data.Entities;
using Quoter.Web.Services;

namespace Quoter.Web.Pages.AppVersions
{
	[Authorize]
	public class CreateModel : PageModel
	{
		private readonly IFileVersionsService _fileVersionsService;
		private readonly Data.ApplicationDbContext _context;

		public CreateModel(Data.ApplicationDbContext context, IFileVersionsService fileVersionsService)
		{
			_context = context;
			_fileVersionsService = fileVersionsService;
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		[BindProperty]
		public AppVersionModel ViewModel { get; set; } = default!;

		public class AppVersionModel
		{
			[Required]
			public string Name { get; set; }

			[Required]
			public string Version { get; set; }

			public string? Description { get; set; }

			public EnumOperatingSystem Os { get; set; }

			[Required]
			public IFormFile File { get; set; }
		}


		// To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid || _context.AppVersions == null || ViewModel == null)
			{
				return Page();
			}
			try
			{
				if( await _context.AppVersions.AnyAsync(v => v.Name == ViewModel.Name || v.Version == ViewModel.Version))
				{
					return Page();
				}

				string filePath = await _fileVersionsService.SaveFileVersionAsync(ViewModel.File, ViewModel.Version);

				AppVersion newVersion = new()
				{
					Name = ViewModel.Name,
					Version = ViewModel.Version,
					Description = ViewModel.Description,
					Os = ViewModel.Os,
					Path = filePath,
					CreationDate = DateTime.UtcNow
				};
				_context.AppVersions.Add(newVersion);
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
