using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Shared.Enums;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;
using Quoter.Web.Services;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Quoter.Web.Pages.AppVersions
{
	[Authorize]
	public class CreateModel : PageModel
	{
		private readonly ILogger<CreateModel> _logger;
		private readonly IFileVersionsService _fileVersionsService;
		private readonly ApplicationDbContext _context;

		[BindProperty]
		[Required]
		public string Name { get; set; }

		[BindProperty]
		[Required]
		public string Version { get; set; }

		[BindProperty]
		public string? Description { get; set; }

		[BindProperty]
		public EnumOperatingSystem Os { get; set; }

		[BindProperty]
		[Required]
		public EnumVersionType VersionType { get; set; }

		[BindProperty]
		[Required]
		public IFormFile FileUpload { get; set; }

		public CreateModel(
			ILogger<CreateModel> logger,
			ApplicationDbContext context,
			IFileVersionsService fileVersionsService)
		{
			_logger = logger;
			_context = context;
			_fileVersionsService = fileVersionsService;
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
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
				string filePath = await _fileVersionsService.SaveFileVersionUploadAsync(FileUpload, fileName);

				AppVersion newVersion = new()
				{
					Name = Name,
					Version = Version,
					Description = Description,
					Type = VersionType,
					Os = Os,
					Path = filePath,
					CreationDate = DateTime.UtcNow
				};
				_context.AppVersions.Add(newVersion);
				await _context.SaveChangesAsync();

				return RedirectToPage("./Index");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occured while trying to upload and save an update file.");
				throw;
			}
		}

		private string GetFileName()
		{
			switch (VersionType)
			{
				case EnumVersionType.UpdateZip:
					if (FileUpload.FileName.Contains(".zip"))
					{
						return FileUpload.FileName;
					}
					return $"{Version}.zip";
				default:
					return FileUpload.FileName;
			}
		}

		private async Task<bool> ValidatePostAsync()
		{
			Regex regexVersionFormat = new("^\\d+\\.\\d+\\.\\d+\\.\\d+$");
			if (!regexVersionFormat.IsMatch(Version))
			{
				ModelState.TryAddModelError($"{nameof(Version)}", "Version format is incorrect, example correct format: 1.2.3.4");
			}

			bool isDuplicateVersion = await _context.AppVersions
				.AnyAsync(v => v.Version == Version
							&& v.Type == VersionType);
			if (isDuplicateVersion)
			{
				ModelState.TryAddModelError($"{nameof(Version)}", "A version with the same version and version type already exists");
				ModelState.TryAddModelError($"{nameof(VersionType)}", "A version with the same version and version type already exists");
				return false;
			}
			return true;
		}
	}
}
