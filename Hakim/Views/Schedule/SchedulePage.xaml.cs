using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;

namespace Hakim.Views.Schedule;

public sealed partial class SchedulePage : Page
{
    public SchedulePage()
    {
        this.InitializeComponent();
        CustomCalendarDatePicker.AppointmentsData = new Dictionary<DateTime, int>
        {
            { new DateTime(2024, 9, 1), 5 },
            { new DateTime(2024, 9, 3), 15 },
            { new DateTime(2024, 9, 4), 1 },
            { new DateTime(2024, 9, 5), 25 },
        };
    }
}
