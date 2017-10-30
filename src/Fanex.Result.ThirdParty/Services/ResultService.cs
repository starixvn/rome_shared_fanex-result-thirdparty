namespace Fanex.Result.ThirdParty.Services
{
    using Models;
    using Models.Requests;

    public interface ResultService
    {
        ThirdPartyResult GetResultLink(ThirdPartyResultQuery request);
    }
}
