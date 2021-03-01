using Microsoft.Extensions.Configuration;

namespace LightestNight.Configuration
{
    public static class ConfigurationManagerFactory
    {
        private static ConfigurationManager? _configurationManager;

        public static ConfigurationManager Build() =>
            _configurationManager ??= new ConfigurationManager(new ConfigurationBuilder());
    }
}