namespace Fanex.Result.ThirdParty.Models.Views
{
    using Resources;

    public class ThirdPartyResultViewModel
    {
        public ThirdPartyResultViewModel(
            string productName,
            ThirdPartyResult thirdPartyResult)
        {
            Title = $"{productName} {Resources.Result}";
            ThirdPartyResult = thirdPartyResult;
        }

        public string Title { get; }

        public ThirdPartyResult ThirdPartyResult { get; }
    }
}
