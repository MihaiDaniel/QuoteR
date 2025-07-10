namespace Quoter.Shared.Enums
{
	/// <summary>
	/// Describes what type of version file is the file
	/// </summary>
	/// <remarks>
	/// This can mean a zip file (for updating installed app) or an installer
	/// </remarks>
	public enum EnumVersionType
	{
		/// <summary>
		/// This has no special purpose, it can be used as a test file
		/// </summary>
		None = 0,

		/// <summary>
		/// This represents an installer file used for installing the app (e.g. Setup.exe)
		/// </summary>
		Installer = 1,

		/// <summary>
		/// This represents zip a file used to update the installed application (used by the auto-updater)
		/// </summary>
		UpdaterZipPackage = 2,

		/// <summary>
		/// This represents a zipped application file, which is used to distribute the app in a compressed format
		/// without the installer
		/// </summary>
		ZipArchive = 3,
	}
}
