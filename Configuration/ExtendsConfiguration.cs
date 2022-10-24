using System;
using Microsoft.Extensions.Configuration;

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
}