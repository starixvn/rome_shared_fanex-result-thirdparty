namespace Fanex.Result.ThirdParty.Tests.Configurations
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using ThirdParty.Configurations;
    using ThirdParty.Constants;
    using Xunit;

    public class ProductResultApiConfigImplTests
    {
        private readonly ProductResultApiConfigImpl thirdPartyResultApiConfig;

        public ProductResultApiConfigImplTests()
        {
            var productId = 6;
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["6:ProductName"] = "ProductName",
                ["6:AuthorizationUrl"] = "AuthorizationUrl",
                ["6:Password"] = "Password",
                ["6:SiteIds"] = "3200;3100;3000",
                ["6:VendorId"] = "10",
                ["6:Seq"] = "Seq",
                ["6:ResultUrl"] = "ResultUrl",
                ["6:Parameters"] = "Parameters",
                ["6:EncryptedKey"] = "EncryptedKey"
            };
            thirdPartyResultApiConfig = new ProductResultApiConfigImpl(productId);
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
        public void SiteIdMaps_HasConfig_ReturnCorrectSetting()
        {
            // Arrange
            var expectedSetting = new Dictionary<int, string>
            {
                [RoleIdConstants.Super] = "3200",
                [RoleIdConstants.Master] = "3100",
                [RoleIdConstants.Agent] = "3000"
            };

            // Act
            var actualSetting = thirdPartyResultApiConfig.SiteIdMaps;

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
