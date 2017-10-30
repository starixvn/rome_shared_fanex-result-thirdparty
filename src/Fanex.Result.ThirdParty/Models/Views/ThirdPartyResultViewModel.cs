namespace Fanex.Result.ThirdParty.Models.Views
{
    public class ThirdPartyResultViewModel
    {
        public ThirdPartyResultViewModel(
            string productName,
            Result result)
        {
            Title = $"{productName} Result";
            Result = result;
        }

        public string Title { get; }

        public Result Result { get; }
    }
}
