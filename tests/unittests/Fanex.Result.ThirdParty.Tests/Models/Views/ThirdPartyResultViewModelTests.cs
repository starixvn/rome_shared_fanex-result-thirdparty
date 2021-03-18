namespace Fanex.Result.ThirdParty.Tests.Models.Views
{
    using ThirdParty.Models.Views;
    using Xunit;

    public class ThirdPartyResultViewModelTests
    {
        [Fact]
        public void Title_Always_ReturnsCorrectValue()
        {
            // Arrange
            var productName = "ProductName";
            var expectedTitle = "ProductName";

            // Act
            var viewModel = new ThirdPartyResultViewModel(productName, null);

            // Assert
            Assert.Equal(expectedTitle, viewModel.Title);
        }
    }
}
