using Shouldly;
using Xunit;

namespace LightestNight.Configuration.Tests;

[Collection(nameof(ConfigurationCollectionFixture))]
public class CommandLineTests
{
    private readonly ConfigurationFixture _fixture;

    public CommandLineTests(ConfigurationFixture fixture)
    {
        _fixture = fixture;
    }
    
    [Theory]
    [InlineData("environment", "Development")]
    [InlineData("Environment", "Development")]
    [InlineData("env", "Staging")]
    [InlineData("Env", "Staging")]
    [InlineData("e", "Production")]
    public void Should_Add_Command_Line_Args_To_Configuration(string key, string value)
    {
        // Arrange
        var commandLine = new[]{$"--{key}={value}"};
        _fixture.BuildConfig(commandLine);
        var configuration = _fixture.ConfigurationBuilder.Build();

        // Act
        var result = configuration.GetSetting<string>(key);

        // Assert
        result.ShouldBe(value);
    }
}