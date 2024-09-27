using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakim.Converters
{
    public class TypeToPrintVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // Check if the value is an integer
            if (value is int intValue)
            {
                // Return Visible if the value is 1 or 4, else return Collapsed
                return (intValue == 0 || intValue == 3) ? Visibility.Visible : Visibility.Collapsed;
            }

            // Default to Collapsed if value is not an integer
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
