namespace Fanex.Result.ThirdParty.Tests.Services
{
    using Constants;
    using Fanex.Result.Core.Casino;
    using Fanex.Result.Core.PGSoft;
    using KellermanSoftware.CompareNetObjects;
    using Logging;
    using NSubstitute;
    using RestSharp;
    using ThirdParty.Configurations;
    using ThirdParty.Models;
    using ThirdParty.Models.Requests;
    using ThirdParty.Services.Impl;
    using Xunit;

    public class Product55ResultServiceImplTests
    {
        private readonly CompareLogic compareLogic;
        private readonly ICasinoResult result;
        private readonly IRestClient restClient;
        private readonly Product55ResultServiceImpl resultService;
        private readonly IPGSoftResult pgSoftResult;

        public Product55ResultServiceImplTests()
        {
            compareLogic = new CompareLogic();
            restClient = Substitute.For<IRestClient>();
            var config = Substitute.For<ProductResultApiConfig>();
            var logger = Substitute.For<ILogger>();
            result = Substitute.For<ICasinoResult>();
            pgSoftResult = Substitute.For<IPGSoftResult>();

            resultService = new Product55ResultServiceImpl(config, logger, restClient, pgSoftResult);
        }

        [Fact]
        public void GetResultLink_NotHaveException_CallGetResultUrlFromApi()
        {
            // Arrange
            var query = new ThirdPartyResultBaseQuery { CustId = 1, GameId = "RefNo", RoleId = 2, Language = DefaultVariables.Language };

            // Act
            resultService.GetDetailLink(query);

            // Assert
            pgSoftResult.Received().GetResult(Arg.Any<PGSoftParam>());

        }

        [Fact]
        public void GetResultLink_NotHaveException_ReturnSuccessThirdPartyResult()
        {
            // Arrange
            var query = new ThirdPartyResultBaseQuery { CustId = 1, GameId = "RefNo", RoleId = 2, Language = DefaultVariables.Language };

            result.GetCasinoResult(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>())
                .Returns(string.Empty);

            var expectedModel = new SuccessResult(string.Empty);

            // Act
            var actualModel = resultService.GetDetailLink(query);

            // Assert
            Assert.True(compareLogic.Compare(expectedModel, actualModel).AreEqual);
        }       
    }
}
