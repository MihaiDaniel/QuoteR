using Quoter.Web.Data.Entities;
using Quoter.Web.Models;

namespace Quoter.Web.Services
{
	public interface IFileUploadService
	{
		string GenerateUniqueFileName(string fileName, EnumFileUploadType fileUploadType);

		Task<string> SaveFileUploadAsync(IFormFile file, EnumFileUploadType fileUploadType, string name = null);

		Task<VersionFileContent> GetVersionFileContentAsync(AppVersion appVersion);

		void Delete(string path);
	}
}
