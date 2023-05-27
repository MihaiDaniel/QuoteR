using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Quoter.Web.Pages.AppRegistrations
{
	[Authorize]
	public class IndexModel : PageModel
	{
		private readonly Data.ApplicationDbContext _context;

		public IndexModel(Data.ApplicationDbContext context)
		{
			_context = context;
		}

		public IList<AppRegistrationModel> ViewModels { get; set; } = default!;

		public class AppRegistrationModel
		{
			public Guid Id { get; set; }

			public string Identifier { get; set; }

			public string? IpAddress { get; set; }

			public DateTime RegisteredDateTime { get; set; }

			public int VersionDownloads { get; set; }
		}

		public async Task OnGetAsync()
		{
			try
			{
				ViewModels =  await _context.AppRegistrations
					.Include(r => r.LstUpdateDownloads)
					.Select(r => new AppRegistrationModel()
					{
						Id = r.Id,
						Identifier = r.Identifier,
						IpAddress = r.IpAddress,
						RegisteredDateTime = r.RegisteredDateTime,
						VersionDownloads = r.LstUpdateDownloads.Count
					})
					.ToListAsync();
			}
			catch (Exception ex)
			{
			}
		}
	}
}
