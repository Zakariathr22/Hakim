using Hakim.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Hakim.Views.Patients.Patient;

public sealed partial class PatientInfoEditorControl : UserControl
{
    PatientPage ParentPage;
    Models.Patient patient;
    public PatientInfoEditorControl(Models.Patient patient)
    {
        this.InitializeComponent();
        this.patient = patient;
        DataContext = this.patient;
        this.Loaded += PatientInfoEditorControl_Loaded;
    }

    private void PatientInfoEditorControl_Loaded(object sender, RoutedEventArgs e)
    {
        ParentPage = VisualTreeExtensionsService.FindParent<PatientPage>(this);
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        ParentPage.ShowEditPatientDialog(this.patient);
    }
}
