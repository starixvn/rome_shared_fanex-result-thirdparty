namespace Fanex.Result.ThirdParty.Services.Impl
{
    using Configurations;
    using Fanex.Result.Core.Casino;
    using Helpers;
    using Logging;
    using Models;
    using Models.Requests;
    using RestSharp;

    public class Product36ResultServiceImpl : ResultServiceImpl
    {
        private readonly ICasinoResult result;

        public Product36ResultServiceImpl(
            ProductResultApiConfig config,
            ILogger logger,
            IRestClient restClient)
            : this(config, logger, restClient, null)
        {
        }

        public Product36ResultServiceImpl(
            ProductResultApiConfig config,
            ILogger logger,
            IRestClient restClient,
            ICasinoResult result)
            : base(config, logger, restClient)
        {
            this.result = result ?? new CasinoResult("SpadegamingResultConfigs");
        }

        public override Result GetDetailLink(ThirdPartyResultBaseQuery request)
        {
            var siteId = SiteHelper.GetSiteIdByRole(request.RoleId);

            var resultLink = result.GetCasinoResult(
                request.CustId.ToString(),
                request.GameId,
                request.Language,
                siteId);

            return new SuccessResult(resultLink);
        }
    }
}
