using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakim.Converters
{
    internal class FileTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int type)
            {
                if (type == 0)
                    return @"Consultation";
                else if (type == 1)
                    return @"Radiographie";
                else if (type == 2)
                    return @"Radiographie télémétrie";
                else if (type == 3)
                    return @"Protocol opératoire";
                else return @"Fichier médical";
            }
            return @"Fichier médical";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
