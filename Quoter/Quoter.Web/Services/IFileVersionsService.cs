namespace Quoter.Web.Services
{

	public interface IFileVersionsService
	{
		Task<string> SaveFileVersionAsync(IFormFile file, string name = null);

		Task<IFormFile> GetFileVersionAsync(string path);

		void Delete(string path);
	}
}
