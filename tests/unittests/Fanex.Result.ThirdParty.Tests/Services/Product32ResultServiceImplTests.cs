namespace Fanex.Result.ThirdParty.Tests.Services
{
    using System;
    using Constants;
    using Core.PP;
    using KellermanSoftware.CompareNetObjects;
    using Logging;
    using NSubstitute;
    using ThirdParty.Configurations;
    using ThirdParty.Models;
    using ThirdParty.Models.Requests;
    using ThirdParty.Services;
    using ThirdParty.Services.Impl;
    using Xunit;

    public class Product32ResultServiceImplTests
    {
        private readonly CompareLogic compareLogic;
        private readonly IPPResult ppResult;

        private readonly Product32ResultServiceImpl ppResultService;

        public Product32ResultServiceImplTests()
        {
            compareLogic = new CompareLogic();

            var config = Substitute.For<ProductResultApiConfig>();
            var logger = Substitute.For<ILogger>();
            var jsonConverter = Substitute.For<JsonConverter>();
            ppResult = Substitute.For<IPPResult>();

            ppResultService = new Product32ResultServiceImpl(config, logger, jsonConverter, ppResult);
        }

        [Fact]
        public void GetResultLink_NotHaveException_CallGetResultUrlFromApi()
        {
            // Arrange
            var query = new ThirdPartyResultBaseQuery { CustId = 1, GameId = "RefNo", RoleId = 2, Language = DefaultVariables.Language };

            // Act
            ppResultService.GetResultLink(query);

            // Assert
            ppResult.Received().GetPPResult(
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

            ppResult.GetPPResult(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>())
                .Returns("Url");

            var expectedModel = new SuccessResult("Url");

            // Act
            var actualModel = ppResultService.GetResultLink(query);

            // Assert
            Assert.True(compareLogic.Compare(expectedModel, actualModel).AreEqual);
        }

        [Fact]
        public void GetResultLink_HaveException_ReturnErrorThirdPartyResult()
        {
            // Arrange
            var query = new ThirdPartyResultBaseQuery { CustId = 1, GameId = "RefNo", RoleId = 2, Language = DefaultVariables.Language };

            ppResult.GetPPResult(Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>())
                .Returns(x => { throw new Exception("ErrorMessage"); });

            var expectedModel = new ErrorResult("ErrorMessage");

            // Act
            var actualModel = ppResultService.GetResultLink(query);

            // Assert
            Assert.True(compareLogic.Compare(expectedModel, actualModel).AreEqual);
        }
    }
}
