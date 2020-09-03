namespace Fanex.Result.ThirdParty.Tests.Services
{
    using Constants;
    using Fanex.Result.Core.Casino;
    using KellermanSoftware.CompareNetObjects;
    using Logging;
    using NSubstitute;
    using RestSharp;
    using ThirdParty.Configurations;
    using ThirdParty.Models;
    using ThirdParty.Models.Requests;
    using ThirdParty.Services.Impl;
    using Xunit;

    public class Product52ResultServiceImplTests
    {
        private readonly CompareLogic compareLogic;
        private readonly ICasinoResult result;
        private readonly IRestClient restClient;
        private readonly Product52ResultServiceImpl resultService;

        public Product52ResultServiceImplTests()
        {
            compareLogic = new CompareLogic();
            restClient = Substitute.For<IRestClient>();
            var config = Substitute.For<ProductResultApiConfig>();
            var logger = Substitute.For<ILogger>();
            result = Substitute.For<ICasinoResult>();

            resultService = new Product52ResultServiceImpl(config, logger, restClient, result);
        }

        [Fact]
        public void GetResultLink_NotHaveException_CallGetResultUrlFromApi()
        {
            // Arrange
            var query = new ThirdPartyResultBaseQuery { CustId = 1, GameId = "RefNo", RoleId = 2, Language = DefaultVariables.Language };

            // Act
            resultService.GetDetailLink(query);

            // Assert
            result.Received().GetCasinoResult(
                Arg.Is(query.CustId.ToString()),
                Arg.Is(query.GameId),
                Arg.Is(query.Language),
                "3000");
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
                .Returns("Url");

            var expectedModel = new SuccessResult("Url");

            // Act
            var actualModel = resultService.GetDetailLink(query);

            // Assert
            Assert.True(compareLogic.Compare(expectedModel, actualModel).AreEqual);
        }       
    }
}
