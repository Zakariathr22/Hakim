using Hakim.Models;
using Hakim.Services;
using Microsoft.UI.Xaml;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;

namespace Hakim;

public partial class App : Application
{
    public static MainWindow mainWindow { get; set; }
    public static User user = new User();
    public static bool isNavigatingToUser = false; 

    public App()
    {
        this.InitializeComponent();
        GenerateAppSettingsJson();
        try
        {
            LanguageService.SetLanguage(ConfigurationService.GetAppSetting("Language"));
        }
        catch
        {
            LanguageService.SetLanguage("fr-DZ");
        }
        InitializeDatabase();
    }
    private void InitializeDatabase()
    {
        try
        {
            // Get the path to the AppData\Local directory
            //C:\Users\TAHRI.ZAKARIA\AppData\Local\Packages\666f5b0a-7c74-496a-803c-fe11ab9b23ce_8a0x43e1nc0dt\LocalCache\Local\Hakim
            string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string appFolder = System.IO.Path.Combine(localAppDataPath, "Hakim");
            string databaseFile = System.IO.Path.Combine(appFolder, "Database.sqlite");

            // Log the full folder path
            System.Diagnostics.Debug.WriteLine($"App folder path: {appFolder}");

            // Check if directory exists, create if not
            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
                System.Diagnostics.Debug.WriteLine($"Created app folder: {appFolder}");
            }

            // Check if the database file exists
            if (!System.IO.File.Exists(databaseFile))
            {
                SQLiteConnection.CreateFile(databaseFile);
                System.Diagnostics.Debug.WriteLine($"Created database file: {databaseFile}");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error initializing the database: {ex.Message}");
        }

        DataAccessService.SetupDatabaseSchema();
    }

    private void GenerateAppSettingsJson()
    {
        try
        {
            string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string appFolder = System.IO.Path.Combine(localAppDataPath, "Hakim");
            string settingsFile = System.IO.Path.Combine(appFolder, "appsettings.json");

            Debug.WriteLine($"App folder path: {appFolder}");

            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
                Debug.WriteLine($"Created app folder: {appFolder}");
            }

            // Define default JSON structure
            var appSettings = new
            {
                AppSettings = new
                {
                    Language = "fr-DZ",
                    Rank = "Dr.",
                    LastName = "",
                    FirstName = "",
                    AppTheme = 0,
                    AppBackDrop = 0,
                    LandingPage = 0,
                    PatientsOrder = 0
                }
            };

            string jsonContent = JsonConvert.SerializeObject(appSettings, Formatting.Indented);

            if (!System.IO.File.Exists(settingsFile))
            {
                System.IO.File.WriteAllText(settingsFile, jsonContent);
                Debug.WriteLine($"Created appsettings.json file: {settingsFile}");
            }
            else
            {
                Debug.WriteLine("appsettings.json already exists.");
            }

            // FINAL CHECK: Ensure NavigationStyle exists
            string existingJson = System.IO.File.ReadAllText(settingsFile);
            JObject jsonObj = JObject.Parse(existingJson);
            JObject appSettingsObj = (JObject)jsonObj["AppSettings"];

            if (appSettingsObj["NavigationStyle"] == null)
            {
                appSettingsObj["NavigationStyle"] = 0;
                System.IO.File.WriteAllText(settingsFile, jsonObj.ToString(Formatting.Indented));
                Debug.WriteLine("Added missing NavigationStyle key with value 0.");
            }
            else
            {
                Debug.WriteLine("NavigationStyle already exists.");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error generating appsettings.json: {ex.Message}");
        }
    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        mainWindow = new MainWindow();
        mainWindow.Activate();
    }

}
