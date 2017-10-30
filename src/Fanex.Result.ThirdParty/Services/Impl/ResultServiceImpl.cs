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
    using Utils;

    public class ResultServiceImpl : ResultService
    {
        private static readonly Dictionary<string, string> SupportedLanguage = new Dictionary<string, string>()
        {
            ["en-US"] = "en",
            ["zh-TW"] = "ch",
            ["zh-CN"] = "cs",
            ["ja-JP"] = "jp",
            ["ko-KR"] = "ko",
            ["vi-VN"] = "vi",
            ["th-TH"] = "th",
        };
        private const string DefaultLanguage = "en-US";
        private readonly ILogger logger;
        private readonly JsonConverter jsonConverter;
        private readonly ThirdPartyResultApiConfig config;

        public ResultServiceImpl(ThirdPartyResultApiConfig config, ILogger logger, JsonConverter jsonConverter)
        {
            this.config = config;
            this.logger = logger;
            this.jsonConverter = jsonConverter;
        }

        public virtual ThirdPartyResult GetResultLink(ThirdPartyResultQuery request)
        {
            var toKen = GetToken(request.RoleId);

            if (toKen == null)
            {
                var resultMessage = $"Cannot connect to {config.ProductName} API. Please contact Administrator.";
                return new ErrorThirdPartyResult(resultMessage);
            }

            if (!toKen.Result.ErrorCode.Equals("0"))
            {
                return new ErrorThirdPartyResult(toKen.Result.ErrorDescription);
            }

            var parameters = GetParameters(toKen.Result.Token, request.RefNo, request.Language);
            var resultLink = string.Format(config.ResultUrl, parameters);

            return new SuccessThirdPartyResult(resultLink);
        }

        protected internal virtual TokenResponse GetToken(int roleId)
        {
            try
            {
                var siteId = GetSiteIdByRole(roleId);
                var authorizationUrl = string.Format(config.AuthorizationUrl);

                var request = new Request
                {
                    Timestamp = SystemTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"),
                    Auth = new Auth
                    {
                        Login = "Login",
                        Password = config.Password,
                        SiteId = siteId
                    },
                    Call = new Call
                    {
                        Seq = config.Seq,
                        VendorId = config.VendorId
                    }
                };

                var responseString = WebClientHelper.CallHttpPost(authorizationUrl, jsonConverter.SerializeObject(request));
                var result = jsonConverter.DeserializeObject<TokenResponse>(responseString);

                return result;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex.InnerException);
            }
            return null;
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
            var siteIds = config.SiteIds;

            var siteIdMaps = new Dictionary<int, string>
            {
                [RoleIdConstants.Super] = siteIds[0],
                [RoleIdConstants.Master] = siteIds[1],
                [RoleIdConstants.Agent] = siteIds[2]
            };

            var siteId = SiteHelper.GetSiteIdByRole(roleId, siteIdMaps, siteIdMaps[RoleIdConstants.Super]);

            return Converter.ToInt(siteId);
        }

        protected internal virtual string GetLanguageCode(string language)
            => !string.IsNullOrEmpty(language) && SupportedLanguage.ContainsKey(language)
                ? SupportedLanguage[language]
                : SupportedLanguage[DefaultLanguage];
    }
}
