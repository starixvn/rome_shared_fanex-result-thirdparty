namespace Fanex.Result.ThirdParty.Tests.Services
{
    using System;
    using Constants;
    using Core.Base.Models;
    using Core.GlobalGaming.DTO;
    using KellermanSoftware.CompareNetObjects;
    using Logging;
    using Models;
    using Models.Requests;
    using NSubstitute;
    using ThirdParty.Configurations;
    using ThirdParty.Services;
    using ThirdParty.Services.Impl;
    using Xunit;

    public class Product24ResultServiceImplTests
    {
        private readonly CompareLogic compareLogic;
        private readonly Base3rdResultApi resultApi;
        private readonly Product24ResultServiceImpl fishingWorldResultService;

        public Product24ResultServiceImplTests()
        {
            compareLogic = new CompareLogic();

            var config = Substitute.For<ProductResultApiConfig>();
            var logger = Substitute.For<ILogger>();
            var jsonConverter = Substitute.For<JsonConverter>();
            resultApi = Substitute.ForPartsOf<Base3rdResultApi>();

            fishingWorldResultService = new Product24ResultServiceImpl(config, logger, jsonConverter, resultApi);
        }

        [Fact]
        public void GetResultLink_NotHaveException_CallGetResultUrlFromApi()
        {
            // Arrange
            var query = new ThirdPartyResultBaseQuery { CustId = 1, RefNo = "RefNo", RoleId = 2, Language = DefaultVariables.Language };

            // Act
            fishingWorldResultService.GetResultLink(query);

            // Assert
            resultApi.Received().GetResultUrl(Arg.Is<GlobalGamingDTO>(
                param => param.CustId == query.CustId.ToString()
                    && param.GameId == query.RefNo
                    && param.Language == query.Language
                    && param.SiteId == "3000"));
        }

        [Fact]
        public void GetResultLink_NotHaveException_ReturnSuccessThirdPartyResult()
        {
            // Arrange
            var query = new ThirdPartyResultBaseQuery { CustId = 1, RefNo = "RefNo", RoleId = 2, Language = DefaultVariables.Language };

            resultApi.GetResultUrl(Arg.Any<GlobalGamingDTO>()).Returns("Url");

            var expectedModel = new SuccessResult("Url");

            // Act
            var actualModel = fishingWorldResultService.GetResultLink(query);

            // Assert
            Assert.True(compareLogic.Compare(expectedModel, actualModel).AreEqual);
        }

        [Fact]
        public void GetResultLink_HaveException_ReturnErrorThirdPartyResult()
        {
            // Arrange
            var query = new ThirdPartyResultBaseQuery { CustId = 1, RefNo = "RefNo", RoleId = 2, Language = DefaultVariables.Language };

            resultApi.GetResultUrl(Arg.Any<GlobalGamingDTO>()).Returns(x => { throw new Exception("ErrorMessage"); });

            var expectedModel = new ErrorResult("ErrorMessage");

            // Act
            var actualModel = fishingWorldResultService.GetResultLink(query);

            // Assert
            Assert.True(compareLogic.Compare(expectedModel, actualModel).AreEqual);
        }
    }
}
