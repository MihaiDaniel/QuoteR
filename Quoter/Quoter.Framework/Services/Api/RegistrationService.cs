namespace Quoter.Framework.Services.Api
{
	public class RegistrationService : IRegistrationService
	{
		private readonly ISettings _settings;
		private readonly IWebApiService _webApiService;

		public RegistrationService(ISettings settings, IWebApiService webApiService)
		{
			_settings = settings;
			_webApiService = webApiService;
		}

		public bool IsRegistered()
		{
			if (_settings.RegistrationId == Guid.Empty)
			{
				return false;
			}
			return true;
		}

		public async Task<Guid> GetRegistrationId()
		{
			if (_settings.RegistrationId == Guid.Empty)
			{
				string installId = _settings.InstallId;
				Guid registrationId = await _webApiService.RegisterAsync(installId);
				if(registrationId != Guid.Empty)
				{
					_settings.RegistrationId = registrationId;
				}
			}
			return _settings.RegistrationId;
		}

	}
}
