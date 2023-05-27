using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Quoter.Web.Data;
using System.Net;

namespace Quoter.Web.Controllers
{
	public class BaseApiController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public BaseApiController(ApplicationDbContext applicationDbContext)
		{
			_context = applicationDbContext;
		}

		[NonAction]
		public async Task<bool> IsRequestValid()
		{
			Guid registrationId = GetRequestRegistrationId();
			if(registrationId != Guid.Empty )
			{
				return await _context.AppRegistrations.AnyAsync(r => r.Id == registrationId);
			}
			return false;
		}

		[NonAction]
		public Guid GetRequestRegistrationId()
		{
			StringValues strValues = HttpContext.Request.Headers["Registration"];
			if (strValues.Any(sv => !string.IsNullOrEmpty(sv)))
			{
				string? registrationId = strValues.Last();
				Guid.TryParse(registrationId, out Guid result);
				return result;
			}
			return Guid.Empty;

		}

		[NonAction]
		protected JsonResult GetInternalServerErrorResponse()
		{
			HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			return new JsonResult("An error occured during your request.");
		}
	}
}
