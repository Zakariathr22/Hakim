using Hakim.Service;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakim.Converters
{
    class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int type)
            {
                if (type == 0)
                    return LanguageService.GetResourceValue("Male");
                else if (type == 1)
                    return LanguageService.GetResourceValue("Female");
            }
            return LanguageService.GetResourceValue("NotEntered");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
