using Xunit;
namespace Fanex.Result.ThirdParty.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using Constants;
    using Core.Base.Models;
    using Core.GlobalGaming.DTO;
    using KellermanSoftware.CompareNetObjects;
    using Logging;
    using NSubstitute;
    using RestSharp;
    using ThirdParty.Configurations;
    using ThirdParty.Constants;
    using ThirdParty.Models;
    using ThirdParty.Models.Requests;
    using ThirdParty.Services;
    using ThirdParty.Services.Impl;
    using Xunit;

    public class Product30ResultServiceImplTests
    {
        private readonly CompareLogic compareLogic;
        private readonly ProductResultApiConfig config;
        private readonly ResultService service;
        private readonly IRestClient restClient;
        public Product30ResultServiceImplTests()
        {
            compareLogic = new CompareLogic();

            config = Substitute.For<ProductResultApiConfig>();
            var logger = Substitute.For<ILogger>();
            restClient = Substitute.For<IRestClient>();
            config = Substitute.For<ProductResultApiConfig>();
            config.ProductName.Returns("RNG Casino");
            config.DetailUrl.Returns("ResultUrl?o={0}");

            var siteIdMaps = new Dictionary<int, string>
            {
                [RoleIdConstants.Super] = "3300100",
                [RoleIdConstants.Master] = "3200100",
                [RoleIdConstants.Agent] = "3100100"
            };
            config.SiteIdMaps.Returns(siteIdMaps);
            config.AuthorizationUrl.Returns("http://testing.com");
            config.Password.Returns("testing");
            config.VendorId.Returns(12121);
            config.VendorKey.Returns("vendorkey");
            config.ResultUrl.Returns("");

            service = new Product30ResultServiceImpl(config, logger, restClient);
        }

        [Fact]
        public void GetResultLink_TokenHasTimeOutException_ThrowExceptionMessage()
        {
            // Arrange
            var query = new ThirdPartyResultBaseQuery { CustId = 1, GameId = "RefNo", RoleId = 2, Language = DefaultVariables.Language };
            restClient
                .Execute<BullfightingToken>(Arg.Any<RestRequest>())
                .Returns(new RestResponse<BullfightingToken>
                {
                    StatusCode = System.Net.HttpStatusCode.RequestTimeout,
                    ErrorMessage = "Time  out"
                });

            // Act && Assert
            Assert.Throws<Exception>(() => service.GetResultLink(query));
        }

        [Fact]
        public void GetResultLink_TokenReturnErrorCode_ThrowExceptionMessage()
        {
            // Arrange
            var query = new ThirdPartyResultBaseQuery { CustId = 1, GameId = "RefNo", RoleId = 2, Language = DefaultVariables.Language };
            restClient
                .Execute<BullfightingToken>(Arg.Any<RestRequest>())
                .Returns(new RestResponse<BullfightingToken>
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = new BullfightingToken { ErrorCode = "1212" }
                });

            // Act && Assert
            Assert.Throws<Exception>(() => service.GetResultLink(query));
        }

        [Fact]
        public void GetResultLink_HasToken_ReturnSuccessResult()
        {
            //// Arrange
            var query = new ThirdPartyResultBaseQuery { CustId = 1, GameId = "RefNo", RoleId = 2, Language = DefaultVariables.Language };
            restClient
               .Execute<BullfightingToken>(Arg.Any<RestRequest>())
               .Returns(new RestResponse<BullfightingToken>
               {
                   StatusCode = System.Net.HttpStatusCode.OK,
                   Data = new BullfightingToken
                   {
                       ErrorCode = "0",
                       Data = new AuthorizationData
                       {
                           Authorization = "bullfightingAuthorization"
                       }

                   }
               });

            //// Act
            var act = service.GetResultLink(query);

            //// Assert
            Assert.IsType<SuccessResult>(act);
        }

        [Theory]
        [InlineData(2, "3100100")]
        [InlineData(3, "3200100")]
        [InlineData(4, "3300100")]
        public void GetResultLink_CheckRoleId_ReturnCorrectLink(int roleId, string siteId)
        {
            //// Arrange
            var query = new ThirdPartyResultBaseQuery { CustId = 1, GameId = "RefNo", RoleId = roleId, Language = DefaultVariables.Language };
            config.ResultUrl.Returns("abc.com?b=#siteID#&c=#action#&d=#lang#&e=#authorization#");
            restClient
               .Execute<BullfightingToken>(Arg.Any<RestRequest>())
               .Returns(new RestResponse<BullfightingToken>
               {
                   StatusCode = System.Net.HttpStatusCode.OK,
                   Data = new BullfightingToken
                   {
                       ErrorCode = "0",
                       Data = new AuthorizationData
                       {
                           Authorization = "bullfightingAuthorization"

                       }

                   }
               });

            //// Act
            var act = service.GetResultLink(query);

            //// Assert
            Assert.Equal($"abc.com?b={siteId}&c=R&d=en&e=bullfightingAuthorization", act.Value);
        }

        [Theory]
        [InlineData("en-US", "en")]
        [InlineData("zh-TW", "ch")]
        [InlineData("zh-CN", "cs")]
        [InlineData("ja-JP", "jp")]
        [InlineData("ko-KR", "ko")]
        [InlineData("vi-VN", "vn")]
        [InlineData("th-TH", "th")]
        public void GetResultLink_CheckLanguage_ReturnCorrectLink(string language, string expectedLanguage)
        {
            //// Arrange
            var query = new ThirdPartyResultBaseQuery { CustId = 1, GameId = "RefNo", RoleId = 2, Language = language };
            config.ResultUrl.Returns("abc.com?b=#siteID#&c=#action#&d=#lang#&e=#authorization#");
            restClient
               .Execute<BullfightingToken>(Arg.Any<RestRequest>())
               .Returns(new RestResponse<BullfightingToken>
               {
                   StatusCode = System.Net.HttpStatusCode.OK,
                   Data = new BullfightingToken
                   {
                       ErrorCode = "0",
                       Data = new AuthorizationData
                       {
                           Authorization = "bullfightingAuthorization"

                       }

                   }
               });

            //// Act
            var act = service.GetResultLink(query);

            //// Assert
            Assert.Equal($"abc.com?b=3100100&c=R&d={expectedLanguage}&e=bullfightingAuthorization", act.Value);
        }

        [Fact]
        public void GetDetailLink_TokenIsSuccess_ReturnCorrectLink()
        {
            //// Arrange
            var query = new ThirdPartyResultBaseQuery { CustId = 1, GameId = "RefNo", RoleId = 2, Language = DefaultVariables.Language };
            config.DetailUrl.Returns("resultpage.com?a=#ticketID#&b=#siteID#&c=#action#&d=#lang#&e=#authorization#");
            restClient
               .Execute<BullfightingToken>(Arg.Any<RestRequest>())
               .Returns(new RestResponse<BullfightingToken>
               {
                   StatusCode = System.Net.HttpStatusCode.OK,
                   Data = new BullfightingToken
                   {
                       ErrorCode = "0",
                       Data = new AuthorizationData
                       {
                           Authorization = "bullfightingAuthorization"

                       }

                   }
               });

            //// Act
            var act = service.GetDetailLink(query);

            //// Assert
            Assert.Equal($"resultpage.com?a=RefNo&b=3100100&c=T&d=en&e=bullfightingAuthorization", act.Value);
        }
    }
}
