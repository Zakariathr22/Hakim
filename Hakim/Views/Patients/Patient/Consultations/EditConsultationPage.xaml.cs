using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml.Controls;
using Hakim.Models;

namespace Hakim.Views.Patients.Patient.Consultations
{
    public sealed partial class EditConsultationPage : Page
    {
        ContentDialog dialog;
        Models.File file;

        public EditConsultationPage(ContentDialog dialog, Models.File file)
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
}
