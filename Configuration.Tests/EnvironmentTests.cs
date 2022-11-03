using System;
using System.Collections;
using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace LightestNight.Configuration.Tests;

[Collection(nameof(ConfigurationCollectionFixture))]
public class EnvironmentTests
{
    private readonly ConfigurationFixture _fixture;
    
    public EnvironmentTests(ConfigurationFixture fixture)
    {
        _fixture = fixture;
        foreach (var kvp in new TestData())
        {
            var environmentKey = kvp[0].ToString();
            if (!string.IsNullOrWhiteSpace(environmentKey))
                Environment.SetEnvironmentVariable(environmentKey, null);
        }

        _fixture.BuildConfig();
    }
    
    [Theory]
    [ClassData(typeof(TestData))]
    public void Should_Add_Environment_Variable_Args_To_Configuration(string key, string value)
    {
        // Arrange
        Environment.SetEnvironmentVariable(key, value);
        var configurationManager = _fixture.ConfigurationBuilder.Build();
        
        // Act
        var result = configurationManager.GetSetting<string>(key, "");
            
        // Assert
        result.ShouldBe(value);
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void Should_Read_Setting_From_Correct_Settings_File(string key, string value)
    {
        // Arrange
        const string settingKey = "Test";
        Environment.SetEnvironmentVariable(key, value);
        _fixture.BuildConfig(environment: value);
        var configurationManager = _fixture.ConfigurationBuilder.Build();
        
        // Act
        var result = configurationManager.GetSetting<string>(settingKey, "");
            
        // Assert
        result.ShouldBe(value);
    }
    //
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