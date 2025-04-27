using Hakim.Models;
using Microsoft.UI.Xaml.Controls;

namespace Hakim.Views.Patients.Patient.SurgeryProtocols;

public sealed partial class AddSurgeryProtocolPage : Page
{
    ContentDialog dialog;
    SurgeryProtocol surgeryProtocol;
    public AddSurgeryProtocolPage(ContentDialog dialog, SurgeryProtocol surgeryProtocol)
    {
        this.InitializeComponent();
        this.dialog = dialog;
        this.surgeryProtocol = surgeryProtocol;
        DataContext = surgeryProtocol;
    }

    private void titleTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (titleTextBox.Text != "")
            dialog.IsPrimaryButtonEnabled = true;
        else dialog.IsPrimaryButtonEnabled = false;
    }
}
