using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;

namespace Quoter.Web.Pages.AppVersions
{
	[Authorize]
	public class IndexModel : PageModel
	{
		private readonly ApplicationDbContext _context;
		public IList<AppVersion> AppVersion { get; set; } = default!;


		public IndexModel(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task OnGetAsync()
		{
			if (_context.AppVersions != null)
			{
				AppVersion = await _context.AppVersions.ToListAsync();
			}
		}
	}
}
