using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Shared.Enums;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;
using Quoter.Web.Services;
using Quoter.Web.ViewModels.AppVersions;

namespace Quoter.Web.Pages.AppVersions
{
	[Authorize]
	public class CreateModel : PageModel
	{
		private readonly IFileVersionsService _fileVersionsService;
		private readonly ApplicationDbContext _context;

		[BindProperty]
		public CreateViewModel ViewModel { get; set; } = default!;

		public CreateModel(ApplicationDbContext context, IFileVersionsService fileVersionsService)
		{
			_context = context;
			_fileVersionsService = fileVersionsService;
		}

		public IActionResult OnGet()
		{
			return Page();
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
				bool isValid = await ValidatePostAsync();
				if (!isValid)
				{
					return Page();
				}

				string fileName = GetFileName();
				string filePath = await _fileVersionsService.SaveFileVersionAsync(ViewModel.File, fileName);

				AppVersion newVersion = new()
				{
					Name = ViewModel.Name,
					Version = ViewModel.Version,
					Description = ViewModel.Description,
					Type = ViewModel.VersionType,
					Os = ViewModel.Os,
					Path = filePath,
					CreationDate = DateTime.UtcNow
				};
				_context.AppVersions.Add(newVersion);
				await _context.SaveChangesAsync();

				return RedirectToPage("./Index");
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		private string GetFileName()
		{
			switch (ViewModel.VersionType)
			{
				case EnumVersionType.UpdateZip:
					return $"{ViewModel.Version}.zip";
				case EnumVersionType.Installer:
					return $"Quoter installer {ViewModel.Version}.exe";
				default:
					return ViewModel.File.FileName;
			}
		}

		private async Task<bool> ValidatePostAsync()
		{
			bool isDuplicateName = await _context.AppVersions.AnyAsync(v => v.Name == ViewModel.Name);
			if (isDuplicateName)
			{
				ModelState.TryAddModelError(nameof(CreateViewModel.Name), "A version with the same name already exists");
				return false;
			}
			bool isDuplicateVersion = await _context.AppVersions
				.AnyAsync(v => v.Version == ViewModel.Version
							&& v.Type == ViewModel.VersionType);
			if (isDuplicateVersion)
			{
				ModelState.TryAddModelError(nameof(CreateViewModel.Version), "A version with the same version and version type already exists");
				ModelState.TryAddModelError(nameof(CreateViewModel.VersionType), "A version with the same version and version type already exists");
				return false;
			}
			return true;
		}
	}
}
