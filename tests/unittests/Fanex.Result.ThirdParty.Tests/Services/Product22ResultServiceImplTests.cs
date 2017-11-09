namespace Fanex.Result.ThirdParty.Tests.Services
{
    using System;
    using Constants;
    using Core.Voidbridge;
    using KellermanSoftware.CompareNetObjects;
    using Logging;
    using NSubstitute;
    using ThirdParty.Configurations;
    using ThirdParty.Models;
    using ThirdParty.Models.Requests;
    using ThirdParty.Services;
    using ThirdParty.Services.Impl;
    using Xunit;

    public class Product22ResultServiceImplTests
    {
        private readonly CompareLogic compareLogic;
        private readonly VoidbridgeApi voidbridgeApi;

        private readonly Product22ResultServiceImpl macauGamesResultService;

        public Product22ResultServiceImplTests()
        {
            compareLogic = new CompareLogic();

            var config = Substitute.For<ProductResultApiConfig>();
            var logger = Substitute.For<ILogger>();
            var jsonConverter = Substitute.For<JsonConverter>();
            voidbridgeApi = Substitute.For<VoidbridgeApi>();

            macauGamesResultService = new Product22ResultServiceImpl(config, logger, jsonConverter, voidbridgeApi);
        }

        [Fact]
        public void GetResultLink_NotHaveException_CallGetResultUrlFromApi()
        {
            // Arrange
            var query = new ThirdPartyResultBaseQuery { CustId = 1, GameId = "RefNo", RoleId = 2, Language = DefaultVariables.Language };

            // Act
            macauGamesResultService.GetResultLink(query);

            // Assert
            voidbridgeApi.Received().GetBetHistoryUrl(
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

            voidbridgeApi.GetBetHistoryUrl(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>())
                .Returns("Url");

            var expectedModel = new SuccessResult("Url");

            // Act
            var actualModel = macauGamesResultService.GetResultLink(query);

            // Assert
            Assert.True(compareLogic.Compare(expectedModel, actualModel).AreEqual);
        }

        [Fact]
        public void GetResultLink_HaveException_ReturnErrorThirdPartyResult()
        {
            // Arrange
            var query = new ThirdPartyResultBaseQuery { CustId = 1, GameId = "RefNo", RoleId = 2, Language = DefaultVariables.Language };

            voidbridgeApi.GetBetHistoryUrl(Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>())
                .Returns(x => { throw new Exception("ErrorMessage"); });

            var expectedModel = new ErrorResult("ErrorMessage");

            // Act
            var actualModel = macauGamesResultService.GetResultLink(query);

            // Assert
            Assert.True(compareLogic.Compare(expectedModel, actualModel).AreEqual);
        }
    }
}
