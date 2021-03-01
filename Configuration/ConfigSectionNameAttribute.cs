using System;
using System.Reflection;

namespace LightestNight.Configuration
{
    /// <summary>
    /// The name of this config section in the config file
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigSectionNameAttribute : Attribute
    {
        private const string Config = "Config";
        
        /// <summary>
        /// The name of the section in the config file
        /// </summary>
        public string Name { get; set; }

        public ConfigSectionNameAttribute(string name)
        {
            Name = name;
        }

        public static string ReadFrom(Type classType)
        {
            var attr = classType.GetCustomAttribute<ConfigSectionNameAttribute>();
            if (attr != null)
                return attr.Name;

            var classTypeName = classType.Name;
            return classTypeName.EndsWith(Config)
                ? classTypeName.Substring(0, classTypeName.Length - Config.Length)
                : classTypeName;
        }
    }
}