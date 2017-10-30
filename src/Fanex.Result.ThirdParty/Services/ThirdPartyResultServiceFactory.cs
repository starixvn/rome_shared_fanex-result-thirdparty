namespace Fanex.Result.ThirdParty.Services
{
    public interface ThirdPartyResultServiceFactory
    {
        ResultService CreateThirdPartyResultService(int productId);
    }
}
