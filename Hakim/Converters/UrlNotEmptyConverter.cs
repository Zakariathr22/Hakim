using Microsoft.UI.Xaml.Data;
using System;

namespace Hakim.Converters;

internal class UrlNotEmptyConverter : IValueConverter
{

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is string url)
        {
            // Remove leading/trailing spaces and check if the URL is empty
            return !string.IsNullOrEmpty(url);
        }

        // If the value is not a string, return false
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
