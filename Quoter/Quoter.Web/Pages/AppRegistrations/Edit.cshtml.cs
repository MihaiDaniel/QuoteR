using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Web.Data.Entities;

namespace Quoter.Web.Pages.AppRegistrations
{
	[Authorize]
	public class EditModel : PageModel
    {
        private readonly Quoter.Web.Data.ApplicationDbContext _context;

        public EditModel(Quoter.Web.Data.ApplicationDbContext context)
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

            var appregistration =  await _context.AppRegistrations.FirstOrDefaultAsync(m => m.Id == id);
            if (appregistration == null)
            {
                return NotFound();
            }
            AppRegistration = appregistration;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(AppRegistration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppRegistrationExists(AppRegistration.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AppRegistrationExists(Guid id)
        {
          return (_context.AppRegistrations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
