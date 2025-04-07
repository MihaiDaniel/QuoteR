using Moq;
using Quoter.Framework.Services;
using Quoter.Framework.Services.Api;
using Quoter.Framework.Services.AppSettings;

namespace Quoter.Framework.Tests.Services.Api
{
	public class WebApiServiceTests
	{
		Mock<IAppSettings> _mockSettings;
		Mock<ILogger> _mockLogger;
		WebApiService _webApiService;

		public WebApiServiceTests()
		{
			_mockLogger = new Mock<ILogger>();
			_mockSettings = new Mock<IAppSettings>();
			_mockSettings
				.SetupGet(_ => _.WebApiUrl)
				.Returns("https://localhost:7277");

			_webApiService = new(_mockLogger.Object, _mockSettings.Object);
		}


		[Fact]
		public async Task RegisterAsync_Should_GetNewGuid()
		{
			string installId = Guid.NewGuid().ToString();

			Guid registrationId = await _webApiService.RegisterAsync(installId, null, null);

			Assert.NotEqual(Guid.Empty, registrationId);
		}
	}
}
