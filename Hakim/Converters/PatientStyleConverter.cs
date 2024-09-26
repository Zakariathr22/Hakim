using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakim.Converters
{
    public class PatientStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int patientId)
            {
                if (patientId % 4 == 0)
                {
                    return App.Current.Resources["GreenBackgroundStyle"];
                }
                else if (patientId % 3 == 0)
                {
                    return App.Current.Resources["AmberBackgroundStyle"];
                }
                else if (patientId % 2 == 0)
                {
                    return App.Current.Resources["RedBackgroundStyle"];
                }
                else
                {
                    return App.Current.Resources["AccentBackgroundStyle"];
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
