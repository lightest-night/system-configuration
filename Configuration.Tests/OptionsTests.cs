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
}

internal record Options
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public bool ConfigFound { get; set; }
}