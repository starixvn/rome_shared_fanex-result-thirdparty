namespace Fanex.Result.ThirdParty.Services.Impl
{
    using Configurations;
    using Fanex.Result.Core.PGSoft;
    using Helpers;
    using Logging;
    using Models;
    using Models.Requests;
    using RestSharp;

    public class Product55ResultServiceImpl : ResultServiceImpl
    {
        private readonly IPGSoftResult result;

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
            IPGSoftResult result)
            : base(config, logger, restClient)
        {
            this.result = result ?? new PGSoftResult();
        }

        public override Result GetDetailLink(ThirdPartyResultBaseQuery request)
        {
            var siteId = SiteHelper.GetSiteIdByRole(request.RoleId);
            var param = new PGSoftParam
            {
                CustId = request.CustId.ToString(),
                GamePlayId=request.GameId,
                GameId3rd = request.GameCode,
                Language = request.Language,
                SiteId= siteId
            };

            var resultLink = result.GetResult(param);

            return new SuccessResult(resultLink);
        }
    }
}
