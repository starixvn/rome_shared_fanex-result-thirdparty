namespace Fanex.Result.ThirdParty.Configurations
{
    using Common;
    using Fanex.Result.ThirdParty.Helpers;

    public interface ThirdPartyResultApiConfig
    {
        string ProductName { get; }

        string AuthorizationUrl { get; }

        string Password { get; }

        string[] SiteIds { get; }

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

            SiteIds = AppSettings.GetConfigValue($"{productId}:SiteIds")?.ToArray();
        }

        public string ProductName => AppSettings.GetConfigValue($"{productId}:ProductName");

        public string AuthorizationUrl => AppSettings.GetConfigValue($"{productId}:AuthorizationUrl");

        public string Password => AppSettings.GetConfigValue($"{productId}:Password");

        public string[] SiteIds { get; }

        public int VendorId => Converter.ToInt(AppSettings.GetConfigValue($"{productId}:VendorId"));

        public string Seq => AppSettings.GetConfigValue($"{productId}:Seq");

        public string ResultUrl => AppSettings.GetConfigValue($"{productId}:ResultUrl");

        public string Parameters => AppSettings.GetConfigValue($"{productId}:Parameters");

        public string EncryptedKey => AppSettings.GetConfigValue($"{productId}:EncryptedKey");
    }
}
