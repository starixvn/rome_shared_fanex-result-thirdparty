namespace Fanex.Result.ThirdParty.Services.Impl
{
    using System;
    using Configurations;
    using Core.Base.Models;
    using Core.GlobalGaming;
    using Core.GlobalGaming.DTO;
    using Helpers;
    using Logging;
    using Models;
    using Models.Requests;

    public class Product24ResultServiceImpl : ResultServiceImpl
    {
        private readonly Base3rdResultApi resultApi;

        public Product24ResultServiceImpl(
           ProductResultApiConfig config,
           ILogger logger,
           JsonConverter jsonConverter)
            : this(config, logger, jsonConverter, null)
        {
        }

        public Product24ResultServiceImpl(
            ProductResultApiConfig config,
            ILogger logger,
            JsonConverter jsonConverter,
            Base3rdResultApi resultApi)
            : base(config, logger, jsonConverter)
        {
            this.resultApi = resultApi ?? new GlobalGamingResultApi();
        }

        public override Result GetResultLink(ThirdPartyResultBaseQuery request)
        {
            try
            {
                var siteId = SiteHelper.GetSiteIdByRole(request.RoleId);

                var globalGamingDTO = new GlobalGamingDTO
                {
                    CustId = request.CustId.ToString(),
                    GameId = request.GameId,
                    SiteId = siteId,
                    Language = request.Language
                };

                var resultLink = resultApi.GetResultUrl(globalGamingDTO);

                return new SuccessResult(resultLink);
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }
    }
}
