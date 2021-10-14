using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace LightestNight.Configuration
{
    public class ConfigurationManager
    {
        public IConfigurationRoot Configuration { get; }

        public ConfigurationManager(IConfigurationBuilder configurationBuilder, string[]? args = null)
        {
            if (configurationBuilder is not ConfigurationBuilder builder)
                throw new ArgumentNullException(nameof(configurationBuilder));

            builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, false);

            if (args is not null)
                builder.AddCommandLine(args);

            // If any, add the environment files
            var commandLineParser = builder.Build();
            var environment = commandLineParser.GetValue<string?>("environment", null)
                              ?? commandLineParser.GetValue<string?>("Environment", null)
                              ?? commandLineParser.GetValue<string?>("env", null)
                              ?? commandLineParser.GetValue<string?>("Env", null)
                              ?? commandLineParser.GetValue<string?>("e", null)
                              ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                              ?? Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            
            if (!string.IsNullOrEmpty(environment))
                builder.AddJsonFile($"appsettings.{environment}.json", true, false);
            
            // Add any environment variables there may be
            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public TConfig Bind<TConfig>(string? sectionName = null)
            where TConfig : class, new()
        {
            var jsonSectionName = (sectionName ?? ConfigSectionNameAttribute.ReadFrom(typeof(TConfig)));
            var bind = new TConfig();

            Configuration.GetSection(jsonSectionName).Bind(bind);

            return bind;
        }

        public string GetConnectionString(string name)
            => Configuration.GetConnectionString(name);

        public TSetting? GetSetting<TSetting>(string name, TSetting? defaultValue = default)
        {
            var result = Configuration[name];

            if (string.IsNullOrWhiteSpace(result))
                return defaultValue;

            return (TSetting) Convert.ChangeType(result, typeof(TSetting));
        }
    }
}