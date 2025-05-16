using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;
using System.Text.Json;

namespace Quoter.Web.Pages.AppLogs
{
	public class DetailsModel : PageModel
	{
		private readonly LogsDbContext _context;
		private readonly ILogger _logger;

		public int LogId { get; set; }

		public string Timestamp { get; set; }

		public string Level { get; set; }

		public string Exception { get; set; }

		public string RenderedMessage { get; set; }

		public string Properties { get; set; }

		public DetailsModel(LogsDbContext context, ILoggerFactory loggerFactory)
		{
			_context = context;
			_logger = loggerFactory.CreateLogger("AppLogs.Details");
		}

		public async Task OnGetAsync(int id)
		{
			try
			{
				Log log = await _context.Logs.FirstAsync(log => log.LogId == id);
				LogId = log.LogId;
				Timestamp = log.Timestamp.ToString("yyyy/MM/dd hh:mm:ss,fff");
				Level = log.Level;
				Exception = log.Exception;
				RenderedMessage = ParseRenderedMessageWithProperties(log.RenderedMessage, log.Properties);
				Properties = log.Properties;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An exception occured while retieving log details for {property}={value}", nameof(Log.LogId), id);
			}
			
		}

		private string ParseRenderedMessageWithProperties(string renderedMessage, string properties)
		{
			JsonDocument jsonDocument = JsonDocument.Parse(properties);
			JsonElement jsonElement = jsonDocument.RootElement;

			string[] propertiesInRenderedMsg = ExtractPropsFromRenderedMessage(renderedMessage);
			foreach (string property in propertiesInRenderedMsg)
			{
				if (jsonElement.TryGetProperty(property, out JsonElement value))
				{
					renderedMessage = renderedMessage.Replace($"{{{property}}}", value.ToString());
				}
			}
			return renderedMessage;
		}

		private string[] ExtractPropsFromRenderedMessage(string renderedMessage)
		{
			if (string.IsNullOrEmpty(renderedMessage))
				return Array.Empty<string>();

			var matches = System.Text.RegularExpressions.Regex.Matches(renderedMessage, @"\{([^}]+)\}");
			return matches.Select(m => m.Groups[1].Value).ToArray();
		}
	}
}
