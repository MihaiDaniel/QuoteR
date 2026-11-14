using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Quoter.Web.Pages.HomePageParameters
{
	/// <summary>
	/// Admin page for editing the values displayed on the home page
	/// </summary>
	[Authorize]
	public class IndexModel : PageModel
	{
		private readonly ApplicationDbContext _context;

		[BindProperty]
		[Url]
		[Display(Name = "Store URL")]
		public string? StoreUrl { get; set; }

		[BindProperty]
		[Range(0, int.MaxValue)]
		[Display(Name = "Downloads")]
		public int? DownloadsCount { get; set; }

		[BindProperty]
		[Range(0, int.MaxValue)]
		[Display(Name = "Bible translations")]
		public int? BibleTranslationsCount { get; set; }

		public IndexModel(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task OnGetAsync()
		{
			HomePageParameter? parameters = await _context.HomePageParameters.AsNoTracking().FirstOrDefaultAsync();
			if (parameters == null)
			{
				return;
			}

			StoreUrl = parameters.StoreUrl;
			DownloadsCount = parameters.DownloadsCount;
			BibleTranslationsCount = parameters.BibleTranslationsCount;
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			HomePageParameter? parameters = await _context.HomePageParameters.FirstOrDefaultAsync();
			if (parameters == null)
			{
				parameters = new HomePageParameter();
				_context.HomePageParameters.Add(parameters);
			}

			parameters.StoreUrl = string.IsNullOrWhiteSpace(StoreUrl) ? null : StoreUrl.Trim();
			parameters.DownloadsCount = DownloadsCount;
			parameters.BibleTranslationsCount = BibleTranslationsCount;

			await _context.SaveChangesAsync();

			return RedirectToPage();
		}
	}
}
