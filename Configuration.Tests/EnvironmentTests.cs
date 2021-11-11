using System;
using Microsoft.Extensions.Configuration;
using Shouldly;
using Xunit;

namespace LightestNight.Configuration.Tests
{
    public class EnvironmentTests
    {
        [Theory]
        [InlineData("ASPNETCORE_ENVIRONMENT", "Development")]
        [InlineData("DOTNET_ENVIRONMENT", "Staging")]
        public void Should_Add_Environment_Variable_Args_To_Configuration(string key, string value)
        {
            // Arrange
            Environment.SetEnvironmentVariable(key, value);
            
            // Act
            var configurationManager = new ConfigurationManager(new ConfigurationBuilder());
            
            // Assert
            var result = configurationManager.GetSetting<string>(key);
            result.ShouldBe(value);
        }

        [Theory]
        [InlineData("ASPNETCORE_ENVIRONMENT", "")]
        [InlineData("ASPNETCORE_ENVIRONMENT", "Development")]
        public void Should_Read_Setting_From_Correct_Settings_File(string key, string value)
        {
            // Arrange
            const string settingKey = "Test";
            Environment.SetEnvironmentVariable(key, value);
            
            // Act
            var configurationManager = new ConfigurationManager(new ConfigurationBuilder());
            
            // Assert
            var result = configurationManager.GetSetting<string>(settingKey, "");
            result.ShouldBe(value);
        }
    }
}