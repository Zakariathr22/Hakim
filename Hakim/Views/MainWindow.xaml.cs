using Microsoft.UI.Windowing;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;
using WinRT.Interop;
using Hakim.ViewModels;
using Hakim.Services;

namespace Hakim
{
    public sealed partial class MainWindow : Window
    {
        private AppWindow appWindow;
        private AppWindowTitleBar titleBar;
        private MainViewModel viewModel = new MainViewModel();
        public MainWindow()
        {
            this.InitializeComponent();
            this.InitializeLocalization();

            mainPanel.DataContext = viewModel;

            appWindow = GetAppWindowForCurrentWindow();
            titleBar = GetAppWindowTitleBar(appWindow);
            
            titleBar.ButtonBackgroundColor = Windows.UI.Color.FromArgb(0, 0, 0, 0);            titleBar.ButtonInactiveBackgroundColor = Windows.UI.Color.FromArgb(0, 0, 0, 0);

            appWindow.Title = "Hakim";
            appWindow.SetIcon("Assets/Icons/Hakim.ico");

            titleBar.ExtendsContentIntoTitleBar = true;
            appWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Tall;

            CenterWindow();

            viewModel.SetAppTheme(this);
            viewModel.SetAppBackDrop(this);
            navigationView.SelectedItem = navigationView.MenuItems.OfType<NavigationViewItem>().ElementAt(viewModel.LandingPage);

            mainPanel.ActualThemeChanged += MainPanel_ActualThemeChanged;
            mainPanel.Loaded += MainPanel_Loaded;
        }

        private void MainPanel_Loaded(object sender, RoutedEventArgs e)
        {
            if (mainPanel.ActualTheme == ElementTheme.Dark)
            {
                titleBar.ButtonForegroundColor = Windows.UI.Color.FromArgb(0, 255, 255, 255);
            }
            else
            {
                titleBar.ButtonForegroundColor = Windows.UI.Color.FromArgb(0, 0, 0, 0);
            }
        }

        private void MainPanel_ActualThemeChanged(FrameworkElement sender, object args)
        {
            if (mainPanel.ActualTheme == ElementTheme.Dark)
            {
                titleBar.ButtonForegroundColor = Windows.UI.Color.FromArgb(0, 255, 255, 255);
            }
            else
            {
                titleBar.ButtonForegroundColor = Windows.UI.Color.FromArgb(0, 0, 0, 0);
            }
        }

        private AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(myWndId);
        }

        private AppWindowTitleBar GetAppWindowTitleBar(AppWindow appWindow)
        {
            if (AppWindowTitleBar.IsCustomizationSupported())
            {
                var titleBar = appWindow.TitleBar;
                return titleBar;
            }
            else
            {
                return null;
            }
        }

        private void CenterWindow()
        {
            var hWnd = WindowNative.GetWindowHandle(this);
            WindowId windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
            AppWindow appWindow = AppWindow.GetFromWindowId(windowId);
            if (appWindow is not null)
            {
               DisplayArea displayArea = DisplayArea.GetFromWindowId(windowId, DisplayAreaFallback.Nearest);
                if (displayArea is not null)
                {
                    var CenteredPosition = appWindow.Position;
                    CenteredPosition.X = ((displayArea.WorkArea.Width - appWindow.Size.Width) / 2);
                    CenteredPosition.Y = ((displayArea.WorkArea.Height - appWindow.Size.Height) / 2);
                    appWindow.Move(CenteredPosition);
                }
            }
        }

        private void navigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var selectedItem = (NavigationViewItem)args.SelectedItem;
            if (selectedItem != null)
            {
                string selectedItemTag = ((string)selectedItem.Tag);
                string pageName = $"Hakim.Views.{selectedItemTag}.{selectedItemTag}Page";
                Type pageType = Type.GetType(pageName);
                if (pageType != null)
                {
                    contentFrame.Navigate(pageType);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Page type not found: {pageName}");
                }
            }
        }

        public void InitializeLocalization()
        {
            searchAutoSuggestBox.PlaceholderText = LanguageService.GetResourceValue("Search");
            ToolTipService.SetToolTip(homeNavigationItem, LanguageService.GetResourceValue("Home"));
            homeNavigationItemText.Text = LanguageService.GetResourceValue("Home");

            ToolTipService.SetToolTip(patientsNavigationItem, LanguageService.GetResourceValue("Patients"));
            patientsNavigationItemText.Text = LanguageService.GetResourceValue("Patients");

            ToolTipService.SetToolTip(scheduleNavigationItem, LanguageService.GetResourceValue("Appointments"));
            scheduleNavigationItemText.Text = LanguageService.GetResourceValue("Appointments");

            ToolTipService.SetToolTip(settingsNavigationItem, LanguageService.GetResourceValue("Settings"));
            settingsNavigationItemText.Text = LanguageService.GetResourceValue("Settings");
        }
    }
}
