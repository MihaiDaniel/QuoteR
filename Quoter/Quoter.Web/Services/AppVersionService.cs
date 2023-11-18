using Microsoft.EntityFrameworkCore;
using Quoter.Web.Data;

namespace Quoter.Web.Services
{
	public class AppVersionService : IAppVersionService
	{
		private readonly ApplicationDbContext _context;

		public AppVersionService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> IsAppVersionIdValid(Guid? appVersionId)
		{
			if (appVersionId is null || _context.AppVersions is null)
			{
				return false;
			}

			bool isFound = await _context.AppVersions.AnyAsync(av => av.Id == appVersionId);
			if (isFound)
			{
				return true;
			}
			return false;
		}
	}
}
