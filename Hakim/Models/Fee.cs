using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hakim.Models;

public partial class Fee
{
    public int Id { get; set; }
    private Patient patient;
    private DateTime visitDate;
    private double amount;
    private double paidAmount;
    private string notes;
}

public partial class Fee : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public Patient Patient
    {
        get => patient;
        set
        {
            patient = value;
            OnPropertyChanged();
        }
    }

    public DateTime VisitDate
    {
        get => visitDate;
        set
        {
            visitDate = value;
            OnPropertyChanged();
        }
    }

    public double Amount
    {
        get => amount;
        set
        {
            amount = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(Status)); // Update status if amount changes
        }
    }

    public double PaidAmount
    {
        get => paidAmount;
        set
        {
            paidAmount = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(Status)); // Update status if paid changes
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

    public string Status =>
        paidAmount >= amount ? "Paid" :
        paidAmount == 0 ? "Unpaid" :
        "Partially Paid";
}