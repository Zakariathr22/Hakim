using Hakim.Models;
using Microsoft.UI.Xaml.Controls;

namespace Hakim.Views.Patients.Patient.Consultations;

public sealed partial class AddEditConsultaionFilePage : Page
{
    ContentDialog dialog;
    MedicalConsultation consultation;
    public AddEditConsultaionFilePage(ContentDialog dialog, MedicalConsultation consultation)
    {
        this.InitializeComponent();
        this.dialog = dialog;
        this.consultation = consultation;
        DataContext = consultation;
    }

    private void titleTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if(string.IsNullOrEmpty(titleTextBox.Text))
            dialog.IsPrimaryButtonEnabled = false;
        else dialog.IsPrimaryButtonEnabled = true;
    }
}
