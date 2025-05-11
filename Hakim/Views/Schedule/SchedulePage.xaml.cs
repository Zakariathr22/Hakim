using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;

namespace Hakim.Views.Schedule;

public sealed partial class SchedulePage : Page
{
    public Dictionary<DateTime, int> AppointmentsData { get; set; }
    public DateTime CurrentDate { get; set; }

    public SchedulePage()
    {
        this.InitializeComponent();

        var today = DateTime.Now.Date;
        var totalDays = (15 * 365);
        AppointmentsData = new Dictionary<DateTime, int>(totalDays);

        for (int i = -3650; i <= 1825; i++) // 10 years back, 5 years forward
        {
            var date = today.AddDays(i);
            AppointmentsData[date] = Random.Shared.Next(0, 25); // density between 0 and 24
        }
    }
}
