using Microsoft.EntityFrameworkCore;
using Quoter.Web.Framework.Data;

namespace Quoter.Web.App.Helpers
{
    public static class Migration
    {
        public static void ApplyDbMigrations(this WebApplication app)
        {
            using IServiceScope scope = app.Services.CreateScope();
            using QuoterContext context = scope.ServiceProvider.GetService<QuoterContext>();
            context.Database.Migrate();
        }
    }
}
