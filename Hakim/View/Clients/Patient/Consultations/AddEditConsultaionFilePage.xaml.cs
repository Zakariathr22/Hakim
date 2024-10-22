using Hakim.Model;
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

namespace Hakim.View.Clients.Patient.Consultations
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
}
