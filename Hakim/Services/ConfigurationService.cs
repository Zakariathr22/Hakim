using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Hakim.Services;

public static class ConfigurationService
{
    // Declare a private static field of type ConfigurationBuilder.
    private static ConfigurationBuilder builder;
    // Declare a private static field of type IConfigurationRoot.
    private static IConfigurationRoot configuration;

    // This is a public static method named Configure. It takes a string parameter named settingsFile.
    public static void Configure(string settingsFile)
    {
        // Instantiate a new ConfigurationBuilder and assign it to the builder field.
        builder = new ConfigurationBuilder();
        // Get the absolute path of the app settings.
        string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string appFolder = System.IO.Path.Combine(localAppDataPath, "Hakim");
        // Set the base path of the builder to the app settings path.
        FileConfigurationExtensions.SetBasePath(builder, appFolder);
        // Add a JSON configuration source to the builder.
        JsonConfigurationExtensions.AddJsonFile(builder, $"{settingsFile}.json", false, true);
        // Build the configuration and assign it to the configuration field.
        configuration = builder.Build();
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
        string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string appFolder = System.IO.Path.Combine(localAppDataPath, "Hakim");

        // Parse the appsettings.json file into a JObject.
        // JObject is a class of the Newtonsoft.Json library that represents a JSON object.
        var appSettings = JObject.Parse(File.ReadAllText($"{appFolder}\\appsettings.json"));

        // Update the specified setting in the AppSettings section of the appsettings.json file.
        appSettings["AppSettings"][name] = value;

        // Write the updated JObject back to the appsettings.json file.
        // The JObject is converted back into a string using the JsonConvert.SerializeObject method.
        // Formatting.Indented is used to format the JSON string with indented formatting.
        File.WriteAllText($"{appFolder}\\appsettings.json", JsonConvert.SerializeObject(appSettings, Newtonsoft.Json.Formatting.Indented));
    }
}
