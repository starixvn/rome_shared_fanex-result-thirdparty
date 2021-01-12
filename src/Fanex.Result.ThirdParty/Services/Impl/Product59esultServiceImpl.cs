namespace Fanex.Result.ThirdParty.Services.Impl
{
    using Configurations;
    using Fanex.Result.Core.GDG;
    using Logging;
    using Models;
    using Models.Requests;
    using RestSharp;

    public class Product59ResultServiceImpl : ResultServiceImpl
    {
        private readonly IGoldDiamondGamingResult result;

        public Product59ResultServiceImpl(
            ProductResultApiConfig config,
            ILogger logger,
            IRestClient restClient)
            : this(config, logger, restClient, null)
        {
        }

        public Product59ResultServiceImpl(
            ProductResultApiConfig config,
            ILogger logger,
            IRestClient restClient,
            IGoldDiamondGamingResult result)
            : base(config, logger, restClient)
        {
            this.result = result ?? new GoldDiamondGamingResult();
        }

        public override Result GetDetailLink(ThirdPartyResultBaseQuery request)
        {
            var resultLink = result.GetResult(
                request.CustId.ToString(),
                request.GameId,
                request.GameCode,                
                request.SiteId,
                request.Language);

            return new SuccessResult(resultLink);
        }
    }
}
