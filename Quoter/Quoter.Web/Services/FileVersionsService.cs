namespace Quoter.Web.Services
{
	public class FileVersionsService : IFileVersionsService
	{
		private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
		private const string DirUploads = "Versions";

		public FileVersionsService(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
		{
			_environment = environment;
		}

		public async Task<IFormFile> GetFileVersionAsync(string path)
		{
			throw new NotImplementedException();
		}

		public async Task<string> SaveFileVersionAsync(IFormFile file, string? name = null)
		{
			string versionsDir = Path.Combine(_environment.ContentRootPath, DirUploads);
			if (!Directory.Exists(versionsDir))
			{
				Directory.CreateDirectory(versionsDir);
			}

			string filePath = Path.Combine(versionsDir, name ?? file.FileName);

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
