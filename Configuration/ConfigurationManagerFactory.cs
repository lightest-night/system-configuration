using Microsoft.Extensions.Configuration;

namespace LightestNight.Configuration
{
    public static class ConfigurationManagerFactory
    {
        private static ConfigurationManager? _configurationManager;

        public static ConfigurationManager Build(string[]? args = null) =>
            _configurationManager ??= new ConfigurationManager(new ConfigurationBuilder(), args);

        public static ConfigurationManager UseConfiguration(this IConfigurationBuilder? builder, string[]? args = null)
        {
            _configurationManager = new ConfigurationManager(builder ?? new ConfigurationBuilder(), args);
            return _configurationManager;
        }
    }
}