# Lightest Night
## Configuration

Facilitates binding of Configuration Files into POCO objects. This then allows the config to be used within other classes via dependency injection etc

### Build Status
![](https://github.com/lightest-night/system.configuration/workflows/CI/badge.svg)
![](https://github.com/lightest-night/system.configuration/workflows/Release/badge.svg)
#### How To Use
##### Registration
* Asp.Net Standard/Core Dependency Injection
  * Use the provided `services.AddConfiguration()` method
  
* Other Containers
  * Register an instance of `ConfigurationBuilder` as the `IConfigurationBuilder` type as a Singleton
  * Register an instance of `ConfigurationManager` as a Singleton aginst the `IConfiguration` interface
  
##### Usage
###### `ConfigurationManager` extends `IConfiguration` with the following useful methods
* `TConfig Bind(string sectionName = default)`
  * Binds the config settings found in the config section to the given `TConfig` type
  * `sectionName` allows the user to override the section name in the config file
  
* `string GetConnectionString(string name)`
  * Gets the Connection String with the given name
  
* `TSetting GetSetting(string name, TSetting defaultValue = default)`
  * Gets the setting from the config file with the given name as an instance of `TSetting`
  * If not found in the config file, the value of `defaultValue` will be returned
