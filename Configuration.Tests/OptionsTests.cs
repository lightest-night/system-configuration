using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shouldly;
using Xunit;

namespace LightestNight.Configuration.Tests;

[Collection(nameof(ConfigurationCollectionFixture))]
public class OptionsTests
{
    private readonly IServiceCollection _services = new ServiceCollection();

    public OptionsTests(ConfigurationFixture fixture)
    {
        fixture.BuildConfig();
        
        _services.AddSingleton<IConfiguration>(_ => fixture.ConfigurationBuilder.Build());
    }

    [Fact]
    public void Should_Bind_Config_To_Options()
    {
        // Act
        _services.BindOptions<Options>();
        
        // Assert
        var config = _services.BuildServiceProvider().GetRequiredService<IOptions<Options>>();
        config.Value.ConfigFound.ShouldBeTrue();
    }

    [Fact]
    public void Should_Bind_Config_With_Options()
    {
        // Arrange
        var functionFired = false;
        
        // Act
        _services.BindOptions<Options>((options, configuration) =>
        {
            configuration.SetConfig(options);
            functionFired = true;
        });
        
        // Assert
        var config = _services.BuildServiceProvider().GetRequiredService<IOptions<Options>>();
        config.Value.ConfigFound.ShouldBeTrue();
        functionFired.ShouldBe(true);
    }

    [Fact]
    public void Should_Bind_Dictionary()
    {
        // Act
        _services.BindOptions<Options>();
        
        // Assert
        var config = _services.BuildServiceProvider().GetRequiredService<IOptions<Options>>();
        config.Value.Fields.ShouldNotBeEmpty();
    }
}

internal record Options
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public bool ConfigFound { get; init; }

    public Dictionary<string, string> Fields { get; init; } = [];
}