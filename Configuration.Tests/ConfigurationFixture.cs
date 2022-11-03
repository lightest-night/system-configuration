// ReSharper disable ClassNeverInstantiated.Global

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Xunit;

namespace LightestNight.Configuration.Tests;

[CollectionDefinition(nameof(ConfigurationCollectionFixture))]
public class ConfigurationCollectionFixture : ICollectionFixture<ConfigurationFixture>
{
    // This class has no code, and is never created. It's purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

public class ConfigurationFixture : IDisposable
{
    public ConfigurationBuilder ConfigurationBuilder { get; } = new();

    private readonly HostBuilderContext _hostingContext = new(new Dictionary<object, object>())
    {
        HostingEnvironment = new HostingEnvironment
        {
            EnvironmentName = "Development"
        }
    };

    public void BuildConfig(string[]? args = null, string? environment = null)
    {
        if (environment is not null)
            _hostingContext.HostingEnvironment.EnvironmentName = environment;
        
        ConfigBuilder.Build(_hostingContext, ConfigurationBuilder, args);
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}