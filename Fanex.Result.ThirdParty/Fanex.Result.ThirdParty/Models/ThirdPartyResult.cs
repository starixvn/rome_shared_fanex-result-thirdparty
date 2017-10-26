namespace Fanex.Result.ThirdParty.Models
{
    public class ThirdPartyResult
    {
        public ThirdPartyResult(
            bool isSuccess,
            string resultLink,
            string resultMessage)
        {
            IsSuccess = isSuccess;
            ResultLink = resultLink;
            ResultMessage = resultMessage;
        }

        public bool IsSuccess { get; }

        public string ResultLink { get; }

        public string ResultMessage { get; }
    }
}