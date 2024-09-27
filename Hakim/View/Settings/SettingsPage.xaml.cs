using Hakim.Converters;
using Hakim.Service;
using Hakim.View.Controls;
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
    /// An empty page that can be used on its own or navigated to within PatientDetailsDisplay Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private SettingsViewModel viewModel;
        public SettingsPage()
        {
            this.InitializeComponent();
            this.InitializeLocalization();

            viewModel = new SettingsViewModel();
            DataContext = viewModel;

            userSettingsCard.Header = viewModel.User.profitionalName;
            themeComboBox.SelectedIndex = viewModel.AppTheme;
            backDropComboBox.SelectedIndex = viewModel.AppBackDrop;
            landingPageComboBox.SelectedIndex = viewModel.LandingPage;

            Binding binding = new Binding
            {
                Path = new PropertyPath("Language"),
                Converter = new LanguageTagConverter(),
                ConverterParameter = languageComboBox,
                Mode = BindingMode.TwoWay
            };
            languageComboBox.SetBinding(ComboBox.SelectedIndexProperty, binding);
        }

        private void themeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.ThemeChangedCommand.Execute(null);
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
            viewModel.LandingPageChangedCommand.Execute(null);
        }

        private void ShortCutToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {

        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            ShowEditNameDialog();
        }

        private async void ShowEditNameDialog()
        {
            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of PatientDetailsDisplay ContentDialog running in PatientDetailsDisplay Desktop app
            dialog.XamlRoot = Content.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = new TitleControl("Modifier titre, nom ou prénom");
            dialog.SecondaryButtonText = "Fermer";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new EditNamePage(dialog, viewModel);
            dialog.RequestedTheme = ThemeSelectorService.GetTheme(App.mainWindow);
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Secondary) 
            {
                viewModel.LastNameChangedCommand.Execute(null);
                viewModel.FirstNameChangedCommand.Execute(null);
            }
        }

        public void InitializeLocalization()
        {
            settingsPageHeader.Text = LanguageService.GetResourceValue("Settings");

            userSubtitle.Text = LanguageService.GetResourceValue("User");

            editUserHyperlinkButton.Content = LanguageService.GetResourceValue("Edit");
            
            themeSettingCard.Header = LanguageService.GetResourceValue("AppTheme");
            themeSettingCard.Description = LanguageService.GetResourceValue("AppThemeDescription");
            
            systemModeComboBoxItem.Content = LanguageService.GetResourceValue("SystemMode");
            lightModeComboBoxItem.Content = LanguageService.GetResourceValue("LightMode");
            darkmModeComboBoxItem.Content = LanguageService.GetResourceValue("DarkMode");

            backDropSettingCard.Header = LanguageService.GetResourceValue("AppBackdrop");
            backDropSettingCard.Description = LanguageService.GetResourceValue("AppBackDropDescription");

            micaComboBoxItem.Content = LanguageService.GetResourceValue("Mica");
            altMicaComboBoxItem.Content = LanguageService.GetResourceValue("AltMica");
            desktopAcrylicComboBoxItem.Content = LanguageService.GetResourceValue("DesktopAcrylic");
            
            generalSettingsSubtitle.Text = LanguageService.GetResourceValue("GeneralSettings");
            
            landingPageSettingCard.Header = LanguageService.GetResourceValue("LandingPage");
            landingPageSettingCard.Description = LanguageService.GetResourceValue("LandingPageDescription");
            
            homeComboBoxItem.Content = LanguageService.GetResourceValue("Home");
            patientsComboBoxItem.Content = LanguageService.GetResourceValue("Patients");
            appointmentsComboBoxItem.Content = LanguageService.GetResourceValue("Appointments");

            languageSettingCard.Header = LanguageService.GetResourceValue("AppLanguage");
            languageSettingCard.Description = LanguageService.GetResourceValue("AppLanguageDescription");

            shortCutSettingCard.Header = LanguageService.GetResourceValue("CreateShortCut");
            shortCutSettingCard.Description = LanguageService.GetResourceValue("CreateShortCutDescription");

            aboutAppSubtitle.Text = LanguageService.GetResourceValue("AboutApp");
            
            aboutAppSettingCard.Description = LanguageService.GetResourceValue("AboutAppDescription");

            aboutIcons.Header = LanguageService.GetResourceValue("Icons");
            aboutIcons.Description = LanguageService.GetResourceValue("IconsDescription");

            pichonHyperlinkButton.Content = LanguageService.GetResourceValue("Link");
        }

        private void languageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.LanguageChangedCommand.Execute(null);
            this.InitializeLocalization();
            App.mainWindow.InitializeLocalization();

            ForceComboBoxRefresh(themeComboBox);
            ForceComboBoxRefresh(backDropComboBox);
            ForceComboBoxRefresh(landingPageComboBox);
        }

        private void ForceComboBoxRefresh(ComboBox comboBox)
        {
            // Force a UI refresh by resetting the SelectedIndex
            int selectedIndex = comboBox.SelectedIndex;
            comboBox.SelectedIndex = -1; // Temporarily set to an invalid index
            comboBox.SelectedIndex = selectedIndex; // Reset to the original index
        }
    }
}
