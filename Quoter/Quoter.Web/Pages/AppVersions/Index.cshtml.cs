using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;

namespace Quoter.Web.Pages.AppVersions
{
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
