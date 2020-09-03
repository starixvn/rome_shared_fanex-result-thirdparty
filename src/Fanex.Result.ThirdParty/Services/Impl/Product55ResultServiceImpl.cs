namespace Fanex.Result.ThirdParty.Services.Impl
{
    using Configurations;
    using Fanex.Result.Core.Casino;
    using Helpers;
    using Logging;
    using Models;
    using Models.Requests;
    using RestSharp;

    public class Product55ResultServiceImpl : ResultServiceImpl
    {
        private readonly ICasinoResult result;

        public Product55ResultServiceImpl(
            ProductResultApiConfig config,
            ILogger logger,
            IRestClient restClient)
            : this(config, logger, restClient, null)
        {
        }

        public Product55ResultServiceImpl(
            ProductResultApiConfig config,
            ILogger logger,
            IRestClient restClient,
            ICasinoResult result)
            : base(config, logger, restClient)
        {
            this.result = result ?? new CasinoResult("PGSoftResultConfigs");
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
