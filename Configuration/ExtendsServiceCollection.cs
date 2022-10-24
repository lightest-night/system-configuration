using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LightestNight.Configuration;

public static class ExtendsServiceCollection
{
    public static IServiceCollection AddLightestNightConfiguration(this IServiceCollection services,
        IConfigurationBuilder? builder = null, string[]? args = null,
        Func<IConfigurationBuilder, ConfigurationBuilder>? customizer = null)
        => services.AddSingleton<IConfiguration>(serviceProvider =>
            new ConfigurationManager(
                builder ?? serviceProvider.GetService<IConfigurationBuilder>() ?? new ConfigurationBuilder(), args,
                customizer));
}