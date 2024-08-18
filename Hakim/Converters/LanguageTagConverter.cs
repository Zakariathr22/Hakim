using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakim.Converters
{
    internal class LanguageTagConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string tag && parameter is ComboBox comboBox)
            {
                for (int i = 0; i < comboBox.Items.Count; i++)
                {
                    if (comboBox.Items[i] is ComboBoxItem item && item.Tag?.ToString() == tag)
                    {
                        return i;
                    }
                }
            }
            return 0; // Return 0 if no match is found
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is int index && parameter is ComboBox comboBox)
            {
                if (index >= 0 && index < comboBox.Items.Count)
                {
                    if (comboBox.Items[index] is ComboBoxItem item)
                    {
                        return item.Tag?.ToString();
                    }
                }
            }
            return "en-US"; // Return "en-US" if the index is out of range or no match is found
        }
    }

}
