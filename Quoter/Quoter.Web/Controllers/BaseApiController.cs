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

		/// <summary>
		/// Verifies if the calling application client is registered to use the API.
		/// We expect the registration id to be present in the request header
		/// </summary>
		[NonAction]
		public async Task<bool> IsClientRegistered()
		{
			Guid registrationId = GetClientRegistrationId();
			if(registrationId != Guid.Empty )
			{
				return await _context.AppRegistrations.AnyAsync(r => r.Id == registrationId);
			}
			return false;
		}

		[NonAction]
		public Guid GetClientRegistrationId()
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
