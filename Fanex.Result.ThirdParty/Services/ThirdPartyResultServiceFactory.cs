namespace Fanex.Result.ThirdParty.Services
{
    public interface ThirdPartyResultServiceFactory
    {
        ThirdPartyResultService CreateThirdPartyResultService(int productId);
    }
}
