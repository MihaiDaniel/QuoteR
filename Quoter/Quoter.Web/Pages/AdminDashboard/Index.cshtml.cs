using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Shared.Enums;
using Quoter.Web.Data;
using System.ComponentModel.DataAnnotations;

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
			ViewModel.CollectionsNo = await _context.AppCollections.CountAsync();
			ViewModel.TopLanguageDownloads = await _context.AppCollectionDownloads
				.GroupBy(cd => cd.AppCollection!.Language)
				.OrderByDescending(g => g.Count())
				.Select(g => g.Count())
				.FirstOrDefaultAsync();
			ViewModel.CollectionsDownloadedNo = await _context.AppCollectionDownloads
				.CountAsync();

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
				.Where(vd => vd.AppVersion!.Type == EnumVersionType.UpdaterZipPackage)
				.CountAsync();

			ViewModel.RegistrationsNo = await _context.AppRegistrations.CountAsync();

			ViewModel.AppKeysNo = await _context.AppKeys.CountAsync();

			ViewModel.ErrorsToday = await _logsContext.Logs.Where(l => l.Timestamp >= DateTime.Today && l.Level == "Error").CountAsync();
			ViewModel.ErrorsLastSevenDays = await _logsContext.Logs.Where(l => l.Timestamp >= DateTime.Today.AddDays(-7) && l.Level == "Error").CountAsync();
			ViewModel.ErrorsLastThirtyDays = await _logsContext.Logs.Where(l => l.Timestamp >= DateTime.Today.AddDays(-30) && l.Level == "Error").CountAsync();

			ViewModel.Statistics = await GetStatisticsAsync();
		}

		private async Task<StatisticsViewModel> GetStatisticsAsync()
		{
			StatisticsViewModel model = new();

			

			DateTime startOfCurrentDay = new DateTime(
				DateTime.UtcNow.Year,
				DateTime.UtcNow.Month,
				DateTime.UtcNow.Day,
				0, 0, 0, DateTimeKind.Utc);

			model.TodayVisits = await _context.Visits
				.Where(s => s.VisitDate >= startOfCurrentDay)
				.CountAsync();

			model.TodayUniqueVisitors = await _context.Visits
				.Where(s => s.VisitDate >= startOfCurrentDay)
				.GroupBy(s => s.IpAddress)
				.CountAsync();

			model.AllTimeVisits = model.TodayVisits + await _context.VisitsStatistics.SumAsync(s => s.VisitsCount);
			model.AllTimeUniqueVisitors = model.TodayUniqueVisitors + await _context.VisitsStatistics.SumAsync(s => s.UniqueVisitorsCount);

			// Stats for previous months
			model.MonthlyStatistics = await _context.VisitsStatistics
				.OrderBy(s => s.Date)
				.GroupBy(s => new { s.Date.Year, s.Date.Month })
				.Select(grouping => new MonthlyStatisticsViewModel
				{
					YearAndMonth = $"{grouping.Key.Year}-{grouping.Key.Month}",
					Visits = grouping.Sum(s => s.VisitsCount),
					UniqueVisitors = grouping.Sum(s => s.UniqueVisitorsCount),
				})
				.ToListAsync();

			return model;
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

		public StatisticsViewModel Statistics { get; set; } = new StatisticsViewModel();
	}

	public class StatisticsViewModel
	{
		public int AllTimeVisits { get; set; }
		public int AllTimeUniqueVisitors { get; set; }

		public int TodayVisits { get; set; }
		public int TodayUniqueVisitors { get; set; }

		public List<MonthlyStatisticsViewModel> MonthlyStatistics { get; set; } = new List<MonthlyStatisticsViewModel>();
	}

	public class MonthlyStatisticsViewModel
	{
		[Display(Name = "YearAndMonth")]
		public string YearAndMonth { get; set; }// e.g. "2023-10"

		[Display(Name = "Visits")]
		public int Visits { get; set; }

		[Display(Name = "UniqueVisitors")]
		public int UniqueVisitors { get; set; }
	}
}
