using Hakim.ViewModel;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Hakim.View.Settings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private SettingsViewModel viewModel;
        public SettingsPage()
        {
            this.InitializeComponent();
            viewModel = new SettingsViewModel();
            DataContext = viewModel;

            userSettingsCard.Header = viewModel.User.profitionalName;
            themeComboBox.SelectedIndex = viewModel.AppTheme;
            backDropComboBox.SelectedIndex = viewModel.AppBackDrop;
            landingPageComboBox.SelectedIndex = viewModel.LandingPage;
        }

        private void themeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.ThemeChangedCommand.Execute(this);
        }

        private void backDropComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (backDropComboBox.SelectedIndex == 0)
            {
                App.mainWindow.SystemBackdrop = new MicaBackdrop() { Kind = MicaKind.Base };
            }
            else if (backDropComboBox.SelectedIndex == 1)
            {
                App.mainWindow.SystemBackdrop = new MicaBackdrop() { Kind = MicaKind.BaseAlt };
            }
            else
            {
                App.mainWindow.SystemBackdrop = new DesktopAcrylicBackdrop();
            }
            viewModel.BackDropChangedCommand.Execute(this);
        }

        private void landingPageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.LandingPageChangedCommand.Execute(this);
        }

        private void ShortCutToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {

        }
    }
}
