namespace Fanex.Result.ThirdParty.Tests.Services
{
    using Constants;
    using Fanex.Result.Core.GDG;
    using KellermanSoftware.CompareNetObjects;
    using Logging;
    using NSubstitute;
    using RestSharp;
    using ThirdParty.Configurations;
    using ThirdParty.Models.Requests;
    using ThirdParty.Services.Impl;
    using Xunit;

    public class Product59ResultServiceImplTests
    {
        private readonly IRestClient restClient;
        private readonly Product59ResultServiceImpl resultService;
        private readonly IGoldDiamondGamingResult result;

        public Product59ResultServiceImplTests()
        {
            restClient = Substitute.For<IRestClient>();
            var config = Substitute.For<ProductResultApiConfig>();
            var logger = Substitute.For<ILogger>();
            result = Substitute.For<IGoldDiamondGamingResult>();

            resultService = new Product59ResultServiceImpl(config, logger, restClient, result);
        }

        [Fact]
        public void GetResultLink_Always_CallGDGResultAPI()
        {
            // Arrange
            var query = new ThirdPartyResultBaseQuery { CustId = 1, GameId = "gameid", GameCode = "gamecode", RoleId = 2, Language = DefaultVariables.Language };

            // Act
            resultService.GetDetailLink(query);

            // Assert
            //result.Received().GetResult(
            //    Arg.Is<string>(i => i == "1"),
            //    Arg.Is<string>(i => i == "gameid"),
            //    Arg.Is<string>(i => i == "gamecode"),
            //    Arg.Is<string>(i => i == "en-us"),
            //    Arg.Is<string>(i => i == "3000"));
            result.Received().GetResult(
                Arg.Is<string>(i => i == "1"),
                Arg.Is<string>(i => i == "gameid"),
                Arg.Is<string>(i => i == "gamecode"),
                Arg.Is<string>(i => i == "en-Us"),
                Arg.Is<string>(i => i == "3000"));
        }
    }
}
