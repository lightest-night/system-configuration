using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace LightestNight.Configuration;

public class ConfigurationManager : IConfigurationRoot
{
    private readonly IConfigurationRoot _root;
    
    public ConfigurationManager(IConfigurationBuilder configurationBuilder, string[]? args = null,
        Func<IConfigurationBuilder, ConfigurationBuilder>? customizer = null)
    {
        if (configurationBuilder is not ConfigurationBuilder builder)
            throw new ArgumentNullException(nameof(configurationBuilder));

        _root = ConfigureBuilder(builder, args, customizer);
    }

    public IConfigurationSection GetSection(string key)
        => _root.GetSection(key);

    public IEnumerable<IConfigurationSection> GetChildren()
        => _root.GetChildren();

    public IChangeToken GetReloadToken()
        => _root.GetReloadToken();

    public string this[string key]
    {
        get => _root[key];
        set => _root[key] = value;
    }
    
    public void Reload()
    {
        _root.Reload();
    }

    public IEnumerable<IConfigurationProvider> Providers => _root.Providers;

    private static IConfigurationRoot ConfigureBuilder(IConfigurationBuilder builder, string[]? args = null,
        Func<IConfigurationBuilder, ConfigurationBuilder>? customizer = null)
    {
        builder.Sources.Clear();

        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, false);

        var environment = GetEnvironmentFromCommandLine(args) ?? GetEnvironmentFromEnvironmentVariable();
        if (!string.IsNullOrWhiteSpace(environment))
            builder.AddJsonFile($"appsettings.{environment}.json", true, false);

        builder.AddEnvironmentVariables();

        if (args is not null)
            builder.AddCommandLine(args);

        if (customizer is not null)
            builder = customizer(builder);

        return builder.Build();
    }

    private static string? GetEnvironmentFromCommandLine(string[]? args)
    {
        if (args is null)
            return null;

        var builder = new ConfigurationBuilder();
        builder.AddCommandLine(args);
        var commandLineParser = builder.Build();

        return commandLineParser.GetValue<string?>("environment", null)
               ?? commandLineParser.GetValue<string?>("Environment", null)
               ?? commandLineParser.GetValue<string?>("env", null)
               ?? commandLineParser.GetValue<string?>("Env", null)
               ?? commandLineParser.GetValue<string?>("e", null);
    }

    private static string? GetEnvironmentFromEnvironmentVariable()
        => Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
           ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
}