using Quoter.Shared.Models;

namespace Quoter.Framework.Services.Versioning
{
	/// <summary>
	/// Interface for service responsible for retrieving the current application version
	/// </summary>
	public interface IVersionService
	{
		/// <summary>
		/// Returns the current application version as a <see cref="QuoterVersionInfo"/>
		/// by reading the AssemblyVersion
		/// </summary>
		QuoterVersionInfo GetCurrentAppVersion();
	}
}
