using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LightestNight.Configuration
{
    public static class ExtendsServiceCollection
    {
        public static IServiceCollection UseLightestNightConfiguration(this IServiceCollection services,
            IConfigurationBuilder? builder = null, string[]? args = null)
            => services.AddSingleton<IConfigurationManager>(serviceProvider =>
                new ConfigurationManager(
                    builder ?? serviceProvider.GetService<IConfigurationBuilder>() ?? new ConfigurationBuilder(),
                    args));
    }
}