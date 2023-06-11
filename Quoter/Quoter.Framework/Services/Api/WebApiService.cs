using Newtonsoft.Json;
using Quoter.Shared.Models;
using System.Net.Http.Json;

namespace Quoter.Framework.Services.Api
{
	public class WebApiService : IWebApiService
	{
		private readonly ILogger _logger;
		private readonly ISettings _settings;

		public WebApiService(ILogger logger, ISettings settings)
		{
			_logger = logger;
			_settings = settings;
		}

		public async Task<Guid> RegisterAsync(string installId)
		{
			try
			{
				using (HttpClient client = new())
				{
					string reqUri = $"{_settings.WebApiDomainUrl}/api/registration/register";
					RegisterPostRequestModel reqModel = new()
					{
						InstallId = installId,
					};
					HttpResponseMessage response = await client.PostAsync(reqUri, JsonContent.Create(reqModel));
					if(response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						string content = await response.Content.ReadAsStringAsync();
						RegisterPostResponseModel responseModel = JsonConvert.DeserializeObject<RegisterPostResponseModel>(content);
						return responseModel.RegistrationId;
					}
				}
			}
			catch(Exception ex)
			{
				_logger.Error(ex, "Failed to register the application.");
			}
			return Guid.Empty;
		}

		public async Task<QuoterVersionInfo> GetLatestVersion()
		{
			QuoterVersionInfo versionInfo = new("0.0.0.0");
			try
			{
				using (HttpClient client = new())
				{
					string reqUri = $"{_settings.WebApiDomainUrl}/api/versions/getLatestVersionInfo";

					HttpRequestMessage requestMessage = new();
					requestMessage.Method = HttpMethod.Get;
					requestMessage.Headers.Add("Registration", _settings.RegistrationId.ToString());

					HttpResponseMessage response = await client.SendAsync(requestMessage);
					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						string content = await response.Content.ReadAsStringAsync();
						QuoterVersionInfo responseModel = JsonConvert.DeserializeObject<QuoterVersionInfo>(content);
						return responseModel;
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Failed to get the latest version from web server");
			}
			return versionInfo;
		}

		public Task<bool> DownloadVersionAsync(Guid versionId)
		{
			throw new NotImplementedException();
		}
	}
}
