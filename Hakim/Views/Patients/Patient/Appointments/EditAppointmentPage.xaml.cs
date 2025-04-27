using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using Hakim.Models;

namespace Hakim.Views.Patients.Patient.Appointments;

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
        datePicker.SelectedDate = appointment.AppointmentDate;
    }
}
