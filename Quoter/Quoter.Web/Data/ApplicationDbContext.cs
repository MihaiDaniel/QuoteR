using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quoter.Web.Data.Entities;

namespace Quoter.Web.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<ApplicationUser> ApplicationUsers { get; set; }

		public DbSet<AppCollection> AppCollections { get; set; }

		public DbSet<AppCollectionDownload> AppCollectionDownloads { get; set; }

		public DbSet<AppError> AppErrors { get; set; }

		public DbSet<AppRegistration> AppRegistrations { get; set; }

		public DbSet<AppVersion> AppVersions { get; set; }

		public DbSet<AppVersionDownload> AppVersionDownloads { get; set; }

		public DbSet<AppKey> AppKeys { get; set; }

		public DbSet<Parameter> Parameters { get; set; }

		public DbSet<Visit> Visits { get; set; }

		public DbSet<VisitsStatistic> VisitsStatistics { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<AppRegistration>(entity =>
			{
				entity
					.HasMany(e => e.LstUpdateDownloads)
					.WithOne(e => e.AppRegistration)
					.OnDelete(DeleteBehavior.Cascade);
			});

			modelBuilder.Entity<AppVersion>(entity =>
			{
				entity
					.HasMany(e => e.LstAppVersionDownloads)
					.WithOne(e => e.AppVersion)
					.OnDelete(DeleteBehavior.Cascade);
			});

			modelBuilder.Entity<AppVersionDownload>(entity =>
			{
				entity
					.HasOne(e => e.AppVersion)
					.WithMany(e => e.LstAppVersionDownloads)
					.OnDelete(DeleteBehavior.Cascade);
			});
		}
	}
}