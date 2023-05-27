using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Web.Data.Entities;

namespace Quoter.Web.Pages.AppVersions
{
	[Authorize]
    public class IndexModel : PageModel
    {
        private readonly Quoter.Web.Data.ApplicationDbContext _context;

        public IndexModel(Quoter.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<AppVersion> AppVersion { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.AppVersions != null)
            {
                AppVersion = await _context.AppVersions.ToListAsync();
            }
        }
    }
}
