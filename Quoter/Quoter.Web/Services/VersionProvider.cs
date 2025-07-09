using System.Text.RegularExpressions;

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
			var path = Path.Combine(env.ContentRootPath, "version");
			Version = File.Exists(path) ? File.ReadAllText(path).Trim() : "unknown";
			// Normalize the version format to "major.minor.patch"
			Match match = Regex.Match(Version, @"(\d+\.\d+\.\d+)");
			if (match.Success)
			{
				Version = match.Groups[1].Value;
			}
		}
	}
}
