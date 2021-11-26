using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace LightestNight.Configuration
{
    public class ConfigurationManager : IConfigurationManager
    {
        public IConfigurationRoot Configuration { get; }

        public static ConfigurationManager GetConfigurationManager(string[]? args = null)
            => new(new ConfigurationBuilder(), args);

        public ConfigurationManager(IConfigurationBuilder configurationBuilder, string[]? args = null)
        {
            if (configurationBuilder is not ConfigurationBuilder builder)
                throw new ArgumentNullException(nameof(configurationBuilder));

            builder.Sources.Clear();

            builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, false);

            var environment = GetEnvironmentFromCommandLine(args) ?? GetEnvironmentFromEnvironmentVariable();
            if (!string.IsNullOrEmpty(environment))
                builder.AddJsonFile($"appsettings.{environment}.json", true, false);

            builder.AddEnvironmentVariables();

            if (args is not null)
                builder.AddCommandLine(args);

            Configuration = builder.Build();
        }

        public TConfig Bind<TConfig>(string? sectionName = null) where TConfig : class, new()
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

            return (TSetting)Convert.ChangeType(result, typeof(TSetting));
        }

        private static string? GetEnvironmentFromCommandLine(string[]? args)
        {
            if (args is null)
                return null;
            
            var builder = new ConfigurationBuilder();
            builder.AddCommandLine(args);
            var commandLineParser = builder.Build();

            return commandLineParser.GetValue<string?>("environment", null)
                   ?? commandLineParser.GetValue<string?>("Environment", null)
                   ?? commandLineParser.GetValue<string?>("env", null)
                   ?? commandLineParser.GetValue<string?>("Env", null)
                   ?? commandLineParser.GetValue<string?>("e", null);
        }

        private static string? GetEnvironmentFromEnvironmentVariable()
            => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
               ?? Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
    }
}