using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hakim.Models;

public partial class Appointment
{
    public int id { set; get; }
    private Patient patient;
    private DateTime appointmentDate;
    private TimeSpan appointmentTime;
    private string purpose;
    private string notes;
}

public partial class Appointment : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string PropertyName = "") =>
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

    public Patient Patient
    {
        get => patient;
        set
        {
            patient = value;
            OnPropertyChanged();
        }
    }

    public DateTime AppointmentDate
    {
        get => appointmentDate;
        set
        {
            appointmentDate = value;
            OnPropertyChanged();
        }
    }

    public TimeSpan AppointmentTime
    {
        get => appointmentTime;
        set
        {
            appointmentTime = value;
            OnPropertyChanged();
        }
    }

    public string Purpose
    {
        get => purpose;
        set
        {
            purpose = value;
            OnPropertyChanged();
        }
    }

    public string Notes
    {
        get => notes;
        set
        {
            notes = value;
            OnPropertyChanged();
        }
    }
}
