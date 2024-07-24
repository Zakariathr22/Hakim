using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hakim.Model;
using Hakim.Service;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakim.ViewModel
{
    public partial class SettingsViewModel : ObservableObject
    {
        [ObservableProperty] private User user;
        [ObservableProperty] private int appTheme;
        [ObservableProperty] private int appBackDrop;
        [ObservableProperty] private int landingPage;

        public SettingsViewModel()
        {
            user = new User();
            user.Rank = ConfigurationService.GetAppSetting("Rank");
            user.LastName = ConfigurationService.GetAppSetting("LastName");
            user.FirstName = ConfigurationService.GetAppSetting("FirstName");

            appTheme = int.Parse(ConfigurationService.GetAppSetting("AppTheme"));
            appBackDrop = int.Parse(ConfigurationService.GetAppSetting("AppBackDrop"));
            landingPage = int.Parse(ConfigurationService.GetAppSetting("LandingPage"));
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
    }
}
