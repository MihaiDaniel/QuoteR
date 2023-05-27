using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quoter.Web.Data.Entities;

namespace Quoter.Web.Pages.AppRegistrations
{
	[Authorize]
	public class CreateModel : PageModel
    {
        private readonly Quoter.Web.Data.ApplicationDbContext _context;

        public CreateModel(Quoter.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AppRegistration AppRegistration { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.AppRegistrations == null || AppRegistration == null)
            {
                return Page();
            }

            _context.AppRegistrations.Add(AppRegistration);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
