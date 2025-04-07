using Moq;
using Quoter.Framework.Services;
using Quoter.Framework.Services.Api;
using Quoter.Framework.Services.AppSettings;
using Quoter.Framework.Services.Registration;

namespace Quoter.Framework.Tests.Services.Api
{
	public class RegistrationServiceTests
	{
		Mock<IAppSettings> _mockSettings;
		Mock<IWebApiService> _mockWebApiService;
		Mock<IAppConfiguration> _mockConfiguration;
		RegistrationService _registrationService;

		public RegistrationServiceTests()
		{
			_mockSettings = new Mock<IAppSettings>();
			_mockConfiguration = new Mock<IAppConfiguration> { };
			_mockWebApiService = new Mock<IWebApiService>();

			_registrationService = new(_mockConfiguration.Object, _mockSettings.Object, _mockWebApiService.Object);
		}


		[Fact]
		public async Task GetRegistrationId_Should_GetNewRegId_When_NotRegistered()
		{
			Guid expected = Guid.NewGuid();
			_mockSettings
				.SetupSequence(_ => _.RegistrationId)
				.Returns(Guid.Empty)
				.Returns(expected);

			_mockWebApiService
				.Setup(_ => _.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
				.ReturnsAsync(expected);

			Guid registrationId = await _registrationService.RegisterAsync();

			Assert.NotEqual(Guid.Empty, registrationId);
			_mockSettings.VerifyGet(s => s.RegistrationId, Times.Exactly(2));
		}

		[Fact]
		public async Task GetRegistrationId_Should_RetSameRegId_When_AlreadyRegistered()
		{
			Guid expected = Guid.NewGuid();
			_mockSettings
				.SetupGet(_ => _.RegistrationId)
				.Returns(expected);

			_mockWebApiService
				.Setup(_ => _.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
				.ReturnsAsync(Guid.NewGuid);

			Guid registrationId = await _registrationService.RegisterAsync();

			Assert.Equal(expected, registrationId);
		}
	}
}
