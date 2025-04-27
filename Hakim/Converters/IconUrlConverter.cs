using Microsoft.UI.Xaml.Data;
using System;

namespace Hakim.Converters;

internal class IconUrlConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is int type)
        {
            if (type == 0)
                return @"\Assets\Icons\Counselor.png";
            else if (type == 1)
                return @"\Assets\Icons\X-ray.png";
            else if (type == 2)
                return @"\Assets\Icons\X-ray1.png";
            else if (type == 3)
                return @"\Assets\Icons\Surgery.png";
            else return @"\Assets\Icons\file.png";
        }
        return @"\Assets\Icons\file.png";
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
