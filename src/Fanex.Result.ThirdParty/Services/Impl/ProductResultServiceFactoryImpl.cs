namespace Fanex.Result.ThirdParty.Services.Impl
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Fanex.Logging;
    using Fanex.Result.ThirdParty.Configurations;

    public class ProductResultServiceFactoryImpl : ProductResultServiceFactory
    {
        private readonly ILogger logger;
        private readonly JsonConverter jsonConverter;

        public ProductResultServiceFactoryImpl(ILogger logger, JsonConverter jsonConverter)
        {
            this.logger = logger;
            this.jsonConverter = jsonConverter;
        }

        public ResultService CreateProductResultService(int productId)
        {
            var config = ConfigFactory.GetProductResultApiConfig(productId);

            var types = Assembly.GetExecutingAssembly().GetTypes();
            var serviceType = types?.FirstOrDefault(t => t.IsClass && t.Name.Equals($"Product{productId}ResultServiceImpl"));

            var service = serviceType != null
                ? (ResultService)Activator.CreateInstance(serviceType, config, logger, jsonConverter)
                : new ResultServiceImpl(config, logger, jsonConverter);

            return service;
        }
    }
}
