using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace LightestNight.System.Configuration
{
    public class ConfigurationManager
    {
        private readonly IConfigurationRoot _configuration;

        public ConfigurationManager(IConfigurationBuilder configurationBuilder)
        {
            if (!(configurationBuilder is ConfigurationBuilder builder))
                throw new ArgumentNullException(nameof(configurationBuilder));

            builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);
            
            // If any, add the environment files
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (!string.IsNullOrEmpty(environment))
                builder.AddJsonFile($"appsettings.{environment}.json");
            
            // Add any environment variables there may be
            builder.AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        public TConfig Bind<TConfig>(string sectionName = default)
            where TConfig : class, new()
        {
            var jsonSectionName = (sectionName ?? ConfigSectionNameAttribute.ReadFrom(typeof(TConfig))) ?? typeof(TConfig).Name;
            var bind = new TConfig();

            _configuration.GetSection(jsonSectionName).Bind(bind);

            return bind;
        }

        public string GetConnectionString(string name)
            => _configuration.GetConnectionString(name);

        public TSetting GetSetting<TSetting>(string name, TSetting defaultValue = default)
        {
            var result = _configuration[name];

            if (string.IsNullOrWhiteSpace(result))
                return defaultValue;

            return (TSetting) Convert.ChangeType(result, typeof(TSetting));
        }
    }
}