namespace Fanex.Result.ThirdParty.Services
{
    public interface ThirdPartyServiceResultFactory
    {
        ThirdPartyResultService CreateThirdPartyResultService(int productId);
    }
}
