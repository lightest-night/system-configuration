using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace LightestNight.Configuration;

public static class ConfigBuilder
{
    public static void Build(HostBuilderContext hostingContext, IConfigurationBuilder config, string[]? args = null,
        bool clearSources = false)
    {
        if (clearSources)
            config.Sources.Clear();

        var environment = hostingContext.HostingEnvironment.EnvironmentName;

        config.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{environment}.json", true, true)
            .AddEnvironmentVariables();

        if (args is not null)
            config.AddCommandLine(args);
    }
}