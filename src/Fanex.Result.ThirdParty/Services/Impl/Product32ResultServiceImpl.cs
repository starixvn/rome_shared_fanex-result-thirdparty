namespace Fanex.Result.ThirdParty.Services.Impl
{
    using System;
    using Configurations;
    using Core.PP;
    using Helpers;
    using Logging;
    using Models;
    using Models.Requests;

    public class Product32ResultServiceImpl : ResultServiceImpl
    {
        private readonly IPPResult ppResult;

        public Product32ResultServiceImpl(
            ProductResultApiConfig config,
            ILogger logger,
            JsonConverter jsonConverter)
            : this(config, logger, jsonConverter, null)
        {
        }

        public Product32ResultServiceImpl(
            ProductResultApiConfig config,
            ILogger logger,
            JsonConverter jsonConverter,
            IPPResult ppResult)
            : base(config, logger, jsonConverter)
        {
            this.ppResult = ppResult ?? new PPResult();
        }

        public override Result GetResultLink(ThirdPartyResultBaseQuery request)
        {
            try
            {
                var siteId = SiteHelper.GetSiteIdByRole(request.RoleId);

                var resultLink = ppResult.GetPPResult(
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
