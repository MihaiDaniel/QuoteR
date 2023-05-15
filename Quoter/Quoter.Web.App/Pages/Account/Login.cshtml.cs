using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Web.Framework.Data;
using Quoter.Web.Framework.Models;
using System.Security.Claims;

namespace Quoter.Web.App.Pages.Account
{
	public class LoginModel : PageModel
	{
		[BindProperty]
		public string UserName { get; set; }

		[BindProperty]
		public string Password { get; set; }

		[BindProperty]
		public string ReturnUrl { get; set; }

		[BindProperty]
		public bool RememberMe { get; set; }

		private QuoterContext _context { get; set; }

		public LoginModel(QuoterContext context)
		{
			_context = context;
		}

		public IActionResult OnGet(string returnUrl)
		{
			ReturnUrl = returnUrl;
			return Page();
		}

		public async Task<IActionResult> OnPost()
		{
			User? user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == UserName && u.Password == Password);

			if (user == null)
			{
				return Page();
			}

			List<Claim> lstClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, user.UserName)
			};

			ClaimsIdentity identity = new ClaimsIdentity(lstClaims, CookieAuthenticationDefaults.AuthenticationScheme);

			ClaimsPrincipal principal = new ClaimsPrincipal(identity);

			await HttpContext.SignInAsync(principal, new AuthenticationProperties()
			{
				IsPersistent = RememberMe,
			});

			return LocalRedirect("/");
		}
	}
}
