namespace Fanex.Result.ThirdParty.Models
{
    public class ErrorThirdPartyResult : ThirdPartyResult
    {
        public ErrorThirdPartyResult(string resultMessage)
            : base(false, resultMessage)
        {
        }
    }
}
