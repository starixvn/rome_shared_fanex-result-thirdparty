namespace Fanex.Result.ThirdParty.Tests.Helpers
{
    using ThirdParty.Helpers;
    using Xunit;

    public class SiteHelperTests
    {
        [Fact]
        public void GetSiteIdByRole_RoleIsNotContainsInMaps_ReturnDefaultSiteId()
        {
            // Arrange
            var roleId = 0;
            var defaultSiteId = "3200";

            // Act
            var siteId = SiteHelper.GetSiteIdByRole(roleId);

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
            var actualSiteId = SiteHelper.GetSiteIdByRole(roleId);

            // Assert
            Assert.Equal(siteId, actualSiteId);
        }
    }
}
