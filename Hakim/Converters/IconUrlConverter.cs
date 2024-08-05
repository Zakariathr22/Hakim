using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakim.Converters
{
    internal class IconUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string type)
            {
                if (type == "Consultation")
                    return @"\Assets\Icons\Counselor.png";
                else if (type == "Radiographie")
                    return @"\Assets\Icons\X-ray.png";
                else if (type == "Radiographie télémétrie")
                    return @"\Assets\Icons\X-ray1.png";
                else if (type == "Protocol opératoire")
                    return @"\Assets\Icons\Surgery.png";
                else return @"\Assets\Icons\file.png";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
