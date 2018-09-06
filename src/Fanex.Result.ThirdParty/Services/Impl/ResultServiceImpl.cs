namespace Fanex.Result.ThirdParty.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using Common;
    using Configurations;
    using Constants;
    using Helpers;
    using Logging;
    using Models;
    using Models.Requests;
    using RestSharp;
    using Utils;

    public class ResultServiceImpl : ResultService
    {
        private readonly ILogger logger;
        private readonly ProductResultApiConfig config;
        private readonly IRestClient restClient;

        public ResultServiceImpl(
            ProductResultApiConfig config,
            ILogger logger,
            IRestClient restClient)
        {
            this.config = config;
            this.logger = logger;
            this.restClient = restClient;
        }

        public Result GetResultLink(ThirdPartyResultBaseQuery request)
        {
            throw new NotImplementedException();
        }

        public virtual Result GetDetailLink(ThirdPartyResultBaseQuery request)
        {
            var token = GetToken(request.CustId);

            if (token == null)
            {
                var resultMessage = $"Cannot connect to {config.ProductName} API. Please contact Administrator.";
                return new ErrorResult(resultMessage);
            }

            if (!token.Result.ErrorCode.Equals("0"))
            {
                return new ErrorResult(token.Result.ErrorDescription);
            }

            var parameters = GetParameters(token.Result.Token, request.GameId, request.Language);
            var resultLink = string.Format(config.DetailUrl, parameters);

            return new SuccessResult(resultLink);
        }

        protected internal virtual TokenResponse GetToken(int roleId)
        {
            restClient.BaseUrl = new Uri(config.AuthorizationUrl);
            var siteId = GetSiteIdByRole(roleId);
            var request = new Request
            {
                Timestamp = SystemTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"),
                Auth = new Auth("Login", config.Password, siteId),
                Call = new Call(config.VendorKey, config.VendorId)
            };
            var restRequest = new RestRequest(Method.POST);
            restRequest.AddJsonBody(request);

            var response = restClient.Execute<TokenResponse>(restRequest);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var message = $"ErrorMsg: {response.ErrorMessage}{Environment.NewLine}" +
                                    $"Request URL: {restClient.BaseUrl}{Environment.NewLine}" +
                                    $"API Response: {response.Content} {Environment.NewLine}";
                logger.Error(message);

                return null;
            }

            return response.Data;
        }

        protected internal virtual string GetParameters(string token, string refno, string language)
        {
            var encryptedKey = config.EncryptedKey;
            var param = string.Format(
                config.Parameters,
                token,
                refno,
                GetLanguageCode(language));
            var result = HttpUtility.UrlEncode(IBCCrypt.IBCCrypt.Encrypt(param, encryptedKey));

            return result;
        }

        protected virtual int GetSiteIdByRole(int roleId)
        {
            var siteId = SiteHelper.GetSiteIdByRole(roleId, config.SiteIdMaps, config.SiteIdMaps[RoleIdConstants.Super]);

            return Converter.ToInt(siteId);
        }

        protected internal virtual string GetLanguageCode(string language)
            => !string.IsNullOrEmpty(language) && LanguageConstants.SupportedLanguage.ContainsKey(language)
                ? LanguageConstants.SupportedLanguage[language]
                : LanguageConstants.SupportedLanguage[LanguageConstants.DefaultLanguage];
    }
}
