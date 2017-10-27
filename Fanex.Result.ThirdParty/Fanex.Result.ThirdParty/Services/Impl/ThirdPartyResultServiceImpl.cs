namespace Fanex.Result.ThirdParty.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using Configurations;
    using Helpers;
    using Logging;
    using Models;
    using Models.Requests;
    using Newtonsoft.Json;
    using Sunplus.Agent.Model.BL.Definitions;
    using Utils;

    public class ThirdPartyResultServiceImpl : ThirdPartyResultService
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
        private readonly ThirdPartyResultApiConfig config;

        public ThirdPartyResultServiceImpl(ThirdPartyResultApiConfig config, ILogger logger)
        {
            this.config = config;
            this.logger = logger;
        }

        public virtual ThirdPartyResult GetResultDetails(ThirdPartyResultQuery request)
        {
            var toKen = GetToken(request.RoleId);

            if (toKen == null)
            {
                var errorMessage = $"Cannot connect to {config.ProductName} API. Please contact Administrator.";
                return new ThirdPartyResult(false, string.Empty, errorMessage);
            }

            if (!toKen.Result.ErrorCode.Equals("0"))
            {
                return new ThirdPartyResult(false, string.Empty, toKen.Result.ErrorDescription);
            }

            var parameters = GetHandHistoryParameters(toKen.Result.Token, request.RefNo, request.Language);
            var resultLink = string.Format(config.ResultUrl, parameters);

            return new ThirdPartyResult(true, resultLink, string.Empty);
        }

        protected virtual TokenResponse GetToken(int roleId)
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

                var responseString = WebClientHelper.UploadString(authorizationUrl, JsonConvert.SerializeObject(request));
                var result = JsonConvert.DeserializeObject<TokenResponse>(responseString);

                return result;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex.InnerException);
            }
            return null;
        }

        protected virtual string GetHandHistoryParameters(string token, string refno, string language)
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
            var siteIds = config.SiteIds.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            var index = roleId.Equals(UserTypes.Agent) ? 2
                    : roleId.Equals(UserTypes.Master) ? 1 : 0;

            return Convert.ToInt32(siteIds[index]);
        }

        protected virtual string GetLanguageCode(string language)
            => !string.IsNullOrEmpty(language) && SupportedLanguage.ContainsKey(language)
                ? SupportedLanguage[language]
                : SupportedLanguage[DefaultLanguage];
    }
}
