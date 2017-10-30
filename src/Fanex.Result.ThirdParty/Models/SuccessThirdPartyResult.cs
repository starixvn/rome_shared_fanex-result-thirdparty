namespace Fanex.Result.ThirdParty.Models
{
    public class SuccessThirdPartyResult : ThirdPartyResult
    {
        public SuccessThirdPartyResult(string resultLink)
            : base(true, resultLink)
        {
        }
    }
}
