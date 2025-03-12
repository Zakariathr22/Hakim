using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Hakim.Models;

namespace Hakim.Views.Patients.Patient.Appointments
{
    public sealed partial class EditAppointmentPage : Page
    {       
        ContentDialog dialog;
        Appointment appointment;
        public EditAppointmentPage(ContentDialog dialog, Appointment appointment, Dictionary<DateTime, int> appointmentCounts)
        {
            this.InitializeComponent();
            this.dialog = dialog;
            this.appointment = appointment;
            DataContext = appointment;
            datePicker.AppointmentsData = appointmentCounts;
        }
    }
}
