namespace Quoter.Framework.Enums
{
	/// <summary>
	/// Indicates how to handle application updates
	/// </summary>
	public enum EnumAutoUpdate
	{
		/// <summary>
		/// Never update the application.
		/// </summary>
		None = 0,
		/// <summary>
		/// Search for updates but ask the user for permission to download and install
		/// </summary>
		AskFirst = 1,
		/// <summary>
		/// Always search for update, download and install automatically without user permission
		/// </summary>
		Auto = 2
	}
}
