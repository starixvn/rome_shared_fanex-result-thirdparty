namespace Fanex.Result.ThirdParty.Tests.Services
{
    using Logging;
    using NSubstitute;
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
            // Act
            var resultService = thirdPartyServiceResultFactory.CreateThirdPartyResultService(ProductIdConstants.RNGCasino);

            // Assert
            Assert.True(resultService is ResultServiceImpl);
        }

        [Fact]
        public void CreateThirdPartyResultService_ProductIsVoidbridge_ReturnMacauGamesResultService()
        {
            // Act
            var resultService = thirdPartyServiceResultFactory.CreateThirdPartyResultService(ProductIdConstants.MacauGames);

            // Assert
            Assert.True(resultService is MacauGamesResultServiceImpl);
        }

        [Fact]
        public void CreateThirdPartyResultService_ProductIsFishingWorld_ReturnFishingWorldResultService()
        {
            // Act
            var resultService = thirdPartyServiceResultFactory.CreateThirdPartyResultService(ProductIdConstants.FishingWorld);

            // Assert
            Assert.True(resultService is FishingWorldResultServiceImpl);
        }
    }
}
