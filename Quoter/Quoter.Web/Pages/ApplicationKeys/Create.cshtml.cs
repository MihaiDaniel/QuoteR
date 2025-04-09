using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Quoter.Web.Pages.ApplicationKeys
{
	public class CreateModel : PageModel
	{
		private readonly ILogger<CreateModel> _logger;
		private readonly ApplicationDbContext _context;

		[BindProperty]
		[Required]
		public string NewKey { get; set; }

		public CreateModel(
			ILogger<CreateModel> logger,
			ApplicationDbContext context)
		{
			_logger = logger;
			_context = context;
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
				bool isValidKey = await IsValidPostAsync();
				if (isValidKey)
				{
					await CreateApplicationKeyAsync();
					return RedirectToPage("./Index");
				}

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occured while trying to create an application version.");
				throw;
			}
			return Page();
		}

		private async Task CreateApplicationKeyAsync()
		{
			AppKey appKey = new AppKey()
			{
				Key = NewKey
			};
			_context.Add(appKey);
			await _context.SaveChangesAsync();
		}

		private async Task<bool> IsValidPostAsync()
		{
			if (await _context.AppKeys.AnyAsync(k => k.Key == NewKey) == true)
			{
				ModelState.AddModelError(nameof(NewKey), "This key is already in use");
				return false;
			}
			return true;
		}
	}
}
