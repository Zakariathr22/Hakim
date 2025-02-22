using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakim.Service
{
    public static class ThemeSelectorService
    {
        public static ElementTheme GetTheme(Window window)
        {
            if (window?.Content is FrameworkElement element)
            {
                return element.ActualTheme;
            }
            return ElementTheme.Default;
        }

        public static void SetTheme(ElementTheme theme, Window window)
        {
            if (window?.Content is FrameworkElement element)
            {
                element.RequestedTheme = theme;
            }
        }

        public static SolidColorBrush SetBrush(string type)
        {
            SolidColorBrush myBrush = new SolidColorBrush();

            if (type == "Critical")
            {
                // Set the Color property of the brush to an RGB color
                myBrush.Color = new Windows.UI.Color()
                {
                    A = 96, // Alpha (transparency)
                    R = 255, // Red
                    G = 128,   // Green
                    B = 128    // Blue
                };
            }

            return myBrush;
        }
    }
}
