namespace Fanex.Result.ThirdParty.Services
{
    using Models;
    using Models.Requests;

    public interface ResultService
    {
        Result GetResultLink(ThirdPartyResultQuery request);
    }
}
