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
    public class DetailsModel : PageModel
    {
        private readonly Quoter.Web.Data.ApplicationDbContext _context;

        public DetailsModel(Quoter.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public AppVersion AppVersion { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.AppVersions == null)
            {
                return NotFound();
            }

            var appversion = await _context.AppVersions.FirstOrDefaultAsync(m => m.Id == id);
            if (appversion == null)
            {
                return NotFound();
            }
            else 
            {
                AppVersion = appversion;
            }
            return Page();
        }
    }
}
