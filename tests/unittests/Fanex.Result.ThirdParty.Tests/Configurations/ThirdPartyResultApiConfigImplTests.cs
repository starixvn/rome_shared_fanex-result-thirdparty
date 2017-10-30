namespace Fanex.Result.ThirdParty.Tests.Configurations
{
    using System.Collections.Specialized;
    using ThirdParty.Configurations;
    using Xunit;

    public class ThirdPartyResultApiConfigImplTests
    {
        private readonly ThirdPartyResultApiConfigImpl thirdPartyResultApiConfig;

        public ThirdPartyResultApiConfigImplTests()
        {
            var productId = 6;
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["6:ProductName"] = "ProductName",
                ["6:AuthorizationUrl"] = "AuthorizationUrl",
                ["6:Password"] = "Password",
                ["6:SiteIds"] = "3000;3100;3200",
                ["6:VendorId"] = "10",
                ["6:Seq"] = "Seq",
                ["6:ResultUrl"] = "ResultUrl",
                ["6:Parameters"] = "Parameters",
                ["6:EncryptedKey"] = "EncryptedKey"
            };
            thirdPartyResultApiConfig = new ThirdPartyResultApiConfigImpl(productId);
        }

        [Fact]
        public void ProductName_HasConfig_ReturnCorrectSetting()
        {
            // Arrange
            var expectedSetting = "ProductName";

            // Act
            var actualSetting = thirdPartyResultApiConfig.ProductName;

            // Assert
            Assert.Equal(expectedSetting, actualSetting);
        }

        [Fact]
        public void AuthorizationUrl_HasConfig_ReturnCorrectSetting()
        {
            // Arrange
            var expectedSetting = "AuthorizationUrl";

            // Act
            var actualSetting = thirdPartyResultApiConfig.AuthorizationUrl;

            // Assert
            Assert.Equal(expectedSetting, actualSetting);
        }

        [Fact]
        public void Password_HasConfig_ReturnCorrectSetting()
        {
            // Arrange
            var expectedSetting = "Password";

            // Act
            var actualSetting = thirdPartyResultApiConfig.Password;

            // Assert
            Assert.Equal(expectedSetting, actualSetting);
        }

        [Fact]
        public void SiteIds_HasConfig_ReturnCorrectSetting()
        {
            // Arrange
            var expectedSetting = new string[] { "3000", "3100", "3200" };

            // Act
            var actualSetting = thirdPartyResultApiConfig.SiteIds;

            // Assert
            Assert.Equal(expectedSetting, actualSetting);
        }

        [Fact]
        public void VendorId_HasConfig_ReturnCorrectSetting()
        {
            // Arrange
            var expectedSetting = 10;

            // Act
            var actualSetting = thirdPartyResultApiConfig.VendorId;

            // Assert
            Assert.Equal(expectedSetting, actualSetting);
        }

        [Fact]
        public void Seq_HasConfig_ReturnCorrectSetting()
        {
            // Arrange
            var expectedSetting = "Seq";

            // Act
            var actualSetting = thirdPartyResultApiConfig.Seq;

            // Assert
            Assert.Equal(expectedSetting, actualSetting);
        }

        [Fact]
        public void ResultUrl_HasConfig_ReturnCorrectSetting()
        {
            // Arrange
            var expectedSetting = "ResultUrl";

            // Act
            var actualSetting = thirdPartyResultApiConfig.ResultUrl;

            // Assert
            Assert.Equal(expectedSetting, actualSetting);
        }

        [Fact]
        public void Parameters_HasConfig_ReturnCorrectSetting()
        {
            // Arrange
            var expectedSetting = "Parameters";

            // Act
            var actualSetting = thirdPartyResultApiConfig.Parameters;

            // Assert
            Assert.Equal(expectedSetting, actualSetting);
        }

        [Fact]
        public void EncryptedKey_HasConfig_ReturnCorrectSetting()
        {
            // Arrange
            var expectedSetting = "EncryptedKey";

            // Act
            var actualSetting = thirdPartyResultApiConfig.EncryptedKey;

            // Assert
            Assert.Equal(expectedSetting, actualSetting);
        }
    }
}
