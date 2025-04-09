using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;

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
				RenderedMessage = log.RenderedMessage;
				Properties = log.Properties;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An exception occured while retieving log details for {property}={value}", nameof(Log.LogId), id);
			}
			
		}
	}
}
