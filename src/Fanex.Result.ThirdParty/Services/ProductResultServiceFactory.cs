namespace Fanex.Result.ThirdParty.Services
{
    public interface ProductResultServiceFactory
    {
        ResultService CreateProductResultService(int productId);
    }
}
