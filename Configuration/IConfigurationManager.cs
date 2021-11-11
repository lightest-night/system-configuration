using Microsoft.Extensions.Configuration;

namespace LightestNight.Configuration
{
    public interface IConfigurationManager
    {
        /// <summary>
        /// Retrieves the underlying <see cref="IConfigurationRoot" /> powering the manager
        /// </summary>
        IConfigurationRoot Configuration { get; }

        /// <summary>
        /// Binds a configuration section to an object of the given type
        /// </summary>
        /// <param name="sectionName">The section name of the configuration section to bind</param>
        /// <typeparam name="TConfig">The type of the object to bind to</typeparam>
        /// <returns>A populated POCO populated with the values found in the Configuration Section</returns>
        TConfig Bind<TConfig>(string? sectionName = null) where TConfig : class, new();

        /// <summary>
        /// Retrieves the named Connection String from the Configuration
        /// </summary>
        /// <param name="name">The name of the Connection String</param>
        /// <returns>The Connection String value</returns>
        string GetConnectionString(string name);

        /// <summary>
        /// Retrieves a setting with the given name
        /// </summary>
        /// <param name="name">The name of the setting to retrieve</param>
        /// <param name="defaultValue">If the setting is not found, or has no value, the value to return by default</param>
        /// <typeparam name="TSetting">The type of the return</typeparam>
        /// <returns>The setting value, or if not found, the default value provided</returns>
        TSetting? GetSetting<TSetting>(string name, TSetting? defaultValue = default);
    }
}