using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hakim.Models;

public partial class File 
{
    public int id { get; set; }
    private Patient patient;
    private string title;
    private DateTime creationDate;
    private string url;
    private int type;
}

public partial class File : INotifyPropertyChanged
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

    public string Title
    {
        get => title;
        set
        {
            title = value;
            OnPropertyChanged();
        }
    }

    public DateTime CreationDate
    {
        get => creationDate;
        set
        {
            creationDate = value;
            OnPropertyChanged();
        }
    }

    public string Url
    {
        get => url;
        set
        {
            url = value;
            OnPropertyChanged();
        }
    }

    public int Type
    {
        get => type;
        set
        {
            type = value;
            OnPropertyChanged();
        }
    }
}
