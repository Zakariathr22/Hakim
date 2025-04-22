using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;

namespace Hakim.Views.Home;

public sealed partial class HomePage : Page
{
    public HomePage()
    {
        InitializeComponent();
        Loaded += HomePage_Loaded;
        scrollViewer.ViewChanged += (s, e) => UpdateScrollButtonVisibility();
        SizeChanged += (s, e) => UpdateScrollButtonVisibility(ActualWidth > scrollViewer.ActualWidth);
        ActualThemeChanged += (s, e) =>
        {
            if (Background is SolidColorBrush brush)
                homePageHeaderImage.SetGradianOfBackground(brush.Color);
        };
    }

    private void HomePage_Loaded(object sender, RoutedEventArgs e)
    {
        if (Background is SolidColorBrush brush)
            homePageHeaderImage.SetGradianOfBackground(brush.Color);
        UpdateScrollButtonVisibility();
    }

    private void ScrollRight(double amount) => Scroll(amount);
    private void ScrollLeft(double amount) => Scroll(-amount);

    private void Scroll(double amount)
    {
        if (scrollViewer == null) return;
        double newOffset = Math.Clamp(scrollViewer.HorizontalOffset + amount, 0, scrollViewer.ScrollableWidth);
        scrollViewer.ChangeView(newOffset, null, null);
    }

    private void UpdateScrollButtonVisibility(bool hideButtons = false)
    {
        if (scrollViewer == null) return;
        scrollLeftButton.Visibility = (scrollViewer.HorizontalOffset > 0 && !hideButtons) ? Visibility.Visible : Visibility.Collapsed;
        scrollRightButton.Visibility = (scrollViewer.HorizontalOffset < scrollViewer.ScrollableWidth && !hideButtons) ? Visibility.Visible : Visibility.Collapsed;
    }

    private void scrollLeftButton_Click(object sender, RoutedEventArgs e) => ScrollLeft(440);
    private void scrollRightButton_Click(object sender, RoutedEventArgs e) => ScrollRight(440);
}