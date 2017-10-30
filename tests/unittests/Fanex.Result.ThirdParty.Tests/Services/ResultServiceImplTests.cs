namespace Fanex.Result.ThirdParty.Tests.Services
{
    using System;
    using KellermanSoftware.CompareNetObjects;
    using Logging;
    using Models;
    using Models.Requests;
    using NSubstitute;
    using ThirdParty.Configurations;
    using ThirdParty.Helpers;
    using ThirdParty.Services;
    using ThirdParty.Services.Impl;
    using Xunit;

    public class ResultServiceImplTests
    {
        private readonly CompareLogic compareLogic;
        private readonly ThirdPartyResultApiConfig config;
        private readonly ILogger logger;
        private readonly JsonConverter jsonConverter;
        private readonly ResultServiceImpl thirdPartyResultService;

        public ResultServiceImplTests()
        {
            compareLogic = new CompareLogic();

            config = Substitute.For<ThirdPartyResultApiConfig>();
            config.ProductName.Returns("RNG Casino");
            config.ResultUrl.Returns("ResultUrl?o={0}");
            config.SiteIds.Returns(new string[] { "3200", "3100", "3000" });

            logger = Substitute.For<ILogger>();
            jsonConverter = Substitute.For<JsonConverter>();

            thirdPartyResultService = Substitute.ForPartsOf<ResultServiceImpl>(config, logger, jsonConverter);
        }

        [Fact]
        public void GetResultLink_TokenIsNull_ReturnErrorThirdPartyResult()
        {
            // Arrange
            var query = new ThirdPartyResultQuery()
            {
                RoleId = 2
            };

            var expectModel = new ErrorThirdPartyResult("Cannot connect to RNG Casino API. Please contact Administrator.");

            // Act
            var actualModel = thirdPartyResultService.GetResultLink(query);

            // Assert
            Assert.True(compareLogic.Compare(expectModel, actualModel).AreEqual);
        }

        [Fact]
        public void GetResultLink_ErrorCodeIsNotZero_ReturnErrorThirdPartyResult()
        {
            // Arrange
            var query = new ThirdPartyResultQuery()
            {
                RoleId = 2
            };

            thirdPartyResultService.GetToken(Arg.Any<int>()).Returns(new TokenResponse { Result = new TokenResult { ErrorCode = "1", ErrorDescription = "ErrorMessage" } });

            var expectModel = new ErrorThirdPartyResult("ErrorMessage");

            // Act
            var actualModel = thirdPartyResultService.GetResultLink(query);

            // Assert
            Assert.True(compareLogic.Compare(expectModel, actualModel).AreEqual);
        }

        [Fact]
        public void GetResultLink_ErrorCodeIsZero_ReturnSuccessThirdPartyResult()
        {
            // Arrange
            var query = new ThirdPartyResultQuery()
            {
                RoleId = 2
            };

            thirdPartyResultService.GetToken(Arg.Any<int>()).Returns(new TokenResponse { Result = new TokenResult { ErrorCode = "0", ErrorDescription = string.Empty } });
            thirdPartyResultService.GetParameters(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns("Parameter");

            var expectModel = new SuccessThirdPartyResult("ResultUrl?o=Parameter");

            // Act
            var actualModel = thirdPartyResultService.GetResultLink(query);

            // Assert
            Assert.True(compareLogic.Compare(expectModel, actualModel).AreEqual);
        }

        [Fact]
        public void GetToken_Always_CallsDeserializeObject()
        {
            // Arrange
            var uploadStringFunc = Substitute.For<Func<string, string, string>>();
            WebClientHelper.CallHttpPost = uploadStringFunc;

            // Act
            thirdPartyResultService.GetToken(roleId: 2);

            // Assert
            jsonConverter.Received().DeserializeObject<TokenResponse>(Arg.Any<string>());
        }

        [Theory]
        [InlineData("en-US", "en")]
        [InlineData("zh-TW", "ch")]
        [InlineData("zh-CN", "cs")]
        [InlineData("ja-JP", "jp")]
        [InlineData("ko-KR", "ko")]
        [InlineData("vi-VN", "vi")]
        [InlineData("th-TH", "th")]
        public void GetLanguageCode_SupportedLanguage_RetrunCorrectLanguageCode(string language, string expectedLanguageCode)
        {
            // Act
            var actualLanguageCode = thirdPartyResultService.GetLanguageCode(language);

            // Assert
            Assert.Equal(expectedLanguageCode, actualLanguageCode);
        }

        [Fact]
        public void GetLanguageCode_NotSupportedLanguage_RetrunDefaultLanguageCode()
        {
            // Arrange
            var expectedLanguageCode = "en";

            // Act
            var actualLanguageCode = thirdPartyResultService.GetLanguageCode("abc");

            // Assert
            Assert.Equal(expectedLanguageCode, actualLanguageCode);
        }
    }
}
