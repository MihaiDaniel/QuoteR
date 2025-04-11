using Quoter.Shared.Enums;
using Quoter.Web.Data.Entities;
using Quoter.Web.Models;

namespace Quoter.Web.Services
{
	public class FileUploadService : IFileUploadService
	{
		private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
		private string DirVersionUploads =  Path.Combine("Quoter", "VersionUploads");
		private string DirAppCollections = Path.Combine("Quoter", "AppCollections");

		public FileUploadService(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
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

		public string GenerateUniqueFileName(string fileName, EnumFileUploadType fileUploadType)
		{
			string dirSavePath = GetDirSavePath(fileUploadType);
			string filePath = Path.Combine(dirSavePath, fileName);
			string uniqueSuffix = "";
			while (File.Exists(filePath + uniqueSuffix))
			{
				uniqueSuffix = Path.GetRandomFileName();
			}
			if(fileUploadType == EnumFileUploadType.AppCollection)
			{
				return fileName + uniqueSuffix + ".qter";
			}
			return fileName + uniqueSuffix;
		}

		public async Task<string> SaveFileUploadAsync(IFormFile file, EnumFileUploadType fileUploadType, string? name = null)
		{
			string dirLocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			string dirUpload;
			switch (fileUploadType)
			{
				case EnumFileUploadType.AppVersion:
					dirUpload = Path.Combine(dirLocalAppData, DirVersionUploads);
					break;
				case EnumFileUploadType.AppCollection:
					dirUpload = Path.Combine(dirLocalAppData, DirAppCollections);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(fileUploadType), fileUploadType, null);
			}
			if (!Directory.Exists(dirUpload))
			{
				Directory.CreateDirectory(dirUpload);
			}

			string filePath = Path.Combine(dirUpload, name ?? file.FileName);

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

		private string GetDirSavePath(EnumFileUploadType fileUploadType)
		{
			string dirLocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			switch (fileUploadType)
			{
				case EnumFileUploadType.AppVersion:
					return Path.Combine(dirLocalAppData, DirVersionUploads);
				case EnumFileUploadType.AppCollection:
					return Path.Combine(dirLocalAppData, DirAppCollections);
				default:
					throw new ArgumentOutOfRangeException(nameof(fileUploadType), fileUploadType, null);
			}
		}
	}

	public enum EnumFileUploadType
	{
		AppVersion,
		AppCollection
	}
}
