namespace Fanex.Result.ThirdParty.Services.Impl
{
    using System;
    using Configurations;
    using Logging;
    using Models;
    using Models.Requests;

    public class RNGCasinoResultServiceImpl : ThirdPartyResultService
    {
        private readonly ThirdPartyResultApiConfig config;
        private readonly ILogger logger;

        public RNGCasinoResultServiceImpl(ThirdPartyResultApiConfig config, ILogger logger)
        {
            this.config = config;
            this.logger = logger;
        }

        public ThirdPartyResult GetResultDetails(ThirdPartyResultQuery request)
        {
            throw new NotImplementedException();
        }
    }
}
