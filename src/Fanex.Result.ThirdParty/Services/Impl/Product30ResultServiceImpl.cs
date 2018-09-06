namespace Fanex.Result.ThirdParty.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Security.Policy;
    using Configurations;
    using Constants;
    using Helpers;
    using Logging;
    using Models;
    using Models.Requests;
    using RestSharp;

    public class Product30ResultServiceImpl : ResultService
    {
        private readonly ProductResultApiConfig config;
        private readonly IRestClient restClient;

        public Product30ResultServiceImpl(
            ProductResultApiConfig config,
            ILogger logger,
            IRestClient restClient)
        {
            this.config = config;
            this.restClient = restClient;
        }

        public Result GetResultLink(ThirdPartyResultBaseQuery request)
        {
            var token = GetToken(request.CustId);
            var siteId = GetSiteIdByRole(request.RoleId);
            var languageCode = GetLanguageCode(request.Language);

            var resultUrl = config.ResultUrl
                .Replace("#action#", "R")
                .Replace("#siteID#", siteId)
                .Replace("#lang#", languageCode)
                .Replace("#authorization#", token.Data != null ? token.Data.Authorization : string.Empty);

            return new SuccessResult(resultUrl);            
        }

        public Result GetDetailLink(ThirdPartyResultBaseQuery request)
        {
            var token = GetToken(request.CustId);
            var siteId = GetSiteIdByRole(request.RoleId);
            var languageCode = GetLanguageCode(request.Language);

            var resultUrl = config.DetailUrl
                .Replace("#ticketID#", request.GameId)
                .Replace("#action#", "T")
                .Replace("#siteID#", siteId)
                .Replace("#lang#", languageCode)
                .Replace("#authorization#", token.Data != null ? token.Data.Authorization : string.Empty);

            return new SuccessResult(resultUrl);
        }

        private string GetSiteIdByRole(int roleId)
            => SiteHelper.GetSiteIdByRole(roleId, config.SiteIdMaps, config.SiteIdMaps[RoleIdConstants.Super]);

        private BullfightingToken GetToken(int custId)
        {
            var authorizationUrl = config.AuthorizationUrl
                .Replace("#vendor#", config.VendorId.ToString())
                .Replace("#vendorKey#", config.VendorKey)
                .Replace("#customerID#", custId.ToString());

            restClient.BaseUrl = new Uri(authorizationUrl);
            var restRequest = new RestRequest();
            var response = restClient.Execute<BullfightingToken>(restRequest);

            if (HasException(response))
            {
                throw new Exception($"AuthorizationBullfighting Error UserID: {custId}{Environment.NewLine}" +
                                    $"ErrorMsg: {response.ErrorMessage ?? response.Data.ErrorMsg}{Environment.NewLine}" +
                                    $"Request URL: {restClient.BaseUrl}{Environment.NewLine}" +
                                    $"API Response: {response.Content} {Environment.NewLine}");
            }

            return response.Data;
        }

        private static bool HasException(IRestResponse<BullfightingToken> response)
            => response.StatusCode != System.Net.HttpStatusCode.OK || !response.Data.ErrorCode.Equals("0");

        private static string GetLanguageCode(string language) => 
            !string.IsNullOrEmpty(language) && LanguageConstants.Product30Language.ContainsKey(language)
            ? LanguageConstants.Product30Language[language]
            : LanguageConstants.Product30Language[LanguageConstants.DefaultLanguage];
    }

    public class BullfightingToken
    {
        public AuthorizationData Data { get; set; }

        public string ErrorCode { get; set; }

        public string ErrorMsg { get; set; }
    }

    public class AuthorizationData
    {
        public string Authorization { get; set; }
    }
}
