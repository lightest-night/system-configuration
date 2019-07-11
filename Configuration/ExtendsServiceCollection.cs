using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LightestNight.System.Configuration
{
    public static class ExtendsServiceCollection
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services)
        {
            services.TryAddSingleton(typeof(IConfigurationBuilder), typeof(ConfigurationBuilder));
            services.TryAddSingleton<ConfigurationManager>();

            return services;
        }
    }
}