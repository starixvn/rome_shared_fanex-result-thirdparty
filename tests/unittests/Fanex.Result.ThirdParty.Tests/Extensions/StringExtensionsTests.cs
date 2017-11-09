namespace Fanex.Result.ThirdParty.Tests.Extensions
{
    using ThirdParty.Extensions;
    using Xunit;

    public class StringExtensionsTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ToArray_delimitedStringIsNullOrWhiteSpace_ReturnsCorrectValue(string delimitedString)
        {
            // Arrange
            var expectedValue = new string[0];

            // Act
            var actualValue = delimitedString.ToArray();

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void ToArray_delimitedStringCorrectFormat_ReturnsCorrectValue()
        {
            // Arrange
            var value = "3200;3100;3000";
            var expectedValue = new[] { "3200", "3100", "3000" };

            // Act
            var actualValue = value.ToArray();

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }
    }
}
