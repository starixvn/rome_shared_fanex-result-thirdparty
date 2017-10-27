namespace Fanex.Result.ThirdParty.Configurations
{
    using System.Collections.Specialized;
    using System.Configuration;
    using Common;

    public interface ThirdPartyResultApiConfig
    {
        string ProductName { get; }

        string AuthorizationUrl { get; }

        string Password { get; }

        string SiteIds { get; }

        int VendorId { get; }

        string Seq { get; }

        string ResultUrl { get; }

        string Parameters { get; }

        string EncryptedKey { get; }
    }

    public class ThirdPartyResultApiConfigImpl : ThirdPartyResultApiConfig
    {
        private readonly int productId;
        private NameValueCollection configValues;

        public ThirdPartyResultApiConfigImpl(int productId)
        {
            this.productId = productId;
        }

        internal NameValueCollection ConfigValues
        {
            get { return configValues ?? ConfigurationManager.AppSettings; }
            set { configValues = value; }
        }

        public string ProductName => GetConfigValue($"{productId}:ProductName");

        public string AuthorizationUrl => GetConfigValue($"{productId}:AuthorizationUrl");

        public string Password => GetConfigValue($"{productId}:Password");

        public string SiteIds => GetConfigValue($"{productId}:SiteIds");

        public int VendorId => Converter.ToInt(GetConfigValue($"{productId}:VendorId"));

        public string Seq => GetConfigValue($"{productId}:Seq");

        public string ResultUrl => GetConfigValue($"{productId}:ResultUrl");

        public string Parameters => GetConfigValue($"{productId}:Parameters");

        public string EncryptedKey => GetConfigValue($"{productId}:EncryptedKey");

        public string GetConfigValue(string configName) => ConfigValues[configName];
    }
}
