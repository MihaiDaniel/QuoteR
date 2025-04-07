using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;

namespace Quoter.Web.Pages.AppRegistrations
{
	[Authorize]
	public class IndexModel : PageModel
	{
		private readonly ApplicationDbContext _context;

		public IList<AppRegistration> AppRegistrations { get; set; }

		public int TotalRecords { get; set; }

		[BindProperty(SupportsGet = true)]
		public int PageNo { get; set; }

		[BindProperty(SupportsGet = true)]
		public int PageSize { get; set; }

		public IndexModel(ApplicationDbContext context)
		{
			_context = context;
			PageNo = 1;
			PageSize = 10;
		}

		public async Task OnGetAsync()
		{
			try
			{
				TotalRecords = await _context.AppRegistrations.CountAsync();

				AppRegistrations =  await _context.AppRegistrations
					.AsNoTracking()
					.OrderByDescending(r => r.RegisteredDateTime)
					.Skip((PageNo - 1) * 10)
					.Take(PageSize)
					.ToListAsync();
			}
			catch (Exception ex)
			{
			}
		}
	}
}
