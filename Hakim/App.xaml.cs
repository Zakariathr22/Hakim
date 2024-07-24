using Hakim.Model;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public static MainWindow mainWindow { get; set; }
        public static User user = new User();
        public App()
        {
            this.InitializeComponent();

            // Get the desired culture (e.g., Arabic)
            CultureInfo newCulture = CultureInfo.GetCultureInfo("fr-FR");

            // Set the CurrentCulture of the current thread
            Thread.CurrentThread.CurrentCulture = newCulture;

            // Set the CurrentUICulture of the current thread (optional)
            // This affects UI elements like date pickers and number formats
            Thread.CurrentThread.CurrentUICulture = newCulture;

            ApplicationLanguages.PrimaryLanguageOverride = "fr-FR";
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            mainWindow = new MainWindow();
            mainWindow.Activate();
        }

        //private Window m_window;
    }
}
