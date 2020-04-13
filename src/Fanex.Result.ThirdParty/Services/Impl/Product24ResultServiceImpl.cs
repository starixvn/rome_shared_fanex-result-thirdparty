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
    using RestSharp;

    public class Product24ResultServiceImpl : ResultServiceImpl
    {
        private readonly Base3rdResultApi resultApi;

        public Product24ResultServiceImpl(
           ProductResultApiConfig config,
           ILogger logger,
           IRestClient restClient)
            : this(config, logger, restClient, null)
        {
        }

        public Product24ResultServiceImpl(
            ProductResultApiConfig config,
            ILogger logger,
            IRestClient restClient,
            Base3rdResultApi resultApi)
            : base(config, logger, restClient)
        {
            this.resultApi = resultApi ?? new GlobalGamingResultApi();
        }

        public override Result GetDetailLink(ThirdPartyResultBaseQuery request)
        {
            try
            {
                var globalGamingDTO = new GlobalGamingDTO
                {
                    CustId = request.CustId.ToString(),
                    GameId = request.GameId,
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
