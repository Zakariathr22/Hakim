using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hakim.Models;
using Hakim.Services;
using Microsoft.UI.Xaml;

namespace Hakim.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        [ObservableProperty] private User user;
        [ObservableProperty] private int appTheme;
        [ObservableProperty] private int appBackDrop;
        [ObservableProperty] private int landingPage;
        [ObservableProperty] private string language;
        [ObservableProperty] private int navigationStyle;

        public SettingsViewModel()
        {
            User = App.user;
            User.Rank = ConfigurationService.GetAppSetting("Rank");
            User.LastName = ConfigurationService.GetAppSetting("LastName");
            User.FirstName = ConfigurationService.GetAppSetting("FirstName");

            AppTheme = int.Parse(ConfigurationService.GetAppSetting("AppTheme"));
            AppBackDrop = int.Parse(ConfigurationService.GetAppSetting("AppBackDrop"));
            LandingPage = int.Parse(ConfigurationService.GetAppSetting("LandingPage"));
            Language = ConfigurationService.GetAppSetting("Language");
            NavigationStyle = int.Parse(ConfigurationService.GetAppSetting("NavigationStyle"));
        }

        [RelayCommand]
        void ThemeChanged()
        {
            if (AppTheme == 0)
            {
                ThemeSelectorService.SetTheme(ElementTheme.Default, App.mainWindow);
            }
            else if (AppTheme == 1)
            {
                ThemeSelectorService.SetTheme(ElementTheme.Light, App.mainWindow);
            }
            else
            {
                ThemeSelectorService.SetTheme(ElementTheme.Dark, App.mainWindow);
            }
            ConfigurationService.SetAppSetting("AppTheme", AppTheme);
        }

        [RelayCommand]
        void BackDropChanged()
        {
            ConfigurationService.SetAppSetting("AppBackDrop", AppBackDrop);
        }

        [RelayCommand]
        void LandingPageChanged()
        {
            ConfigurationService.SetAppSetting("LandingPage", LandingPage);
        }

        [RelayCommand]
        void RankChanged()
        {
            ConfigurationService.SetAppSetting("Rank", User.Rank);
        }

        [RelayCommand]
        void LastNameChanged()
        {
            ConfigurationService.SetAppSetting("LastName", User.LastName);
        }

        [RelayCommand]
        void FirstNameChanged()
        {
            ConfigurationService.SetAppSetting("FirstName", User.FirstName);
        }

        [RelayCommand]
        void LanguageChanged()
        {
            ConfigurationService.SetAppSetting("Language", Language);
            LanguageService.SetLanguage(ConfigurationService.GetAppSetting("Language"));
        }

        [RelayCommand]
        void NavigationStyleChanged()
        {
            ConfigurationService.SetAppSetting("NavigationStyle", NavigationStyle);
        }
    }
}
