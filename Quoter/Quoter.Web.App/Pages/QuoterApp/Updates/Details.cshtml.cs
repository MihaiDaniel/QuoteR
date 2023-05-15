using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Web.Framework.Data;
using Quoter.Web.Framework.Models;

namespace Quoter.Web.App.Pages.QuoterApp.Updates
{
    public class DetailsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid? Id { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Version { get; set; }

        [BindProperty]
        public string Description { get; set; }

        [BindProperty]
        public string Path { get; set; }

        [BindProperty]
        public DateTime CreationDate { get; set; }

        public async Task<IActionResult> OnGet([FromServices] QuoterContext context)
        {
            QuoterUpdate? entry = await context.QuoterUpdates.FirstOrDefaultAsync(qu => qu.Id == Id);
            if (entry != null)
            {
                Id = entry.Id;
                Name = entry.Name;
                Description = entry.Description;
                Path = entry.Path;
                Version = entry.Version;
                CreationDate = entry.CreationDate;
            }
            return Page();
        }
    }
}
