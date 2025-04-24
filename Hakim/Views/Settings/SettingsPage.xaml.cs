using Hakim.Converters;
using Hakim.Services;
using Hakim.Controls;
using Hakim.ViewModels;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;

namespace Hakim.Views.Settings
{
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

            userSettingsCard.HeaderIcon = new FontIcon { Glyph = "\uE77B" };
            themeSettingCard.HeaderIcon = new FontIcon { Glyph = "\uE790" };
            backDropSettingCard.HeaderIcon = new FontIcon { Glyph = "\uE81E" };
            navigationStyleSettingCard.HeaderIcon = new FontIcon { Glyph = "\uE90C" };
            landingPageSettingCard.HeaderIcon = new FontIcon { Glyph = "\uE89A" };
            languageSettingCard.HeaderIcon = new FontIcon { Glyph = "\uF2B7" };
            shortCutSettingCard.HeaderIcon = new FontIcon { Glyph = "\uE8A7" };
            aboutAppSettingCard.HeaderIcon = new FontIcon { Glyph = "\uE946" };
            aboutIcons.HeaderIcon = new FontIcon { Glyph = "\uED58" };

            Loaded += SettingsPage_Loaded;
        }

        private void SettingsPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.isNavigatingToUser)
            {
                App.isNavigatingToUser = false;
                ShowEditNameDialog();
            }
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

            navigationStyleSettingCard.Header = LanguageService.GetResourceValue("NavigationStyle");
            navigationStyleSettingCard.Description = LanguageService.GetResourceValue("NavigationStyleDescription");

            leftComboBoxItem.Content = LanguageService.GetResourceValue("Left");
            topComboBoxItem.Content = LanguageService.GetResourceValue("Top");

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
            ForceComboBoxRefresh(navigationStyleComboBox);
            ForceComboBoxRefresh(landingPageComboBox);
        }

        private void ForceComboBoxRefresh(ComboBox comboBox)
        {
            int selectedIndex = comboBox.SelectedIndex;
            comboBox.SelectedIndex = -1;
            comboBox.SelectedIndex = selectedIndex;
        }

        private void navigationStyleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (navigationStyleComboBox.SelectedIndex == 0)
            {
                if (App.mainWindow.navigationView.PaneDisplayMode != NavigationViewPaneDisplayMode.Left)
                {
                    App.mainWindow.navigationView.PaneDisplayMode = NavigationViewPaneDisplayMode.Left;
                    App.mainWindow.titleBar.IsPaneToggleButtonVisible = true;
                }
                viewModel.NavigationStyleChangedCommand.Execute(null);
            }
            else if(navigationStyleComboBox.SelectedIndex == 1)
            {
                if (App.mainWindow.navigationView.PaneDisplayMode != NavigationViewPaneDisplayMode.Top)
                {
                    App.mainWindow.navigationView.PaneDisplayMode = NavigationViewPaneDisplayMode.Top;
                    App.mainWindow.titleBar.IsPaneToggleButtonVisible = false;
                }
                viewModel.NavigationStyleChangedCommand.Execute(null);
            }
        }
    }
}
