using Hakim.Service;
using Hakim.View.Home;
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
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Hakim.View.Controls
{
    public sealed partial class HomePageHeaderImage : UserControl
    {
        public HomePageHeaderImage()
        {
            this.InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (this.ActualTheme == ElementTheme.Dark)
            {
                
            }
        }

        public void SetGradianOfBackground(Color background)
        {
            // Create a LinearGradientBrush
            var linearGradientBrush = new LinearGradientBrush
            {
                StartPoint = new Windows.Foundation.Point(0, 0.25),
                EndPoint = new Windows.Foundation.Point(0, 1)
            };

            // Add GradientStops to the LinearGradientBrush
            linearGradientBrush.GradientStops.Add(new GradientStop
            {
                Color = Windows.UI.Color.FromArgb(0, 0, 0, 0),
                Offset = 0.0
            });
            linearGradientBrush.GradientStops.Add(new GradientStop
            {
                Color = background,
                Offset = 0.95
            });

            // Apply the brush to a control's background
            GradintBorder.Background = linearGradientBrush;
        }
    }
}
