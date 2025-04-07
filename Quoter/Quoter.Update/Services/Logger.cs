namespace Quoter.Update.Services
{
	public static class Logger
	{
		public static List<string> LstLines = new List<string>();

		public static void Error(string message)
		{
			string formattedLine = $"{DateTime.Now:yyyy/MM/dd HH:mm:ss,fff} ERROR: {message}";
			LstLines.Add(formattedLine);
			Console.WriteLine(formattedLine);
		}

		public static void Error(Exception exception, string message)
		{
			string formattedLine = $"{DateTime.Now:yyyy/MM/dd HH:mm:ss,fff} ERROR: {message}";
			LstLines.Add(formattedLine);
			LstLines.Add($"Exception: {exception}");
			Console.WriteLine(formattedLine);
		}

		public static void Info(string message)
		{
			string formattedLine = $"{DateTime.Now:yyyy/MM/dd HH:mm:ss,fff} INFO: {message}";
			LstLines.Add(formattedLine);
			Console.WriteLine(formattedLine);
		}


		public static async Task WriteAsync()
		{
			try
			{
				LstLines.Add(Environment.NewLine);
				string logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Quoter", "UpdateLog.txt");
				if (!Directory.Exists(Path.GetDirectoryName(logPath)))
				{
					Directory.CreateDirectory(Path.GetDirectoryName(logPath));
				}
				await File.WriteAllLinesAsync(logPath, LstLines);
			}
			catch
			{
				// nothing to do
			}
		}
	}
}
