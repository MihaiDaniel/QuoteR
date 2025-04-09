using Quoter.Web.Data.Entities;
using Quoter.Web.Models;

namespace Quoter.Web.Services
{
	public class FileVersionsService : IFileVersionsService
	{
		private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
		private string DirUploads =  Path.Combine("Quoter", "VersionUploads");

		public FileVersionsService(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
		{
			_environment = environment;
		}

		public async Task<VersionFileContent> GetVersionFileContentAsync(AppVersion appVersion)
		{
			VersionFileContent versionFile = new();
			versionFile.Version = appVersion.Version;
			versionFile.FileName = Path.GetFileName(appVersion.Path);
			versionFile.Content = await File.ReadAllBytesAsync(appVersion.Path);

			return versionFile;
		}

		public async Task<string> SaveFileVersionUploadAsync(IFormFile file, string? name = null)
		{
			string dirLocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			string versionUploadsDir = Path.Combine(dirLocalAppData, DirUploads);
			if (!Directory.Exists(versionUploadsDir))
			{
				Directory.CreateDirectory(versionUploadsDir);
			}

			string filePath = Path.Combine(versionUploadsDir, name ?? file.FileName);

			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(fileStream);
			}

			return filePath;
		}

		public void Delete(string path)
		{
			if(File.Exists(path))
			{
				File.Delete(path);
			}
		}
	}
}
