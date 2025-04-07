using Newtonsoft.Json;
using Quoter.Framework.Services.AppSettings;
using Quoter.Shared.Models;
using System.Net.Http.Json;

namespace Quoter.Framework.Services.Api
{
    public class WebApiService : IWebApiService
	{
		private readonly ILogger _logger;
		private readonly IAppSettings _settings;

		public WebApiService(ILogger logger, IAppSettings settings)
		{
			_logger = logger;
			_settings = settings;
		}

		public async Task<Guid> RegisterAsync(string installId, string applicationKey, string localWinRegionCode)
		{
			try
			{
				using (HttpClient client = new())
				{
					string uri = $"{_settings.WebApiUrl}/api/registration/register";
					RegisterPostRequestModel requestModel = new()
					{
						InstallId = installId,
						ApplicationKey = applicationKey,
						LocalWinRegionCode = localWinRegionCode
					};
					HttpResponseMessage response = await client.PostAsync(uri, JsonContent.Create(requestModel));
					if(response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						string content = await response.Content.ReadAsStringAsync();
						RegisterPostResponseModel responseModel = JsonConvert.DeserializeObject<RegisterPostResponseModel>(content);
						return responseModel.RegistrationId;
					}
					else
					{
						_logger.Error($"Registration API returned an unexpected status code {response.StatusCode}");
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
					string reqUrl = $"{_settings.WebApiUrl}/api/versions/getLatestVersionInfo";

					HttpRequestMessage requestMessage = new();
					requestMessage.RequestUri = new Uri(reqUrl);
					requestMessage.Method = HttpMethod.Get;
					requestMessage.Headers.Add("Registration", _settings.RegistrationId.ToString());

					HttpResponseMessage response = await client.SendAsync(requestMessage);
					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						string content = await response.Content.ReadAsStringAsync();
						LatestVersionInfoGetResponse responseModel = JsonConvert.DeserializeObject<LatestVersionInfoGetResponse>(content);
						return new QuoterVersionInfo(responseModel.Id, responseModel.Version);
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "Failed to get the latest version from web server");
			}
			return versionInfo;
		}

		public async Task<ActionResult> DownloadVersionAsync(Guid versionId)
		{
			try
			{
				using (HttpClient client = new())
				{
					string reqUrl = $"{_settings.WebApiUrl}/api/versions/downloadVersion?versionId={versionId}";
					client.DefaultRequestHeaders.Add("Registration", _settings.RegistrationId.ToString());
					using HttpResponseMessage response = await client.GetAsync(reqUrl);
					response.EnsureSuccessStatusCode();
					
					using HttpContent httpContent = response.Content;
					using Stream stream = await httpContent.ReadAsStreamAsync();
					string downloadFilePath = Path.Combine(GetDownloadFolderPath(), GetFileNameFromResponse(response, versionId));
					using FileStream fileStream = File.Create(downloadFilePath);
					await stream.CopyToAsync(fileStream);
					return ActionResult.Success(downloadFilePath);
				}
			}
			catch(Exception ex)
			{
				_logger.Error(ex, "Failed to download the latest version file");
				return ActionResult.Fail();
			}
		}

		private string GetFileNameFromResponse(HttpResponseMessage response, Guid versionId)
		{
			string? fileName = null;
			if (response.Content.Headers.ContentDisposition != null)
			{
				string? disposition = response.Content.Headers.ContentDisposition.FileName;
				if (!string.IsNullOrEmpty(disposition))
				{
					fileName = disposition.Trim('"');
				}
			}
			return fileName ?? "ver_" + versionId.ToString();
		}

		private string GetDownloadFolderPath()
		{
			string specialFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			string downloadFolderPath = Path.Combine(specialFolderPath, "Quoter");
			if (!Directory.Exists(downloadFolderPath))
			{
				Directory.CreateDirectory(downloadFolderPath);
			}
			return downloadFolderPath;
		}
	}
}
