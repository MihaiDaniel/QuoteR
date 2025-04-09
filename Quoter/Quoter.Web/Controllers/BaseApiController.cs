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
			string? registrationId = GetRequestRegistration();
			if(!string.IsNullOrEmpty(registrationId))
			{
				return await _context.AppRegistrations.AnyAsync(r => r.RegistrationId == registrationId);
			}
			return false;
		}

		[NonAction]
		public string? GetRequestRegistration()
		{
			StringValues strValues = HttpContext.Request.Headers["Registration"];
			if (strValues.Any(sv => !string.IsNullOrEmpty(sv)))
			{
				string? registrationId = strValues.Last();
				return registrationId;
			}
			return null;
		}

		[NonAction]
		public async Task<int> GetAppRegistrationId()
		{
			string? registrationId = GetRequestRegistration();
			return await _context.AppRegistrations.Where(r => r.RegistrationId == registrationId).Select(r => r.Id).FirstAsync();
		}

		[NonAction]
		protected JsonResult GetInternalServerErrorResponse()
		{
			HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			return new JsonResult("An error occured during your request.");
		}
	}
}
