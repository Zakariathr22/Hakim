using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace Hakim.Controls;

public sealed partial class HomePageHeaderImage : UserControl
{
    public HomePageHeaderImage() => InitializeComponent();

    public void SetGradianOfBackground(Color background) =>
        GradintBorder.Background = new LinearGradientBrush
        {
            StartPoint = new Windows.Foundation.Point(0, 0),
            EndPoint = new Windows.Foundation.Point(0, 1),
            GradientStops =
            {
                new GradientStop { Color = Color.FromArgb(0, 0, 0, 0), Offset = 0.2 },
                new GradientStop { Color = background, Offset = 0.95 }
            }
        };
}