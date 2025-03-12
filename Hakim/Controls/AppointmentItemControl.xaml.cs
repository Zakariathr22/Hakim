using Hakim.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using System.Diagnostics;

namespace Hakim.Views.Patients.Patient
{
    public sealed partial class AppointmentItemControl : UserControl
    {
        PatientPage ParentPage;

        public AppointmentItemControl()
        {
            this.InitializeComponent();
            Loaded += AppointmentItemControl_Loaded;
        }

        private void AppointmentItemControl_Loaded(object sender, RoutedEventArgs e)
        {
            ParentPage = VisualTreeExtensionsService.FindParent<PatientPage>(this);
            if (ParentPage != null)
            {
                // You now have access to the parent page
                Debug.WriteLine("Parent Page found: " + ParentPage.GetType().Name);
            }
        }

        private void _ContextRequested(UIElement sender, ContextRequestedEventArgs args)
        {
            CommandBarFlyout flyout = this.Resources["AppointmentCommandBarFlyout"] as CommandBarFlyout;
            FlyoutShowOptions showModeOption = new FlyoutShowOptions
            {
                ShowMode = FlyoutShowMode.Transient
            };
            flyout.ShowAt(this, showModeOption);
        }

        private void deleteAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            AppointmentCommandBarFlyout.Hide();

            if (this.DataContext is Models.Appointment appointment)
            {
                // Now you have access to the associated Patient object
                ParentPage.viewModel.DeleteAppointmentById(appointment.id);
                ParentPage.UpdateAppointmentsList();
                ParentPage.patientRecords.UpdateAppointmentsDisplayVisibility(ParentPage.viewModel.SelectedPatient);
            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is Models.Appointment appointment)
            {
                AppointmentCommandBarFlyout.Hide();
                ParentPage.ShowEditAppointmentDialog(appointment);
            }
        }
    }
}
