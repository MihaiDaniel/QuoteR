using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Web.Framework.Data;
using Quoter.Web.Framework.Models;

namespace Quoter.Web.App.Pages.QuoterApp.Updates
{
	[Authorize]
	public class List : PageModel
	{
		public List<QuoterUpdate> LstQuoterUpdates { get; set; }

		public List()
		{
			LstQuoterUpdates = new();
		}

		public async Task<IActionResult> OnGet([FromServices] QuoterContext context)
		{
			LstQuoterUpdates = await context.QuoterUpdates.ToListAsync();
			return Page();
		}
	}
}
