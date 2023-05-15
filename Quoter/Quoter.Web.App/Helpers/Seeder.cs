using Quoter.Web.Framework.Data;
using Quoter.Web.Framework.Models;

namespace Quoter.Web.App.Helpers
{
	public static class Seeder
	{
		public static void Seed(this WebApplication app)
		{
			using IServiceScope scope = app.Services.CreateScope();
			using QuoterContext context = scope.ServiceProvider.GetService<QuoterContext>();

			User? admin = context.Users.FirstOrDefault(u => u.UserName == "admin");
			if(admin == null)
			{
				admin = new User()
				{
					UserName = "admin",
					Password = "password"
				};
				context.Users.Add(admin);
				context.SaveChanges();
			}
			
		}
	}
}
