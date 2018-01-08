namespace Fanex.Result.ThirdParty.Services.Impl
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Fanex.Logging;
    using Fanex.Result.ThirdParty.Configurations;
    using RestSharp;

    public class ProductResultServiceFactoryImpl : ProductResultServiceFactory
    {
        private readonly ILogger logger;
        private readonly IRestClient restClient;

        public ProductResultServiceFactoryImpl(ILogger logger, IRestClient restClient)
        {
            this.logger = logger;
            this.restClient = restClient;
        }

        public ResultService CreateProductResultService(int productId)
        {
            var config = ConfigFactory.GetProductResultApiConfig(productId);

            var types = Assembly.GetExecutingAssembly().GetTypes();
            var serviceType = types?.FirstOrDefault(t => t.IsClass && t.Name.Equals($"Product{productId}ResultServiceImpl"));

            var service = serviceType != null
                ? (ResultService)Activator.CreateInstance(serviceType, config, logger, restClient)
                : new ResultServiceImpl(config, logger, restClient);

            return service;
        }
    }
}
