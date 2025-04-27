using System;

namespace Hakim.Models;

public partial class XRay : File
{
    private DateTimeOffset xray_date;
    private TimeSpan xray_time;
    private string radiologist;
    private string diagnosis;
    private int xray_type;
}
public partial class XRay
{
    public DateTimeOffset Xray_date
    {
        get => xray_date;
        set
        {
            xray_date = value;
            OnPropertyChanged();
        }
    }

    public TimeSpan XrayTime
    {
        get => xray_time;
        set
        {
            xray_time = value;
            OnPropertyChanged();
        }
    }

    public string Radiologist
    {
        get => radiologist;
        set
        {
            radiologist = value;
            OnPropertyChanged();
        }
    }

    public string Diagnosis
    {
        get => diagnosis;
        set
        {
            diagnosis = value;
            OnPropertyChanged();
        }
    }

    public int Xray_type
    {
        get => xray_type;
        set
        {
            xray_type = value;
            OnPropertyChanged();
        }
    }
}
