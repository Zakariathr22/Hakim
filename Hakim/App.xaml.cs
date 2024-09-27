using Hakim.Model;
using Hakim.Service;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using Microsoft.Windows.ApplicationModel.Resources;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Hakim
{
    public partial class App : Application
    {
        public static MainWindow mainWindow { get; set; }
        public static User user = new User();

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
                // Get the path to the AppData\Local directory
                string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string appFolder = System.IO.Path.Combine(localAppDataPath, "Hakim");
                string settingsFile = System.IO.Path.Combine(appFolder, "appsettings.json");

                // Log the full folder path
                System.Diagnostics.Debug.WriteLine($"App folder path: {appFolder}");

                // Check if directory exists, create if not
                if (!Directory.Exists(appFolder))
                {
                    Directory.CreateDirectory(appFolder);
                    System.Diagnostics.Debug.WriteLine($"Created app folder: {appFolder}");
                }

                // Define the JSON structure
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

                // Convert the settings to JSON format
                string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(appSettings, Newtonsoft.Json.Formatting.Indented);

                // Check if the settings file exists, create if not
                if (!System.IO.File.Exists(settingsFile))
                {
                    System.IO.File.WriteAllText(settingsFile, jsonContent);
                    System.Diagnostics.Debug.WriteLine($"Created appsettings.json file: {settingsFile}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("appsettings.json already exists.");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error generating appsettings.json: {ex.Message}");
            }
        }


        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            mainWindow = new MainWindow();
            mainWindow.Activate();
        }

    }
}
