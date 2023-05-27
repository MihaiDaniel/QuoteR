using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Web.Data.Entities;

namespace Quoter.Web.Pages.AppRegistrations
{
	[Authorize]
	public class DeleteModel : PageModel
    {
        private readonly Quoter.Web.Data.ApplicationDbContext _context;

        public DeleteModel(Quoter.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public AppRegistration AppRegistration { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.AppRegistrations == null)
            {
                return NotFound();
            }

            var appregistration = await _context.AppRegistrations.FirstOrDefaultAsync(m => m.Id == id);

            if (appregistration == null)
            {
                return NotFound();
            }
            else 
            {
                AppRegistration = appregistration;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.AppRegistrations == null)
            {
                return NotFound();
            }
            var appregistration = await _context.AppRegistrations.FindAsync(id);

            if (appregistration != null)
            {
                AppRegistration = appregistration;
                _context.AppRegistrations.Remove(AppRegistration);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
