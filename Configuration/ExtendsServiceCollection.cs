using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LightestNight.Configuration
{
    public static class ExtendsServiceCollection
    {
        public static IServiceCollection AddLightestNightConfiguration(this IServiceCollection services,
                IConfigurationBuilder? builder = null, string[]? args = null,
                Func<IConfigurationBuilder, ConfigurationBuilder>? customizer = null)
        {
            services.TryAddSingleton<IConfigurationManager>(serviceProvider =>
                new ConfigurationManager(
                    builder ?? serviceProvider.GetService<IConfigurationBuilder>() ?? new ConfigurationBuilder(), args,
                    customizer));

            return services;
        }
    }
}