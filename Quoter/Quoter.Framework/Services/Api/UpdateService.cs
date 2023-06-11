using Quoter.Shared.Enums;
using Quoter.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Quoter.Framework.Services.Api
{
	public class UpdateService : IUpdateService
	{
		private readonly ILogger _logger;
		private readonly IRegistrationService _registrationService;
		private readonly IWebApiService _webApiService;

		public UpdateService(ILogger logger,
							IRegistrationService registrationService,
							IWebApiService webApiService)
		{
			_logger = logger;
			_registrationService = registrationService;
			_webApiService = webApiService;
		}

		public Task<bool> IsNewVersionAvailable()
		{
			throw new NotImplementedException();
		}

		public async Task TryUpdate()
		{
			try
			{
				QuoterVersionInfo latestVersion = await _webApiService.GetLatestVersion();
				Version? version = Assembly.GetExecutingAssembly().GetName().Version;
				QuoterVersionInfo currentVersion = new(version.ToString());

				if(currentVersion.CompareWith(latestVersion) == EnumVersionCompare.Older)
				{
					// Update application
				}

			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}

		}
	}
}
