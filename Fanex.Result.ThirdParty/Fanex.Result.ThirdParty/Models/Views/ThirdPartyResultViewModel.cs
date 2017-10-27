namespace Fanex.Result.ThirdParty.Models.Views
{
    public class ThirdPartyResultViewModel
    {
        public ThirdPartyResultViewModel(
            string productName,
            ThirdPartyResult thirdPartyResult)
        {
            Title = $"{productName} Result";
            ThirdPartyResult = thirdPartyResult;
        }

        public string Title { get; }

        public ThirdPartyResult ThirdPartyResult { get; }
    }
}
