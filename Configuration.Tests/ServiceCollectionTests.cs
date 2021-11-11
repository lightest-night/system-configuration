using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace LightestNight.Configuration.Tests
{
    public class ServiceCollectionTests
    {
        [Fact]
        public void Should_Add_Single_Instance_To_ServiceCollection_With_No_Supplied_Builder()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            
            // Act
            serviceCollection.UseLightestNightConfiguration();
            
            // Assert
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfigurationManager>();

            var scopeProvider = serviceProvider.CreateScope();
            var scopedServiceProvider = scopeProvider.ServiceProvider;
            var scopedConfiguration = scopedServiceProvider.GetRequiredService<IConfigurationManager>();
            
            configuration.ShouldBeSameAs(scopedConfiguration);
        }
        
        [Fact]
        public void Should_Add_Single_Instance_To_ServiceCollection_With_Supplied_Builder()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var configurationBuilder = new ConfigurationBuilder();
            
            // Act
            serviceCollection.UseLightestNightConfiguration(configurationBuilder);
            
            // Assert
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfigurationManager>();

            var scopeProvider = serviceProvider.CreateScope();
            var scopedServiceProvider = scopeProvider.ServiceProvider;
            var scopedConfiguration = scopedServiceProvider.GetRequiredService<IConfigurationManager>();
            
            configuration.ShouldBeSameAs(scopedConfiguration);
        }
    }
}