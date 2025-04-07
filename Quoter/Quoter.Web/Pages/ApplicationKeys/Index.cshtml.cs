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
		public IList<AppKey> AppKeys { get; set; } = new List<AppKey>();

		public int TotalRecords { get; set; } = 0;

		[BindProperty(SupportsGet = true)]
		public int PageNo { get; set; } = 1;

		[BindProperty(SupportsGet = true)]
		public int PageSize { get; set; } = 10;

		public IndexModel(ApplicationDbContext context, ILogger<IndexModel> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task OnGetAsync()
		{
			TotalRecords = await _context.AppKeys.CountAsync();

			AppKeys = await _context.AppKeys
				.AsNoTracking()
				.OrderByDescending(k => k.AppKeyId)
				.Skip((PageNo - 1) * 10)
				.Take(PageSize)
				.ToListAsync();
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
