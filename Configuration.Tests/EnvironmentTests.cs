using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Shouldly;
using Xunit;

namespace LightestNight.Configuration.Tests;

public class EnvironmentTests
{
    public EnvironmentTests()
    {
        foreach (var kvp in new TestData())
        {
            var environmentKey = kvp[0].ToString();
            if (!string.IsNullOrWhiteSpace(environmentKey))
                Environment.SetEnvironmentVariable(environmentKey, null);
        }    
    }
    
    [Theory]
    [ClassData(typeof(TestData))]
    public void Should_Add_Environment_Variable_Args_To_Configuration(string key, string value)
    {
        // Arrange
        Environment.SetEnvironmentVariable(key, value);
            
        // Act
        var configurationManager = new ConfigurationManager(new ConfigurationBuilder());
            
        // Assert
        var result = configurationManager.GetSetting<string>(key, "");
        result.ShouldBe(value);
    }

    [Theory]
    [ClassData(typeof(TestData))]
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

    internal class TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "ASPNETCORE_ENVIRONMENT", "Development" };
            yield return new object[] { "DOTNET_ENVIRONMENT", "" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}