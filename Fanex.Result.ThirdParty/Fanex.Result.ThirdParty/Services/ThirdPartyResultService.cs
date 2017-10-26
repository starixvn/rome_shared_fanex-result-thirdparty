namespace Fanex.Result.ThirdParty.Services
{
    using Models;
    using Models.Requests;

    public interface ThirdPartyResultService
    {
        ThirdPartyResult GetResultDetails(ThirdPartyResultQuery request);
    }
}
