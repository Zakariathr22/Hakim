using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Hakim.View.Home
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within PatientDetailsDisplay Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
            this.Loaded += HomePage_Loaded;
            scrollViewer.ViewChanged += ScrollViewer_ViewChanged;
            SizeChanged += HomePage_SizeChanged;
        }

        private void HomePage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ActualWidth > scrollViewer.ActualWidth)
            {
                scrollRightButton.Visibility = Visibility.Collapsed;
                scrollLeftButton.Visibility = Visibility.Collapsed;
            }
            else
                UpdateScrollButtonVisibility();

        }

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            UpdateScrollButtonVisibility();
        }

        private void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            SolidColorBrush backgroundBrush = this.Background as SolidColorBrush;
            if (backgroundBrush != null)
            {
                Color backgroundColor = backgroundBrush.Color;
                homePageHeaderImage.SetGradianOfBackground(backgroundColor);
            }
            UpdateScrollButtonVisibility();
        }

        private void HomePage_Loading(FrameworkElement sender, object args)
        {

        }

        private void ScrollRight(double amount)
        {
            if (scrollViewer == null) return;

            double newOffset = scrollViewer.HorizontalOffset + amount;
            // Ensure the new offset is within bounds
            newOffset = Math.Max(0, Math.Min(newOffset, scrollViewer.ScrollableWidth));
            scrollViewer.ChangeView(newOffset, null, null);
        }

        // Scroll left by a specific amount
        private void ScrollLeft(double amount)
        {
            if (scrollViewer == null) return;

            double newOffset = scrollViewer.HorizontalOffset - amount;
            // Ensure the new offset is within bounds
            newOffset = Math.Max(0, Math.Min(newOffset, scrollViewer.ScrollableWidth));
            scrollViewer.ChangeView(newOffset, null, null);
        }

        private void scrollLeftButton_Click(object sender, RoutedEventArgs e)
        {
            ScrollLeft(480);
        }

        private void scrollRightButton_Click(object sender, RoutedEventArgs e)
        {
            ScrollRight(480);
        }

        private void UpdateScrollButtonVisibility()
        {
            if (scrollViewer == null) return;

            scrollLeftButton.Visibility = scrollViewer.HorizontalOffset > 0 ? Visibility.Visible : Visibility.Collapsed;
            scrollRightButton.Visibility = scrollViewer.HorizontalOffset < scrollViewer.ScrollableWidth ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
