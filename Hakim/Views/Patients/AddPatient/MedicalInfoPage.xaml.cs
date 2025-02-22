using Hakim.Service;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Hakim.View.Clients.AddPatient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within PatientDetailsDisplay Frame.
    /// </summary>
    public sealed partial class MedicalInfoPage : Page
    {
        AddPatientPage p;

        public MedicalInfoPage()
        {
            this.InitializeComponent();
            this.InitializeLocation();
            Loaded += MedicalInfoPage_Loaded;
        }

        private void MedicalInfoPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = p.viewModel;
            this.p.dialog.SecondaryButtonText = LanguageService.GetResourceValue("Save");
            this.p.dialog.SecondaryButtonStyle = Application.Current.Resources["AccentButtonStyle"] as Style;
            this.p.dialog.PrimaryButtonText = null;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is AddPatientPage)
            {
                p = e.Parameter as AddPatientPage;
            }
            base.OnNavigatedTo(e);
        }

        public void InitializeLocation()
        {
            medicalInformationSubtitle.Text = LanguageService.GetResourceValue("MedicalInformation");

            medicalHistoryTextBox.Header = LanguageService.GetResourceValue("MedicalHistory");
            medicalHistoryTextBox.PlaceholderText = LanguageService.GetResourceValue("MedicalHistoryDetails");

            allergiesTextBox.Header = LanguageService.GetResourceValue("Allergies");
            allergiesTextBox.PlaceholderText = LanguageService.GetResourceValue("AllergiesDetails");

            currentMedicationsTextBox.Header = LanguageService.GetResourceValue("CurrentMedications");
            currentMedicationsTextBox.PlaceholderText = LanguageService.GetResourceValue("CurrentMedicationsDetails");
        }
    }
}
