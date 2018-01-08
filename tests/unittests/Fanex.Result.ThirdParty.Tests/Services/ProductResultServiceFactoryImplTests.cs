namespace Fanex.Result.ThirdParty.Tests.Services
{
    using System.Collections.Specialized;
    using Constants;
    using Logging;
    using NSubstitute;
    using RestSharp;
    using ThirdParty.Configurations;
    using ThirdParty.Services;
    using ThirdParty.Services.Impl;
    using Xunit;

    public class ProductResultServiceFactoryImplTests
    {
        private readonly ILogger logger;
        private readonly IRestClient restClient;
        private readonly ProductResultServiceFactoryImpl thirdPartyServiceResultFactory;

        public ProductResultServiceFactoryImplTests()
        {
            logger = Substitute.For<ILogger>();
            restClient = Substitute.For<IRestClient>();

            thirdPartyServiceResultFactory = new ProductResultServiceFactoryImpl(logger, restClient);
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
            var resultService = thirdPartyServiceResultFactory.CreateProductResultService(ProductIdConstants.RNGCasino);

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
            var resultService = thirdPartyServiceResultFactory.CreateProductResultService(ProductIdConstants.MacauGames);

            // Assert
            Assert.True(resultService is Product22ResultServiceImpl);
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
            var resultService = thirdPartyServiceResultFactory.CreateProductResultService(ProductIdConstants.FishingWorld);

            // Assert
            Assert.True(resultService is Product24ResultServiceImpl);
        }

        [Fact]
        public void CreateThirdPartyResultService_ProductIsPP_ReturnPPResultService()
        {
            // Arrange
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["32:SiteIds"] = "3200;3100;3000"
            };

            // Act
            var resultService = thirdPartyServiceResultFactory.CreateProductResultService(ProductIdConstants.PP);

            // Assert
            Assert.True(resultService is Product32ResultServiceImpl);
        }
    }
}
