using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Shared.Enums;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;
using Quoter.Web.Services;
using Quoter.Web.ViewModels.AppVersions;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Quoter.Web.Pages.AppVersions
{
	[Authorize]
	public class EditModel : PageModel
	{
		private readonly ApplicationDbContext _context;
		private readonly IAppVersionService _appVersionService;

		[BindProperty]
		[Required]
		public int Id { get; set; }

		[BindProperty]
		[Required]
		public string Name { get; set; }

		[BindProperty]
		[Required]
		public string Version { get; set; }

		[BindProperty]
		public bool IsReleased { get; set; }

		[BindProperty]
		public string? Description { get; set; }

		[BindProperty]
		public EnumOperatingSystem Os { get; set; }

		public string Path { get; set; }

		public EditModel(ApplicationDbContext context, IAppVersionService appVersionService)
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

			AppVersion appVersion = await _context.AppVersions
				.AsNoTracking()
				.Where(appVersion => appVersion.Id == id)
				.FirstAsync();

			Id = appVersion.Id;
			Name = appVersion.Name;
			Version = appVersion.Version;
			Description = appVersion.Description;
			Os = appVersion.Os;
			Path = appVersion.Path;
			IsReleased = appVersion.IsReleased;

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
				AppVersion? appVersion = _context.AppVersions.FirstOrDefault(a => a.Id == Id);
				if (appVersion == null)
				{
					return BadRequest();
				}
				if(!await ValidatePostAsync(appVersion))
				{
					return Page();
				}

				appVersion.Name = Name;
				appVersion.Version = Version;
				appVersion.IsReleased = IsReleased;
				appVersion.Os = Os;
				appVersion.Description = Description;

				await _context.SaveChangesAsync();

				return RedirectToPage("./Index");
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		private async Task<bool> ValidatePostAsync(AppVersion existingVersion)
		{
			Regex regexVersionFormat = new("^\\d+\\.\\d+\\.\\d+\\.\\d+$");
			if (!regexVersionFormat.IsMatch(Version))
			{
				ModelState.TryAddModelError($"{nameof(Version)}", "Version format is incorrect, example correct format: 1.2.3.4");
			}

			bool isDuplicateVersion = await _context.AppVersions
				.AnyAsync(v => v.Version == Version
							&& v.Type == existingVersion.Type);
			if (isDuplicateVersion)
			{
				ModelState.TryAddModelError($"{nameof(Version)}", "A version with the same version and version type already exists");
				return false;
			}
			return true;
		}
	}
}
