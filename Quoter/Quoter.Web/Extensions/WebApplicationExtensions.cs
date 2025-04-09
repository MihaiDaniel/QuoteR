#nullable disable
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;
using Quoter.Web.Models.Configuration;

namespace Quoter.Web.Extensions
{
	public static class WebApplicationExtensions
	{
		public static void MigrateDatabase(this WebApplication app)
		{
			using IServiceScope scope = app.Services.CreateScope();
			using ApplicationDbContext context = scope.ServiceProvider.GetService<ApplicationDbContext>();
			context.Database.Migrate();

			using LogsDbContext logsContext = scope.ServiceProvider.GetService<LogsDbContext>();
			logsContext.Database.EnsureCreated();
		}

		public static async Task SeedDatabase(this WebApplication app, ConfigurationManager configuration)
		{
			using IServiceScope scope = app.Services.CreateScope();
			using ApplicationDbContext context = scope.ServiceProvider.GetService<ApplicationDbContext>();
			using UserManager<ApplicationUser> userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

			IOptionsMonitor<UsersConfiguration> optionsMonitor = scope.ServiceProvider.GetService<IOptionsMonitor<UsersConfiguration>>();
			UsersConfiguration usersConfiguration = optionsMonitor.CurrentValue;

			ApplicationUser? user = await userManager.FindByEmailAsync(usersConfiguration.DefaultUser.Email);
			if(user == null)
			{
				ApplicationUser appUser = new()
				{
					Email = usersConfiguration.DefaultUser.Email,
					UserName = usersConfiguration.DefaultUser.UserName,
				};
				await userManager.CreateAsync(appUser, usersConfiguration.DefaultUser.Password);
			}

		}
	}
}
