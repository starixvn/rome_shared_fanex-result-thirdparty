namespace Fanex.Result.ThirdParty.Services
{
    using Models;
    using Models.Requests;

    public interface ResultService
    {
        Result GetDetailLink(ThirdPartyResultBaseQuery request);

        Result GetResultLink(ThirdPartyResultBaseQuery request);
    }
}
