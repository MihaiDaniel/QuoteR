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
		private readonly ILogger _logger;
		private readonly IFileUploadService _fileUploadService;
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
			ILoggerFactory loggerFactory,
			ApplicationDbContext context,
			IFileUploadService fileVersionsService)
		{
			_logger = loggerFactory.CreateLogger("AppVersions.Create");
			_context = context;
			_fileUploadService = fileVersionsService;
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			try
			{
				if (!ModelState.IsValid || !await IsPostValidAsync())
				{
					_logger.LogWarning("An attempt was made to create {entity} with invalid model", nameof(AppVersion));
					return Page();
				}

				string fileName = _fileUploadService.GenerateUniqueFileName(FileUpload.FileName, EnumFileUploadType.AppVersion);
				string filePath = await _fileUploadService.SaveFileUploadAsync(FileUpload, EnumFileUploadType.AppVersion, fileName);

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
				_logger.LogError(ex, "An exception occured while trying create a new {entity}", nameof(AppVersion));
				return StatusCode(500);
			}
		}

		private async Task<bool> IsPostValidAsync()
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
