namespace Fanex.Result.ThirdParty.Services.Impl
{
    using System;
    using Configurations;
    using Core.Voidbridge;
    using Helpers;
    using Logging;
    using Models;
    using Models.Requests;
    using RestSharp;

    public class Product22ResultServiceImpl : ResultServiceImpl
    {
        private readonly VoidbridgeApi voidbridgeApi;

        public Product22ResultServiceImpl(
           ProductResultApiConfig config,
           ILogger logger,
           IRestClient restClient)
           : this(config, logger, restClient, null)
        {
        }

        public Product22ResultServiceImpl(
            ProductResultApiConfig config,
            ILogger logger,
            IRestClient restClient,
            VoidbridgeApi voidbridgeApi)
            : base(config, logger, restClient)
        {
            this.voidbridgeApi = voidbridgeApi ?? new VoidbridgeApiImp();
        }

        public override Result GetDetailLink(ThirdPartyResultBaseQuery request)
        {
            try
            {
                var siteId = SiteHelper.GetSiteIdByRole(request.RoleId);

                var resultLink = voidbridgeApi.GetBetHistoryUrl(
                    request.CustId.ToString(),
                    request.GameId,
                    request.Language,
                    siteId);

                return new SuccessResult(resultLink);
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }
    }
}
