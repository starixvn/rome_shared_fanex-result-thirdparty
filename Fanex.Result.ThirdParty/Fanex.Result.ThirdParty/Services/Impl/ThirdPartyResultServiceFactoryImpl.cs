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
        private readonly Dictionary<int, Func<ThirdPartyResultApiConfig, ILogger, ThirdPartyResultService>> thirdPartyProductServiceMaps =
            new Dictionary<int, Func<ThirdPartyResultApiConfig, ILogger, ThirdPartyResultService>>
            {
                [ProductIdConstants.RNGCasino] = (config, logger) => new ThirdPartyResultServiceImpl(config, logger),
                [ProductIdConstants.Voidbridge] = (config, logger) => new MacauGamesResultServiceImpl(),
                [ProductIdConstants.FishingWorld] = (config, logger) => new FishingWorldResultServiceImpl()
            };

        public ThirdPartyResultServiceFactoryImpl(ILogger logger)
        {
            this.logger = logger;
        }

        public ThirdPartyResultService CreateThirdPartyResultService(int productId)
        {
            var config = new ThirdPartyResultApiConfigImpl(productId);

            var service = thirdPartyProductServiceMaps.ContainsKey(productId)
                ? thirdPartyProductServiceMaps[productId](config, logger)
                : new ThirdPartyResultServiceImpl(config, logger);

            return service;
        }
    }
}
