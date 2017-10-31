namespace Fanex.Result.ThirdParty.Services.Impl
{
    using System;
    using Configurations;
    using Core.Voidbridge;
    using Helpers;
    using Logging;
    using Models;
    using Models.Requests;

    public class Product22ResultServiceImpl : ResultServiceImpl
    {
        private readonly VoidbridgeApi voidbridgeApi;

        public Product22ResultServiceImpl(
           ProductResultApiConfig config,
           ILogger logger,
           JsonConverter jsonConverter)
           : this(config, logger, jsonConverter, null)
        {
        }

        public Product22ResultServiceImpl(
            ProductResultApiConfig config,
            ILogger logger,
            JsonConverter jsonConverter,
            VoidbridgeApi voidbridgeApi)
            : base(config, logger, jsonConverter)
        {
            this.voidbridgeApi = voidbridgeApi ?? new VoidbridgeApiImp();
        }

        public override Result GetResultLink(ThirdPartyResultBaseQuery request)
        {
            try
            {
                var siteId = SiteHelper.GetSiteIdByRole(request.RoleId);

                var resultLink = voidbridgeApi.GetBetHistoryUrl(
                    request.CustId.ToString(),
                    request.RefNo,
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
