using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakim.Converters
{
    internal class AppointmentIconUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime appointmentDate)
            {
                if (appointmentDate.Date > DateTime.Now.Date)
                {
                    return @"\Assets\Icons\Future.png";
                }
                else return @"\Assets\Icons\OldAppointment.png";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
