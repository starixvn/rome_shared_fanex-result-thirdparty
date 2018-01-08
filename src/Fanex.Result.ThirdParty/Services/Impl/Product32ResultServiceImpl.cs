namespace Fanex.Result.ThirdParty.Services.Impl
{
    using Configurations;
    using Core.PP;
    using Helpers;
    using Logging;
    using Models;
    using Models.Requests;
    using RestSharp;

    public class Product32ResultServiceImpl : ResultServiceImpl
    {
        private readonly IPPResult ppResult;

        public Product32ResultServiceImpl(
            ProductResultApiConfig config,
            ILogger logger,
            IRestClient restClient)
            : this(config, logger, restClient, null)
        {
        }

        public Product32ResultServiceImpl(
            ProductResultApiConfig config,
            ILogger logger,
            IRestClient restClient,
            IPPResult ppResult)
            : base(config, logger, restClient)
        {
            this.ppResult = ppResult ?? new PPResult();
        }

        public override Result GetDetailLink(ThirdPartyResultBaseQuery request)
        {
            var siteId = SiteHelper.GetSiteIdByRole(request.RoleId);

            var resultLink = ppResult.GetPPResult(
                request.CustId.ToString(),
                request.GameId,
                request.Language,
                siteId);

            return new SuccessResult(resultLink);
        }
    }
}
