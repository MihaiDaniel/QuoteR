using Quoter.Web.Data.Entities;
using Quoter.Web.Models;

namespace Quoter.Web.Services
{

	public interface IFileVersionsService
	{
		Task<string> SaveFileVersionAsync(IFormFile file, string name = null);

		Task<VersionFile> GetVersionFileAsync(AppVersion appVersion);

		void Delete(string path);
	}
}
