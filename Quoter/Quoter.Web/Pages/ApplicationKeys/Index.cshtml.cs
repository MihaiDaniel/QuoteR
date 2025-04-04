using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;

namespace Quoter.Web.Pages.ApplicationKeys
{
	[Authorize]
	public class IndexModel : PageModel
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<IndexModel> _logger;
		public IList<AppKey> AppKeys { get; set; }

		public IndexModel(ApplicationDbContext context, ILogger<IndexModel> logger)
		{
			_context = context;
			_logger = logger;
			AppKeys = new List<AppKey>();
		}

		public async Task OnGetAsync()
		{
			AppKeys = await _context.AppKeys.OrderByDescending(k => k.AppKeyId).ToListAsync();
		}

		/// <summary>
		/// Delete an AppKey. This is called from a modal confirmation button.
		/// </summary>
		/// <remarks>
		/// This should be called /ApplicationKeys/Index?handler=Delete&id=@item.AppKeyId.ToString() 
		/// Note the lack of "OnPost" and "Async"
		/// </remarks>
		public async Task<IActionResult> OnPostDeleteAsync(int id)
		{
			try
			{
				AppKey? appKey = await _context.AppKeys.FirstOrDefaultAsync(k => k.AppKeyId == id);
				if (appKey is not null)
				{
					_context.AppKeys.Remove(appKey);
					await _context.SaveChangesAsync();
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Delete of appkey {0} failed!", id);
				throw;
			}
			return RedirectToPage();
		}

	}
}
