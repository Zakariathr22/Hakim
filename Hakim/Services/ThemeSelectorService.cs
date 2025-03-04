using Microsoft.UI.Xaml;

namespace Hakim.Services
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
    }
}
