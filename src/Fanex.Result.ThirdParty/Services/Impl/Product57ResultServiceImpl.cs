namespace Fanex.Result.ThirdParty.Services.Impl
{
    using Configurations;
    using Fanex.Result.Core.BG;
    using Logging;
    using Models;
    using Models.Requests;
    using RestSharp;

    public class Product57ResultServiceImpl : ResultServiceImpl
    {
        private readonly IBigGamingResult result;

        public Product57ResultServiceImpl(
            ProductResultApiConfig config,
            ILogger logger,
            IRestClient restClient)
            : this(config, logger, restClient, null)
        {
        }

        public Product57ResultServiceImpl(
            ProductResultApiConfig config,
            ILogger logger,
            IRestClient restClient,
            IBigGamingResult result)
            : base(config, logger, restClient)
        {
            this.result = result ?? new BigGamingResult();
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
