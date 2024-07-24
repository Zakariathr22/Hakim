using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Hakim.Service
{
    public static class ConfigurationService
    {
        // Declare a private static field of type ConfigurationBuilder.
        private static ConfigurationBuilder builder;
        // Declare a private static field of type IConfigurationRoot.
        private static IConfigurationRoot configuration;

        // This is a method that takes a file path and a number of levels to go up in the directory structure.
        private static string GetParentDirectoryPath(string path, int levels)
        {
            // Create a DirectoryInfo object from the provided path. This object provides methods for creating, moving, and enumerating through directories and subdirectories.
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            // This loop will run for the number of levels you want to go up in the directory structure.
            for (int i = 0; i < levels; i++)
            {
                // Check if the parent directory of the current directory exists.
                if (directoryInfo.Parent is not null)
                {
                    // If the parent directory exists, set it as the current directory.
                    directoryInfo = directoryInfo.Parent;
                }
                else
                {
                    // If the parent directory does not exist (i.e., we've reached the root), break the loop.
                    break;
                }
            }

            // Return the full name (i.e., the full path) of the current directory.
            return directoryInfo.FullName;
        }

        // This is a public static method named Configure. It takes a string parameter named settingsFile.
        public static void Configure(string settingsFile)
        {
            // Instantiate a new ConfigurationBuilder and assign it to the builder field.
            builder = new ConfigurationBuilder();
            // Get the absolute path of the app settings.
            string appSettingsPath = GetParentDirectoryPath(AppDomain.CurrentDomain.BaseDirectory, 0);
            // Set the base path of the builder to the app settings path.
            FileConfigurationExtensions.SetBasePath(builder, appSettingsPath);
            // Add a JSON configuration source to the builder.
            JsonConfigurationExtensions.AddJsonFile(builder, $"{settingsFile}.json", false, true);
            // Build the configuration and assign it to the configuration field.
            configuration = builder.Build();
        }

        // This is a public static method named GetConnectionString. It takes a string parameter named name.
        public static string GetConnectionString(string name)
        {
            // Call the Configure method with "appsettings" as the argument.
            Configure("appsettings");
            // Return the connection string from the configuration with the specified name.
            return configuration.GetConnectionString(name);
        }

        // This is a public static method named GetAppSetting. It takes a string parameter named name.
        public static string GetAppSetting(string name)
        {
            // Call the Configure method with "appsettings" as the argument.
            Configure("appsettings");
            // Return the app setting from the configuration with the specified name.
            return configuration[$"AppSettings:{name}"];
        }

        // This method updates a specific setting in the appsettings.json file.
        public static void SetAppSetting(string name, JToken value)
        {
            // Get the absolute path of the app settings.
            // The GetParentDirectoryPath method is used to navigate up the directory structure.
            string appSettingsPath = GetParentDirectoryPath(AppDomain.CurrentDomain.BaseDirectory, 0);

            // Parse the appsettings.json file into a JObject.
            // JObject is a class of the Newtonsoft.Json library that represents a JSON object.
            var appSettings = JObject.Parse(File.ReadAllText($"{appSettingsPath}\\appsettings.json"));

            // Update the specified setting in the AppSettings section of the appsettings.json file.
            appSettings["AppSettings"][name] = value;

            // Write the updated JObject back to the appsettings.json file.
            // The JObject is converted back into a string using the JsonConvert.SerializeObject method.
            // Formatting.Indented is used to format the JSON string with indented formatting.
            File.WriteAllText($"{appSettingsPath}\\appsettings.json", JsonConvert.SerializeObject(appSettings, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
