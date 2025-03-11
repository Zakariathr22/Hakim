using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace Hakim.Views.Patients.Patient
{
    public sealed partial class PatientDetailsDisplayControl : UserControl
    {
        public PatientDetailsDisplayControl(Models.Patient patient)
        {
            this.InitializeComponent();
            DataContext = patient;
        }

        private void GeneralHeader_Tapped(object sender, TappedRoutedEventArgs e)
        {
            GeneralTextBlock.Style = App.Current.Resources["SelectedTextBlockStyle"] as Style;
            InfoTextBlock.Style = App.Current.Resources["UnselectedTextBlockStyle"] as Style;
        }

        private void InfoHeader_Tapped(object sender, TappedRoutedEventArgs e)
        {
            GeneralTextBlock.Style = App.Current.Resources["UnselectedTextBlockStyle"] as Style;
            InfoTextBlock.Style = App.Current.Resources["SelectedTextBlockStyle"] as Style;
        }
    }
}
