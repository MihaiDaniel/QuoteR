using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace Quoter.Web.Pages
{
	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	[IgnoreAntiforgeryToken]
	public class ErrorModel : PageModel
	{
		public string? RequestId { get; set; }

		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

		private readonly ILogger<ErrorModel> _logger;

		public new int StatusCode { get; set; }

		public ErrorModel(ILogger<ErrorModel> logger)
		{
			_logger = logger;
		}

		public void OnGet(int statusCode)
		{
			RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
			StatusCode = statusCode;
		}
	}
}