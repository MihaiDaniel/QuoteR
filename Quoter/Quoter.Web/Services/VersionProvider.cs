namespace Quoter.Web.Services
{
	/// <summary>
	/// Provides the current version of the application by reading it from a file.
	/// </summary>
	public class VersionProvider
	{
		public string Version { get; }

		public VersionProvider(IWebHostEnvironment env)
		{
			var path = Path.Combine(env.ContentRootPath, "version.txt");
			Version = File.Exists(path) ? File.ReadAllText(path).Trim() : "unknown";
			Version = Version.Replace("web", "");
		}
	}
}
