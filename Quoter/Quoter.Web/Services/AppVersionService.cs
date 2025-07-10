using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Quoter.Web.Data;

namespace Quoter.Web.Services
{
	public class AppVersionService : IAppVersionService
	{
		private readonly ApplicationDbContext _context;
		private readonly IMemoryCache _memoryCache;

		public AppVersionService(ApplicationDbContext context,
			IMemoryCache memoryCache)
		{
			_context = context;
			_memoryCache = memoryCache;
		}

		public async Task<bool> IsAppVersionIdValid(int? appVersionId)
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
