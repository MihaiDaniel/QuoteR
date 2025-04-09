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
		private readonly ILogger _logger;

		public RegistrationApiController(ApplicationDbContext context, ILoggerFactory loggerFactory) : base(context)
		{
			_context = context;
			_logger = loggerFactory.CreateLogger<RegistrationApiController>();
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

				AppRegistration? existingReg = await _context.AppRegistrations
					.AsNoTracking()
					.Where(r => r.InstallId == requestModel.InstallId)
					.FirstOrDefaultAsync();
				if (existingReg is not null)
				{
					return Ok(new RegisterPostResponseModel(existingReg.RegistrationId));
				}
				
				string? clientIpAddress = HttpContext.Connection?.RemoteIpAddress?.ToString();
				AppRegistration appRegistration = new()
				{
					InstallId = requestModel.InstallId.ToString(),
					IpAddress = clientIpAddress,
					LocalWinRegionCode = requestModel.LocalWinRegionCode,
					RegisteredDateTime = DateTime.UtcNow,
				};

				_context.AppRegistrations.Add(appRegistration);
				await _context.SaveChangesAsync();

				return Ok(new RegisterPostResponseModel(appRegistration.RegistrationId));
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Registration failed");
				return GetInternalServerErrorResponse();
			}

		}


	}

}
