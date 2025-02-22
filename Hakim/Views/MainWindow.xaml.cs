using Microsoft.UI.Windowing;
using Microsoft.UI;
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
using WinRT.Interop;
using Windows.UI;
using Hakim.ViewModel;
using Hakim.Service;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Hakim
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within patientDetailsDisplay Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private AppWindow appWindow;
        private OverlappedPresenter overlappedPresenter;
        private AppWindowTitleBar titleBar;
        private MainViewModel viewModel = new MainViewModel();
        public MainWindow()
        {
            this.InitializeComponent();
            this.InitializeLocalization();

            mainPanel.DataContext = viewModel;

            appWindow = GetAppWindowForCurrentWindow();
            overlappedPresenter = GetAppWindowOverlappedPresenter(appWindow);
            titleBar = GetAppWindowTitleBar(appWindow);
            
            titleBar.ButtonBackgroundColor = Color.FromArgb(0, 0, 0, 0);
            titleBar.ButtonForegroundColor = Color.FromArgb(0, 128, 128, 128);
            titleBar.ButtonInactiveBackgroundColor = Color.FromArgb(0, 0, 0, 0);

            appWindow.Title = "Hakim";
            appWindow.SetIcon("Assets/Icons/Hakim.ico");
            //overlappedPresenter.Maximize();

            titleBar.ExtendsContentIntoTitleBar = true;

            CenterWindow();

            viewModel.SetAppTheme(this);
            viewModel.SetAppBackDrop(this);
            navigationView.SelectedItem = navigationView.MenuItems.OfType<NavigationViewItem>().ElementAt(viewModel.LandingPage);

        }


        private AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(myWndId);
        }

        private OverlappedPresenter GetAppWindowOverlappedPresenter(AppWindow appWindow)
        {
            return (OverlappedPresenter)appWindow.Presenter;
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
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            Microsoft.UI.WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            Microsoft.UI.Windowing.AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            if (appWindow is not null)
            {
                Microsoft.UI.Windowing.DisplayArea displayArea = Microsoft.UI.Windowing.DisplayArea.GetFromWindowId(windowId, Microsoft.UI.Windowing.DisplayAreaFallback.Nearest);
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
            var selectedItem = (Microsoft.UI.Xaml.Controls.NavigationViewItem)args.SelectedItem;
            if (selectedItem != null)
            {
                string selectedItemTag = ((string)selectedItem.Tag);
                string pageName = $"Hakim.View.{selectedItemTag}.{selectedItemTag}Page";
                Type pageType = Type.GetType(pageName);
                contentFrame.Navigate(pageType);
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
