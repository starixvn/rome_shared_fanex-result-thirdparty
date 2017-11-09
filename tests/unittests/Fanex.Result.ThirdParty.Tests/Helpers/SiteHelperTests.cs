namespace Fanex.Result.ThirdParty.Tests.Helpers
{
    using System.Collections.Generic;
    using ThirdParty.Constants;
    using ThirdParty.Helpers;
    using Xunit;

    public class SiteHelperTests
    {
        private static readonly Dictionary<int, string> SiteIdMaps = new Dictionary<int, string>
        {
            [RoleIdConstants.Super] = "3200",
            [RoleIdConstants.Master] = "3100",
            [RoleIdConstants.Agent] = "3000"
        };

        [Fact]
        public void GetSiteIdByRole_RoleIsNotContainsInMaps_ReturnDefaultSiteId()
        {
            // Arrange
            var roleId = 0;
            var defaultSiteId = "3200";

            // Act
            var siteId = SiteHelper.GetSiteIdByRole(roleId, SiteIdMaps, defaultSiteId);

            // Assert
            Assert.Equal(defaultSiteId, siteId);
        }

        [Theory]
        [InlineData(2, "3000")]
        [InlineData(3, "3100")]
        [InlineData(4, "3200")]
        public void GetSiteIdByRole_RoleIsContainsInMaps_ReturnCorrectSiteId(int roleId, string siteId)
        {
            // Act
            var actualSiteId = SiteHelper.GetSiteIdByRole(roleId, SiteIdMaps, "3200");

            // Assert
            Assert.Equal(siteId, actualSiteId);
        }
    }
}
