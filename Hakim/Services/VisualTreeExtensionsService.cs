using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml;

namespace Hakim.Services;

public static class VisualTreeExtensionsService
{
    public static T FindParent<T>(DependencyObject child) where T : DependencyObject
    {
        DependencyObject parentObject = VisualTreeHelper.GetParent(child);

        if (parentObject == null) return null;

        T parent = parentObject as T;
        if (parent != null)
        {
            return parent;
        }
        else
        {
            return FindParent<T>(parentObject);
        }
    }
}
