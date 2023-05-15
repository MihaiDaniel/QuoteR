using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quoter.Web.Framework.Data;
using Quoter.Web.Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace Quoter.Web.App.Pages.QuoterApp.Updates
{
    public class Create : PageModel
    {
        private readonly QuoterContext _context;

        [BindProperty, Required]
        public string Name { get; set; }

        [BindProperty, Required]
        public string Version { get; set; }

        [BindProperty]
        public string Description { get; set; }

        [BindProperty]
        public string Path { get; set; }

        public Create(QuoterContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                QuoterUpdate quoterUpdate = new QuoterUpdate()
                {
                    Name = Name,
                    Version = Version,
                    Description = Description,
                    Path = Path,
                };

                _context.QuoterUpdates.Add(quoterUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("/QuoterApp/Updates/List");
            }
            return Page();
        }
    }
}
