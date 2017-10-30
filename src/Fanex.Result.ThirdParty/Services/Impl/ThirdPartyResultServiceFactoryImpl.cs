namespace Fanex.Result.ThirdParty.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using Configurations;
    using Constants;
    using Logging;

    public class ThirdPartyResultServiceFactoryImpl : ThirdPartyResultServiceFactory
    {
        private readonly ILogger logger;
        private readonly JsonConverter jsonConverter;
        private readonly Dictionary<int, Func<ThirdPartyResultApiConfig, ILogger, JsonConverter, ResultService>> thirdPartyProductServiceMaps =
            new Dictionary<int, Func<ThirdPartyResultApiConfig, ILogger, JsonConverter, ResultService>>
            {
                [ProductIdConstants.RNGCasino] = (config, logger, jsonConverter) => new ResultServiceImpl(config, logger, jsonConverter),
                [ProductIdConstants.MacauGames] = (config, logger, jsonConverter) => new MacauGamesResultServiceImpl(),
                [ProductIdConstants.FishingWorld] = (config, logger, jsonConverter) => new FishingWorldResultServiceImpl()
            };

        public ThirdPartyResultServiceFactoryImpl(ILogger logger, JsonConverter jsonConverter)
        {
            this.logger = logger;
            this.jsonConverter = jsonConverter;
        }

        public ResultService CreateThirdPartyResultService(int productId)
        {
            var config = new ThirdPartyResultApiConfigImpl(productId);

            var service = thirdPartyProductServiceMaps.ContainsKey(productId)
                ? thirdPartyProductServiceMaps[productId](config, logger, jsonConverter)
                : new ResultServiceImpl(config, logger, jsonConverter);

            return service;
        }
    }
}
