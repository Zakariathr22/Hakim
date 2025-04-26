using Microsoft.UI.Xaml.Controls;

namespace Hakim.Views.Patients.Patient;

public sealed partial class RenameFilePage : Page
{
    ContentDialog dialog;
    Models.File file;

    public RenameFilePage(ContentDialog dialog, Models.File file)
    {
        this.InitializeComponent();
        this.dialog = dialog;
        this.file = file;
        DataContext = file;
    }

    private void titleTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(titleTextBox.Text))
            dialog.IsPrimaryButtonEnabled = false;
        else dialog.IsPrimaryButtonEnabled = true;
    }      
}
