namespace Fanex.Result.ThirdParty.Configurations
{
    using System.Collections.Generic;
    using Common;
    using Constants;
    using Helpers;

    public interface ThirdPartyResultApiConfig
    {
        string ProductName { get; }

        string AuthorizationUrl { get; }

        string Password { get; }

        Dictionary<int, string> SiteIdMaps { get; }

        int VendorId { get; }

        string Seq { get; }

        string ResultUrl { get; }

        string Parameters { get; }

        string EncryptedKey { get; }
    }

    public class ThirdPartyResultApiConfigImpl : ThirdPartyResultApiConfig
    {
        private readonly int productId;

        public ThirdPartyResultApiConfigImpl(int productId)
        {
            this.productId = productId;

            var siteIds = AppSettings.GetConfigValue($"{productId}:SiteIds")?.ToArray();

            SiteIdMaps = siteIds == null
                ? new Dictionary<int, string>()
                : new Dictionary<int, string>
                {
                    [RoleIdConstants.Super] = siteIds[0],
                    [RoleIdConstants.Master] = siteIds[1],
                    [RoleIdConstants.Agent] = siteIds[2]
                };
        }

        public string ProductName => AppSettings.GetConfigValue($"{productId}:ProductName");

        public string AuthorizationUrl => AppSettings.GetConfigValue($"{productId}:AuthorizationUrl");

        public string Password => AppSettings.GetConfigValue($"{productId}:Password");

        public Dictionary<int, string> SiteIdMaps { get; }

        public int VendorId => Converter.ToInt(AppSettings.GetConfigValue($"{productId}:VendorId"));

        public string Seq => AppSettings.GetConfigValue($"{productId}:Seq");

        public string ResultUrl => AppSettings.GetConfigValue($"{productId}:ResultUrl");

        public string Parameters => AppSettings.GetConfigValue($"{productId}:Parameters");

        public string EncryptedKey => AppSettings.GetConfigValue($"{productId}:EncryptedKey");
    }
}
