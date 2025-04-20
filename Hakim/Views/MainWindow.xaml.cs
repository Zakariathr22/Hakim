using Hakim.Services;
using Hakim.ViewModels;
using Hakim.Views.Settings;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;
using Windows.Graphics;
using Windows.UI;

namespace Hakim;

public sealed partial class MainWindow : Window
{
    private readonly MainViewModel viewModel = new();

    public MainWindow()
    {
        InitializeComponent();
        InitializeLocalization();

        mainPanel.DataContext = viewModel;
        AppWindow.Title = "Hakim";
        AppWindow.SetIcon("Assets/Icons/Hakim.ico");
        AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
        AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Tall;
        AppWindow.TitleBar.ButtonHoverForegroundColor = Color.FromArgb(0, 127, 127, 127);

        CenterWindow();
        viewModel.SetAppTheme(this);
        viewModel.SetAppBackDrop(this);
        navigationView.SelectedItem = navigationView.MenuItems.OfType<NavigationViewItem>().ElementAt(viewModel.LandingPage);

        mainPanel.Loaded += (_, _) => UpdateTitleBarColor();
        mainPanel.ActualThemeChanged += (_, _) => UpdateTitleBarColor();
    }

    private void UpdateTitleBarColor() =>
        AppWindow.TitleBar.ButtonForegroundColor = mainPanel.ActualTheme == ElementTheme.Dark
            ? Color.FromArgb(0, 255, 255, 255)
            : Color.FromArgb(0, 0, 0, 0);

    private void CenterWindow()
    {
        var area = DisplayArea.GetFromWindowId(AppWindow.Id, DisplayAreaFallback.Nearest)?.WorkArea;
        if (area == null) return;
        AppWindow.Move(new PointInt32((area.Value.Width - AppWindow.Size.Width) / 2, (area.Value.Height - AppWindow.Size.Height) / 2));
    }

    private void navigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is NavigationViewItem item)
        {
            var tag = item.Tag?.ToString();
            var type = Type.GetType($"Hakim.Views.{tag}.{tag}Page");
            if (type != null) contentFrame.Navigate(type);
            else System.Diagnostics.Debug.WriteLine($"Page not found: {tag}");
        }
    }

    public void InitializeLocalization()
    {
        void Set(NavigationViewItem item, TextBlock label, string key)
        {
            var text = LanguageService.GetResourceValue(key);
            ToolTipService.SetToolTip(item, text);
            label.Text = text;
        }

        searchAutoSuggestBox.PlaceholderText = LanguageService.GetResourceValue("Search");
        Set(homeNavigationItem, homeNavigationItemText, "Home");
        Set(patientsNavigationItem, patientsNavigationItemText, "Patients");
        Set(scheduleNavigationItem, scheduleNavigationItemText, "Appointments");
        Set(settingsNavigationItem, settingsNavigationItemText, "Settings");
        Set(statisticsNavigationItem, statisticsNavigationItemText, "Statistics");
    }

    private void PersonPicture_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        App.isNavigatingToUser = true;
        navigationView.SelectedItem = settingsNavigationItem;
    }
}
