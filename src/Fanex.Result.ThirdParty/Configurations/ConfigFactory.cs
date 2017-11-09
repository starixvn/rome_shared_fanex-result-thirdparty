namespace Fanex.Result.ThirdParty.Configurations
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class ConfigFactory
    {
        public static ProductResultApiConfig GetProductResultApiConfig(int productId)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            var configType = types?.FirstOrDefault(t => t.IsClass && t.Name.Equals($"Product{productId}ResultApiConfig"));

            var config = configType != null
                ? (ProductResultApiConfig)Activator.CreateInstance(configType, productId)
                : new ProductResultApiConfigImpl(productId);

            return config;
        }
    }
}
