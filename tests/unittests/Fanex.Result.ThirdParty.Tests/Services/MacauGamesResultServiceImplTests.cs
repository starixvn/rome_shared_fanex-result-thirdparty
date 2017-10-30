namespace Fanex.Result.ThirdParty.Tests.Services
{
    using System;
    using Constants;
    using Core.Voidbridge;
    using KellermanSoftware.CompareNetObjects;
    using Models;
    using Models.Requests;
    using NSubstitute;
    using ThirdParty.Services.Impl;
    using Xunit;

    public class MacauGamesResultServiceImplTests
    {
        private readonly CompareLogic compareLogic;
        private readonly VoidbridgeApi voidbridgeApi;
        private readonly MacauGamesResultServiceImpl macauGamesResultService;

        public MacauGamesResultServiceImplTests()
        {
            compareLogic = new CompareLogic();
            voidbridgeApi = Substitute.For<VoidbridgeApi>();
            macauGamesResultService = new MacauGamesResultServiceImpl(voidbridgeApi);
        }

        [Fact]
        public void GetResultLink_NotHaveException_CallGetResultUrlFromApi()
        {
            // Arrange
            var query = new ThirdPartyResultQuery { CustId = 1, RefNo = "RefNo", RoleId = 2, Language = DefaultVariables.Language };

            // Act
            macauGamesResultService.GetResultLink(query);

            // Assert
            voidbridgeApi.Received().GetBetHistoryUrl(
                Arg.Is(query.CustId.ToString()),
                Arg.Is(query.RefNo),
                Arg.Is(query.Language),
                "3000");
        }

        [Fact]
        public void GetResultLink_NotHaveException_ReturnSuccessThirdPartyResult()
        {
            // Arrange
            var query = new ThirdPartyResultQuery { CustId = 1, RefNo = "RefNo", RoleId = 2, Language = DefaultVariables.Language };

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
            var query = new ThirdPartyResultQuery { CustId = 1, RefNo = "RefNo", RoleId = 2, Language = DefaultVariables.Language };

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
