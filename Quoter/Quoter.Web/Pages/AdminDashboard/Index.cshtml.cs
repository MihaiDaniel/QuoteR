using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Shared.Enums;
using Quoter.Web.Data;

namespace Quoter.Web.Pages.AdminDashboard
{
	/// <summary>
	/// Admin Dashboard index page
	/// </summary>
	public class IndexModel : PageModel
	{
		private readonly ApplicationDbContext _context;
		private readonly LogsDbContext _logsContext;
		private readonly ILogger _logger;

		public IndexViewModel ViewModel { get; set; }

		public IndexModel(ApplicationDbContext context, LogsDbContext logsContext)
		{
			_context = context;
			_logsContext = logsContext;
			ViewModel = new IndexViewModel();
		}

		public async void OnGet()
		{
			ViewModel.LatestVersion = await _context.AppVersions
				.Where(v => v.IsReleased == true)
				.OrderByDescending(v => v.CreationDate)
				.Select(v => v.Version)
				.FirstOrDefaultAsync() ?? " N/A ";
			ViewModel.VersionsNo = await _context.AppVersions.CountAsync();
			ViewModel.VersionsDownloaded = await _context.AppVersionDownloads
				.Where(vd => vd.AppVersion!.Type == EnumVersionType.Installer)
				.CountAsync();
			ViewModel.VersionsUpdates = await _context.AppVersionDownloads
				.Where(vd => vd.AppVersion!.Type == EnumVersionType.UpdateZip)
				.CountAsync();

			ViewModel.RegistrationsNo = await _context.AppRegistrations.CountAsync();

			ViewModel.AppKeysNo = await _context.AppKeys.CountAsync();

			ViewModel.ErrorsToday = await _logsContext.Logs.Where(l => l.Timestamp >= DateTime.Today && l.Level == "Error").CountAsync();
			ViewModel.ErrorsLastSevenDays = await _logsContext.Logs.Where(l => l.Timestamp >= DateTime.Today.AddDays(-7) && l.Level == "Error").CountAsync();
			ViewModel.ErrorsLastThirtyDays = await _logsContext.Logs.Where(l => l.Timestamp >= DateTime.Today.AddDays(-30) && l.Level == "Error").CountAsync();
		}
	}

	/// <summary>
	/// View model for dashboard page.
	/// </summary>
	public class IndexViewModel
	{
		/// <summary>
		/// Total number of collections irespective of language
		/// </summary>
		public int CollectionsNo { get; set; }

		/// <summary>
		/// The top language and amount of collections downloaded in that language
		/// </summary>
		public int TopLanguageDownloads { get; set; }

		/// <summary>
		/// Total number of downloaded collections
		/// </summary>
		public int CollectionsDownloadedNo { get; set; }

		public string LatestVersion { get; set; }

		public int VersionsNo { get; set; }

		public int VersionsDownloaded { get; set; }

		public int VersionsUpdates { get; set; }

		public int RegistrationsNo { get; set; }

		public int AppKeysNo { get; set; }

		public int ErrorsToday { get; set; }

		public int ErrorsLastSevenDays { get; set; }

		public int ErrorsLastThirtyDays { get; set; }
	}
}
