using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;

namespace Quoter.Web.Extensions
{
	public static class WebApplicationExtensions
	{
		public static void MigrateDatabase(this WebApplication app)
		{
			using IServiceScope scope = app.Services.CreateScope();
			using ApplicationDbContext context = scope.ServiceProvider.GetService<ApplicationDbContext>()!;
			context.Database.Migrate();

			using LogsDbContext logsContext = scope.ServiceProvider.GetService<LogsDbContext>()!;
			logsContext.Database.EnsureCreated();
		}

		public static async Task SeedDatabase(this WebApplication app, ConfigurationManager configuration)
		{
			using IServiceScope scope = app.Services.CreateScope();
			using ApplicationDbContext context = scope.ServiceProvider.GetService<ApplicationDbContext>()!;
			using UserManager<ApplicationUser> userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>()!;

			string? administrator = Environment.GetEnvironmentVariable("QUOTER_ADMIN_USER");
			string? password = Environment.GetEnvironmentVariable("QUOTER_ADMIN_PASS");

			if(string.IsNullOrEmpty(administrator) || string.IsNullOrEmpty(password))
			{
				throw new ArgumentException("Environment variables QUOTER_ADMIN_USER and QUOTER_ADMIN_PASS must be set for the application to run!");
			}

			ApplicationUser user = await userManager.FindByNameAsync(administrator);
			if(user == null)
			{
				ApplicationUser appUser = new()
				{
					Email = $"{administrator}@gmail.com",
					UserName = administrator,
				};
				await userManager.CreateAsync(appUser, password);
			}

		}
	}
}
