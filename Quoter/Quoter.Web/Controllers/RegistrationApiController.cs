using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quoter.Shared.Models;
using Quoter.Web.Data;
using Quoter.Web.Data.Entities;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Quoter.Web.Controllers
{
	[Route("api/registration")]
	[ApiController]
	public class RegistrationApiController : BaseApiController
	{
		private readonly ApplicationDbContext _context;

		public RegistrationApiController(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		[Route("register")]
		[HttpPost]
		public async Task<IActionResult> Register(RegisterPostRequestModel requestModel)
		{
			try
			{
				if (string.IsNullOrEmpty(requestModel.InstallId)
					|| string.IsNullOrEmpty(requestModel.ApplicationKey))
				{
					return BadRequest("Identifier provided is not valid");
				}
				if(!await _context.AppKeys.AnyAsync(k => k.Key == requestModel.ApplicationKey))
				{
					return BadRequest("Identifier provided is not valid");
				}

				Guid? existingRegId = await _context.AppRegistrations
					.Where(r => r.Identifier == requestModel.InstallId)
					.Select(r => r.Id)
					.FirstOrDefaultAsync();
				if (existingRegId != null && existingRegId != Guid.Empty)
				{
					return Ok(new RegisterPostResponseModel(existingRegId.Value));
				}

				string? clientIpAddress = HttpContext.Connection?.RemoteIpAddress?.ToString();
				AppRegistration appRegistration = new()
				{
					Identifier = requestModel.InstallId.ToString(),
					IpAddress = clientIpAddress,
					LocalWinRegionCode = requestModel.LocalWinRegionCode,
					RegisteredDateTime = DateTime.UtcNow,
				};

				_context.AppRegistrations.Add(appRegistration);
				await _context.SaveChangesAsync();

				return Ok(new RegisterPostResponseModel(appRegistration.Id));
			}
			catch (Exception ex)
			{
				return GetInternalServerErrorResponse();
			}

		}


	}

}
