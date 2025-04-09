using Quoter.Shared.Models;
using System.Reflection;

namespace Quoter.Framework.Services.Versioning
{
	/// <summary>
	/// Service responsible for retrieving the current application version
	/// </summary>
	public class VersionService : IVersionService
	{
		/// <inheritdoc/>
		public QuoterVersionInfo GetCurrentAppVersion()
		{
			Version? version = Assembly.GetEntryAssembly()?.GetName().Version;
			if (version == null)
			{
				return new QuoterVersionInfo(0,0,0,0);
			}
			else
			{
				QuoterVersionInfo currentVersion = new(version.ToString());
				return currentVersion;
			}
		}
	}
}
