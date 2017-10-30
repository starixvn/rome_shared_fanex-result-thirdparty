namespace Fanex.Result.ThirdParty.Models.Views
{
    public class ThirdPartyResultViewModel
    {
        public ThirdPartyResultViewModel(
            string productName,
            Result thirdPartyResult)
        {
            Title = $"{productName} Result";
            ThirdPartyResult = thirdPartyResult;
        }

        public string Title { get; }

        public Result ThirdPartyResult { get; }
    }
}
