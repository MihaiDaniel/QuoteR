using Microsoft.AspNetCore.Mvc;
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
		public async Task<IActionResult> Register([FromQuery] Guid? identifier)
		{
			try
			{
				if(identifier == null || string.IsNullOrEmpty(identifier.ToString())) 
				{
					return BadRequest();
				}

				string? clientIpAddress = HttpContext.Connection?.RemoteIpAddress?.ToString();
				AppRegistration appRegistration = new()
				{
					Identifier = identifier.ToString(),
					IpAddress = clientIpAddress,
					RegisteredDateTime = DateTime.UtcNow,
				};

				_context.AppRegistrations.Add(appRegistration);
				await _context.SaveChangesAsync();

				return Ok(appRegistration.Id);
			}
			catch (Exception ex)
			{
				return GetInternalServerErrorResponse();
			}
			
		}

	}

}
