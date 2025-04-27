using Hakim.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Hakim.Views.Patients.AddPatient
{
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
