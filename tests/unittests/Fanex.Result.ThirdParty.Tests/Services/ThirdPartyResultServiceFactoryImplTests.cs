namespace Fanex.Result.ThirdParty.Tests.Services
{
    using System.Collections.Specialized;
    using Logging;
    using NSubstitute;
    using ThirdParty.Configurations;
    using ThirdParty.Constants;
    using ThirdParty.Services;
    using ThirdParty.Services.Impl;
    using Xunit;

    public class ThirdPartyResultServiceFactoryImplTests
    {
        private readonly ILogger logger;
        private readonly JsonConverter jsonConverter;
        private readonly ThirdPartyResultServiceFactoryImpl thirdPartyServiceResultFactory;

        public ThirdPartyResultServiceFactoryImplTests()
        {
            logger = Substitute.For<ILogger>();
            jsonConverter = Substitute.For<JsonConverter>();

            thirdPartyServiceResultFactory = new ThirdPartyResultServiceFactoryImpl(logger, jsonConverter);
        }

        [Fact]
        public void CreateThirdPartyResultService_ProductIsRNGCasino_ReturnThirdPartyResultService()
        {
            // Arrange
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["6:SiteIds"] = "3200;3100;3000"
            };

            // Act
            var resultService = thirdPartyServiceResultFactory.CreateThirdPartyResultService(ProductIdConstants.RNGCasino);

            // Assert
            Assert.True(resultService is ResultServiceImpl);
        }

        [Fact]
        public void CreateThirdPartyResultService_ProductIsVoidbridge_ReturnMacauGamesResultService()
        {
            // Arrange
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["22:SiteIds"] = "3200;3100;3000"
            };

            // Act
            var resultService = thirdPartyServiceResultFactory.CreateThirdPartyResultService(ProductIdConstants.MacauGames);

            // Assert
            Assert.True(resultService is MacauGamesResultServiceImpl);
        }

        [Fact]
        public void CreateThirdPartyResultService_ProductIsFishingWorld_ReturnFishingWorldResultService()
        {
            // Arrange
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["24:SiteIds"] = "3200;3100;3000"
            };

            // Act
            var resultService = thirdPartyServiceResultFactory.CreateThirdPartyResultService(ProductIdConstants.FishingWorld);

            // Assert
            Assert.True(resultService is FishingWorldResultServiceImpl);
        }
    }
}
