using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace Hakim.Controls
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
