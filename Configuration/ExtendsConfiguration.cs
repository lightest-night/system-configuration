using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LightestNight.Configuration;

public static class ExtendsConfiguration
{
    public static TConfig Bind<TConfig>(this IConfiguration configuration, string? sectionName = null)
        where TConfig : class, new()
    {
        var jsonSectionName = sectionName ?? ConfigSectionNameAttribute.ReadFrom(typeof(TConfig));
        var bind = new TConfig();

        ((IConfigurationRoot)configuration).GetSection(jsonSectionName).Bind(bind);
        return bind;
    }

    public static TSetting? GetSetting<TSetting>(this IConfiguration configuration, string name,
        TSetting? defaultValue = default)
    {
        var result = configuration[name];

        if (string.IsNullOrWhiteSpace(result))
            return defaultValue;

        return (TSetting)Convert.ChangeType(result, typeof(TSetting));
    }

    public static void SetConfig<TConfig>(this IConfiguration configuration, TConfig bind, string? sectionName = null)
    {
        var jsonSectionName = sectionName ?? ConfigSectionNameAttribute.ReadFrom(typeof(TConfig));
        ((IConfigurationRoot)configuration).GetSection(jsonSectionName).Bind(bind);
    }
    
    public static OptionsBuilder<TOptions> ConfigureBoundOptions<TOptions>(this IServiceCollection services,
        TOptions options, string? sectionName = null)
    where TOptions : class
    {
        var jsonSectionName = sectionName ?? ConfigSectionNameAttribute.ReadFrom(typeof(TOptions));

        return services.AddOptions<TOptions>()
            .Configure<IConfiguration>((bind, configuration) => configuration.SetConfig(bind, jsonSectionName))
            .BindConfiguration(jsonSectionName);
    } 
}