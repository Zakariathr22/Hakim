using Hakim.Models;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;

namespace Hakim.Views.Patients.Patient.Appointments;

public sealed partial class AddAppointmentPage : Page
{
    ContentDialog dialog;
    Appointment appointment;
    public AddAppointmentPage(ContentDialog dialog, Appointment appointment, Dictionary<DateTime, int> appointmentCounts)
    {
        this.InitializeComponent();
        this.dialog = dialog;
        this.appointment = appointment;
        DataContext = appointment;
        datePicker.AppointmentsData = appointmentCounts;
    }
}
