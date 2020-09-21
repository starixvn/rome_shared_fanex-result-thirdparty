namespace Fanex.Result.ThirdParty.Services.Impl
{
    using Configurations;
    using Fanex.Result.Core.Joker;
    using Helpers;
    using Logging;
    using Models;
    using Models.Requests;
    using RestSharp;

    public class Product56ResultServiceImpl : ResultServiceImpl
    {
        private readonly IJokerResult result;

        public Product56ResultServiceImpl(
            ProductResultApiConfig config,
            ILogger logger,
            IRestClient restClient)
            : this(config, logger, restClient, null)
        {
        }

        public Product56ResultServiceImpl(
            ProductResultApiConfig config,
            ILogger logger,
            IRestClient restClient,
            IJokerResult result)
            : base(config, logger, restClient)
        {
            this.result = result ?? new JokerResult();
        }

        public override Result GetDetailLink(ThirdPartyResultBaseQuery request)
        {
            var siteId = SiteHelper.GetSiteIdByRole(request.RoleId);

            var resultLink = result.GetJokerResult(
                request.CustId.ToString(),
                request.GameId,
                request.GameCode,
                request.Language,
                siteId);

            return new SuccessResult(resultLink);
        }
    }
}
