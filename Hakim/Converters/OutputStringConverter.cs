using Hakim.Service;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakim.Converters
{
    internal class OutputStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string input)
            {
                if (!string.IsNullOrEmpty(input))
                    return input;
                else
                    return LanguageService.GetResourceValue("NotEntered");
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
