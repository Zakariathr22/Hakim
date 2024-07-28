using Hakim.Model;
using Hakim.Service;
using Hakim.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static System.Net.Mime.MediaTypeNames;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Hakim.View.Clients
{
    public sealed partial class PatientCardControl : UserControl
    {
        private ClientsPage ParentPage;
        public PatientCardControl()
        {
            this.InitializeComponent();
            Loaded += PatientCardControl_Loaded;
        }

        private void PatientCardControl_Loaded(object sender, RoutedEventArgs e)
        {
            ParentPage = VisualTreeExtensionsService.FindParent<ClientsPage>(this);
            if (ParentPage != null)
            {
                // You now have access to the parent page
                Debug.WriteLine("Parent Page found: " + ParentPage.GetType().Name);
            }
        }

        private void ShowMenu()
        {
            FlyoutShowOptions myOption = new FlyoutShowOptions();
            myOption.ShowMode = FlyoutShowMode.TransientWithDismissOnPointerMoveAway;
            CommandBarFlyout.ShowAt(this, myOption);
        }

        private void CustomButton_ContextRequested(UIElement sender, ContextRequestedEventArgs args)
        {
            ShowMenu();
        }

        private void deletePatientButton_Click(object sender, RoutedEventArgs e)
        {
            CommandBarFlyout.Hide();
            if (this.DataContext is Patient patient)
            {
                // Now you have access to the associated Patient object
                Debug.WriteLine($"Patient Name: {patient.LastName}, Age: {patient.FirstName}");
                ParentPage.viewModel.DeletePatientById(patient.id);
                ParentPage.UpdatePatientSearchResults(ParentPage.SearchAutoSuggestBox);
            }
        }
    }
}
