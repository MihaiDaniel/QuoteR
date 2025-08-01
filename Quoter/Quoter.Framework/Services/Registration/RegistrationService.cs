﻿using Quoter.Framework.Services.Api;
using Quoter.Framework.Services.AppSettings;

namespace Quoter.Framework.Services.Registration
{
	public class RegistrationService : IRegistrationService
	{
		private readonly IAppConfiguration _configuration;
		private readonly IAppSettings _settings;
		private readonly IWebApiService _webApiService;

		public RegistrationService(IAppConfiguration configuration, IAppSettings settings, IWebApiService webApiService)
		{
			_configuration = configuration;
			_settings = settings;
			_webApiService = webApiService;
		}

		public bool IsRegistered()
		{
			if (string.IsNullOrEmpty(_settings.RegistrationId))
			{
				return false;
			}
			return true;
		}

		public async Task<string> RegisterAsync()
		{
			if (string.IsNullOrEmpty(_settings.RegistrationId))
			{
				string regionCode = RegionAndLanguageHelper.GetMachineCurrentLocation();
				string registrationId = await _webApiService.RegisterAsync(_settings.InstallId, _configuration.ApplicationKey, regionCode);
				_settings.RegistrationId = registrationId;
				
			}
			return _settings.RegistrationId;
		}

	}
}
