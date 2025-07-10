using Microsoft.EntityFrameworkCore;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;

namespace Quoter.Web.Services
{
	public class VisitsStatisticsJob : BackgroundService
	{
		private readonly ILogger _logger;
		private readonly IServiceProvider _serviceProvider;

		public VisitsStatisticsJob(ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
		{
			_logger = loggerFactory.CreateLogger("VisitsStatisticsService");
			_serviceProvider = serviceProvider;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				try
				{
					using var scope = _serviceProvider.CreateScope();
					using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

					DateTime currentDateTime = DateTime.UtcNow;
					DateTime startOfCurrentDay = new DateTime(
						currentDateTime.Year,
						currentDateTime.Month,
						currentDateTime.Day,
						0, 0, 0, DateTimeKind.Utc);

					DateTime startOfPreviousDay = startOfCurrentDay.AddDays(-1);
					bool allStatsGenerated = false;
					while (!allStatsGenerated)
					{
						
						VisitsStatistic? stats = await context.VisitsStatistics
							.FirstOrDefaultAsync(s => s.Date == startOfPreviousDay.Date, stoppingToken);

						if (stats is null)
						{
							await GenerateStatsForPreviousDay(startOfPreviousDay, context);
							startOfPreviousDay = startOfPreviousDay.AddDays(-1);
						}
						else
						{
							allStatsGenerated = true;
						}
						if(startOfPreviousDay < startOfCurrentDay.AddDays(-30))
						{
							// Stop if we are trying to generate stats for more than 30 days ago
							allStatsGenerated = true;
						}
					}

					// Wait for 60 minutes
					await Task.Delay(TimeSpan.FromMinutes(60), stoppingToken);
				}
				catch (TaskCanceledException)
				{
					_logger.LogInformation("Visits statistics generation service was cancelled.");
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "An error occurred while generating visits statistics.");
				}
			}
		}

		private async Task GenerateStatsForPreviousDay(DateTime startOfPreviousDay, ApplicationDbContext context)
		{
			DateTime endOfPreviousDay = startOfPreviousDay.AddDays(1).AddSeconds(-1);
			List<Visit> visits = await context.Visits
				.Where(v => v.VisitDate.Date >= startOfPreviousDay.Date && v.VisitDate <= endOfPreviousDay)
				.ToListAsync();

			int visitsCount = visits.Count;
			int uniqueVisitorsCount = visits
				.Select(v => v.IpAddress)
				.Distinct()
				.Count();

			context.VisitsStatistics.Add(new VisitsStatistic()
			{
				Date = startOfPreviousDay.Date,
				Url = "/",
				VisitsCount = visitsCount,
				UniqueVisitorsCount = uniqueVisitorsCount
			});

			await context.SaveChangesAsync();

			// Remove visits from the database after statistics are generated
			context.Visits.RemoveRange(visits);
			await context.SaveChangesAsync();

			// Reset the auto-increment counter for Visits table to avoid overflow in the future
			await context.Database.ExecuteSqlRawAsync($"DELETE FROM sqlite_sequence WHERE name='Visits';");
		}
	}
}
