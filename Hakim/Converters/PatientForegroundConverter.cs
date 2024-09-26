using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakim.Converters
{
    public class PatientForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int patientId)
            {
                if (patientId % 4 == 0)
                {
                    return App.Current.Resources["GreenForegroundStyle"];
                }
                else if (patientId % 3 == 0)
                {
                    return App.Current.Resources["AmberForegroundStyle"];
                }
                else if (patientId % 2 == 0)
                {
                    return App.Current.Resources["RedForegroundStyle"];
                }
                else
                {
                    return App.Current.Resources["AccentForegroundStyle"];
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}