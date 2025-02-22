using Hakim.Model;
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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Hakim.View.Clients.Patient
{
    public sealed partial class PatientInfoEditorControl : UserControl
    {
        PatientPage ParentPage;
        Model.Patient patient;
        public PatientInfoEditorControl(Model.Patient patient)
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
}
