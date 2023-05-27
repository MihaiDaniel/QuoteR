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
	}
}