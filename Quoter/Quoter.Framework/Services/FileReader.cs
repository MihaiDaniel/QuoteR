namespace Quoter.Framework.Services
{
	public class FileReader : IFileReader
	{
		public async Task<List<string>> ReadAsync(string filePath)
		{
			List<string> lstFileLines = new();

			using (StreamReader reader = new StreamReader(filePath))
			{
				while(!reader.EndOfStream)
				{
					string? line = await reader.ReadLineAsync();

					if (!string.IsNullOrWhiteSpace(line))
					{
						lstFileLines.Add(line);
					}
				}
			}
			return lstFileLines;
		}
	}
}
