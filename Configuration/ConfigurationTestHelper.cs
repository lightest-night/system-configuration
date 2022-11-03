using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace LightestNight.Configuration;

public static class ConfigurationTestHelper
{
    private static readonly ConfigurationBuilder ConfigurationBuilder = new();

    private static readonly HostBuilderContext HostingContext = new(new Dictionary<object, object>())
    {
        HostingEnvironment = new HostingEnvironment
        {
            EnvironmentName = "Development"
        }
    };
    
    public static IConfigurationRoot CreateConfiguration(string environment = "Development", string[]? args = null)
    {
        HostingContext.HostingEnvironment.EnvironmentName = environment;
        ConfigBuilder.Build(HostingContext, ConfigurationBuilder, args, true);

        return ConfigurationBuilder.Build();
    }
}