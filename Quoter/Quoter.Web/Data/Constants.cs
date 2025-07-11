namespace Quoter.Web.Data
{
	public static class Constants
	{
		public static class Folders
		{
			public const string AppData = "Quoter";
		}

		public static class Database
		{
			public const string SqliteDbName = "quoter.web.db";
			public const string SqliteLogsDbName = "quoter.web.logs.db";
		}

		public static class MemoryCacheKeys
		{
			public const string IsDownloadAvailable = "IsDownloadAvailable";
			public const string LatestAppVersionForDownload = "LatestVersionForDownload";
			public const string DownloadsCount = "DownloadsCount";
		}
	}
}
