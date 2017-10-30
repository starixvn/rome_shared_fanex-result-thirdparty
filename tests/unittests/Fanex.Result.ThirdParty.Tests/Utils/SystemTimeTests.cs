using System;
using Fanex.Result.ThirdParty.Utils;
using Xunit;

namespace Fanex.Result.ThirdParty.Tests.Utils
{
    public class SystemTimeTests
    {
        [Fact]
        public void Now_Always_ReturnsValueSetToNowProperty()
        {
            // Arrange
            var expectedDate = DateTime.Now;
            SystemTime.Now = expectedDate;

            // Act
            var actualDate = SystemTime.Now;

            // Assert
            Assert.Equal(expectedDate, actualDate);
        }
    }
}
