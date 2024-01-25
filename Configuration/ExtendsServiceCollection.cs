using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LightestNight.Configuration;

public static class ExtendsServiceCollection
{
    public static OptionsBuilder<TConfig> BindOptions<TConfig>(this IServiceCollection services) where TConfig : class
        => services.AddOptions<TConfig>()
            .Configure<IConfiguration>((bind, configuration) => configuration.SetConfig(bind));

    public static OptionsBuilder<TConfig> BindOptions<TConfig>(this IServiceCollection services,
        Action<TConfig, IConfiguration> configureOptions) where TConfig : class
        => services.AddOptions<TConfig>().Configure(configureOptions);
}