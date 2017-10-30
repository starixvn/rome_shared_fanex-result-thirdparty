namespace Fanex.Result.ThirdParty.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using Configurations;
    using Logging;
    using Sunplus.Agent.Model.Definitions;

    public class ThirdPartyResultServiceFactoryImpl : ThirdPartyResultServiceFactory
    {
        private readonly ILogger logger;
        private readonly JsonConverter jsonConverter;
        private readonly Dictionary<int, Func<ThirdPartyResultApiConfig, ILogger, JsonConverter, ThirdPartyResultService>> thirdPartyProductServiceMaps =
            new Dictionary<int, Func<ThirdPartyResultApiConfig, ILogger, JsonConverter, ThirdPartyResultService>>
            {
                [ProductIdConstants.RNGCasino] = (config, logger, jsonConverter) => new ThirdPartyResultServiceImpl(config, logger, jsonConverter),
                [ProductIdConstants.Voidbridge] = (config, logger, jsonConverter) => new MacauGamesResultServiceImpl(),
                [ProductIdConstants.FishingWorld] = (config, logger, jsonConverter) => new FishingWorldResultServiceImpl()
            };

        public ThirdPartyResultServiceFactoryImpl(ILogger logger, JsonConverter jsonConverter)
        {
            this.logger = logger;
            this.jsonConverter = jsonConverter;
        }

        public ThirdPartyResultService CreateThirdPartyResultService(int productId)
        {
            var config = new ThirdPartyResultApiConfigImpl(productId);

            var service = thirdPartyProductServiceMaps.ContainsKey(productId)
                ? thirdPartyProductServiceMaps[productId](config, logger, jsonConverter)
                : new ThirdPartyResultServiceImpl(config, logger, jsonConverter);

            return service;
        }
    }
}
