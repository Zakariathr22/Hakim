using Microsoft.UI.Xaml.Data;
using System;

namespace Hakim.Converters;

internal class DateShortTimeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is DateTime dateTime)
        {
            return dateTime.ToString("g"); // Format the date as short date pattern (e.g., MM/dd/yyyy)
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
