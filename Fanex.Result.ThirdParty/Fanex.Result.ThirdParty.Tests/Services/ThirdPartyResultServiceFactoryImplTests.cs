namespace Fanex.Result.ThirdParty.Tests.Services
{
    using Logging;
    using NSubstitute;
    using Sunplus.Agent.Model.Definitions;
    using ThirdParty.Services.Impl;
    using Xunit;

    public class ThirdPartyResultServiceFactoryImplTests
    {
        private readonly ILogger logger;
        private readonly ThirdPartyResultServiceFactoryImpl thirdPartyServiceResultFactory;

        public ThirdPartyResultServiceFactoryImplTests()
        {
            logger = Substitute.For<ILogger>();
            thirdPartyServiceResultFactory = new ThirdPartyResultServiceFactoryImpl(logger);
        }


        [Fact]
        public void CreateThirdPartyResultService_ProductIsRNGCasino_ReturnThirdPartyResultService()
        {
            // Act
            var resultService = thirdPartyServiceResultFactory.CreateThirdPartyResultService(ProductIdConstants.RNGCasino);

            // Assert
            Assert.True(resultService is ThirdPartyResultServiceImpl);
        }
    }
}
