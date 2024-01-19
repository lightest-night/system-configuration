using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LightestNight.Configuration;

public static class ExtendsServiceCollection
{
    public static void BindOptions<TConfig>(this IServiceCollection services) where TConfig : class
    {
        services.AddOptions<TConfig>()
            .Configure<IConfiguration>((bind, configuration) => configuration.SetConfig(bind));
    }
}