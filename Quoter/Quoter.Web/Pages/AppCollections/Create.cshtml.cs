using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quoter.Shared.Enums;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;
using Quoter.Web.Services;
using System.ComponentModel.DataAnnotations;

namespace Quoter.Web.Pages.AppCollections
{
	public class CreateModel : PageModel
	{
		private readonly ILogger _logger;
		private readonly ApplicationDbContext _context;
		private readonly IFileUploadService _fileUploadService;

		[BindProperty]
		[Required]
		public string Name { get; set; } = string.Empty;

		[BindProperty]
		public string Description { get; set; } = string.Empty;

		[BindProperty]
		public EnumLanguage Language { get; set; } = EnumLanguage.English;

		[BindProperty]
		[Required]
		public IFormFile FileUpload { get; set; }

		public CreateModel(ILoggerFactory loggerFactory, ApplicationDbContext context, IFileUploadService fileUploadService)
		{
			_logger = loggerFactory.CreateLogger("AppCollections.Create");
			_context = context;
			_fileUploadService = fileUploadService;
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			try
			{
				if (!ModelState.IsValid)
				{
					_logger.LogWarning("An attempt was made to create {entity} with invalid model", nameof(AppCollection));
					return Page();
				}

				string fileName = _fileUploadService.GenerateUniqueFileName(FileUpload.FileName, EnumFileUploadType.AppCollection);
				string filePath = await _fileUploadService.SaveFileUploadAsync(FileUpload, EnumFileUploadType.AppCollection, fileName);

				AppCollection newCollection = new()
				{
					Name = Name,
					Description = Description,
					Path = filePath,
					Language = Language,
					UploadDateTime = DateTime.UtcNow
				};

				_context.AppCollections.Add(newCollection);
				await _context.SaveChangesAsync();

				return RedirectToPage("./Index");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An exception occurred while trying to create a new {entity}", nameof(AppCollection));
				return StatusCode(500);
			}
		}
	}
}
