using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;

namespace Quoter.Web.Pages.Logs
{
	public class IndexModel : PageModel
	{
		private readonly LogsDbContext _context;

		public IList<Log> Logs { get; set; }

		public int TotalRecords { get; set; } = 0;

		[BindProperty(SupportsGet = true)]
		public int PageNo { get; set; } = 1;

		[BindProperty(SupportsGet = true)]
		public int PageSize { get; set; } = 10;

		public IndexModel(LogsDbContext context)
		{
			_context = context;
		}

		public async Task OnGetAsync()
		{
			TotalRecords = await _context.Logs.CountAsync();

			Logs = await _context.Logs
				.AsNoTracking()
				.OrderByDescending(v => v.LogId)
				.Skip((PageNo - 1) * PageSize)
				.Take(PageSize)
				.Select(l => new Log()
				{
					LogId = l.LogId,
					Level = l.Level,
					Timestamp = l.Timestamp,
					RenderedMessage = l.RenderedMessage,
				})
				.ToListAsync();
		}

		public async Task<IActionResult> OnPostDeleteOldLogsAsync()
		{
			try
			{
				var logs = await _context.Logs
					.Where(l => l.Timestamp < DateTime.UtcNow.AddDays(-30))
					.ToListAsync();
				if (logs.Count > 0)
				{
					_context.Logs.RemoveRange(logs);
					await _context.SaveChangesAsync();
				}
				return RedirectToPage();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
