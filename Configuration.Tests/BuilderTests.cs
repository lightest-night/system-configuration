using System.Linq;
using Microsoft.Extensions.Configuration;
using Shouldly;
using Xunit;

namespace LightestNight.Configuration.Tests
{
    public class BuilderTests
    {
        [Fact]
        public void Should_Build_Successfully_With_No_Builder_Supplied()
        {
            // Act
            var result = ConfigurationManagerFactory.Build();
            
            // Assert
            result.Configuration.Providers.Count().ShouldBe(2);
        }

        [Fact]
        public void Should_Build_Successfully_With_Supplied_Builder()
        {
            // Arrange
            var builder = new ConfigurationBuilder();
            builder.AddNewtonsoftJsonFile("newtonsoft.json", true);
            builder.Build().Providers.Count().ShouldBe(1);
            
            // Act
            var result = builder.UseConfiguration();
            
            // Assert
            result.Configuration.Providers.Count().ShouldBe(2);
        }
    }
}