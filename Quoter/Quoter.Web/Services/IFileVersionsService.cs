using Quoter.Web.Data.Entities;
using Quoter.Web.Models;

namespace Quoter.Web.Services
{

	public interface IFileVersionsService
	{
		Task<string> SaveFileVersionUploadAsync(IFormFile file, string name = null);

		Task<VersionFileContent> GetVersionFileContentAsync(AppVersion appVersion);

		void Delete(string path);
	}
}
