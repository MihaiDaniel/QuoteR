using Microsoft.EntityFrameworkCore;
using Quoter.Web.Data.Entities;

namespace Quoter.Web.Data
{
	public class LogsDbContext : DbContext
	{
		public LogsDbContext(DbContextOptions<LogsDbContext> options)
			: base(options)
		{
		}

		public DbSet<Log> Logs { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

		}
	}
}
