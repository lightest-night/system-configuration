using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace LightestNight.System.Configuration
{
    public class ConfigurationManager
    {
        public IConfigurationRoot Configuration { get; }

        public ConfigurationManager(IConfigurationBuilder configurationBuilder)
        {
            if (!(configurationBuilder is ConfigurationBuilder builder))
                throw new ArgumentNullException(nameof(configurationBuilder));

            builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddXmlFile("web.config", true, true)
                .AddXmlFile("app.config", true, true);
            
            // If any, add the environment files
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (!string.IsNullOrEmpty(environment))
                builder.AddJsonFile($"appsettings.{environment}.json", true, true);
            
            // Add any environment variables there may be
            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public TConfig Bind<TConfig>(string? sectionName = null)
            where TConfig : class, new()
        {
            var jsonSectionName = (sectionName ?? ConfigSectionNameAttribute.ReadFrom(typeof(TConfig))) ?? typeof(TConfig).Name;
            var bind = new TConfig();

            Configuration.GetSection(jsonSectionName).Bind(bind);

            return bind;
        }

        public string GetConnectionString(string name)
            => Configuration.GetConnectionString(name);

        public TSetting GetSetting<TSetting>(string name, TSetting defaultValue = default)
        {
            var result = Configuration[name];

            if (string.IsNullOrWhiteSpace(result))
                return defaultValue;

            return (TSetting) Convert.ChangeType(result, typeof(TSetting));
        }
    }
}