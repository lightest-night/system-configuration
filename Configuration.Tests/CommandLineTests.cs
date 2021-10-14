using Microsoft.Extensions.Configuration;
using Shouldly;
using Xunit;

namespace LightestNight.Configuration.Tests
{
    public class CommandLineTests
    {
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
            
            // Act
            var configurationManager = new ConfigurationManager(new ConfigurationBuilder(), commandLine);
            
            // Assert
            var result = configurationManager.GetSetting<string>(key);
            result.ShouldBe(value);
        }
    }
}