using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;

namespace Hakim.Controls;

public sealed partial class CustomCalendarDatePicker : UserControl
{
    public static readonly DependencyProperty AppointmentsDataProperty =
        DependencyProperty.Register(
            "AppointmentsData",
            typeof(Dictionary<DateTime, int>),
            typeof(CustomCalendarDatePicker),
            new PropertyMetadata(new Dictionary<DateTime, int>(), OnAppointmentsDataChanged));

    public Dictionary<DateTime, int> AppointmentsData
    {
        get => (Dictionary<DateTime, int>)GetValue(AppointmentsDataProperty);
        set => SetValue(AppointmentsDataProperty, value);
    }

    public static readonly DependencyProperty SelectedDateProperty =
        DependencyProperty.Register(
            "SelectedDate",         // The name of the property
            typeof(DateTime),       // The type of the property
            typeof(CustomCalendarDatePicker), // The owner of the property
            new PropertyMetadata(DateTime.Now)); // Default value

    public DateTime SelectedDate
    {
        get => (DateTime)GetValue(SelectedDateProperty);
        set
        {
            SetValue(SelectedDateProperty, value);
            dateTextBlock.Text = value.ToString("D");
        }
    }

    public static readonly DependencyProperty BorderWidthProperty =
        DependencyProperty.Register(
            "BorderWidth",         // The name of the property
            typeof(double),       // The type of the property
            typeof(CustomCalendarDatePicker), // The owner of the property
            new PropertyMetadata(64, OnBorderWidthChanged)); // Default value

    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }

    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register(
            "Header",         // The name of the property
            typeof(string),       // The type of the property
            typeof(CustomCalendarDatePicker), // The owner of the property
            new PropertyMetadata(string.Empty)); // Default value

    public string Header
    {
        get => (string)GetValue(HeaderProperty);
        set 
        { 
            SetValue(HeaderProperty, value);
            headerTextBlock.Text = value;
        }
    }

    public CustomCalendarDatePicker()
    {
        this.InitializeComponent();
        SelectedDate = DateTime.Now;
        calendarView.SelectedDates.Add(SelectedDate.Date);
        calendarView.SelectedDatesChanged += CalendarView_SelectedDatesChanged;
        calendarView.CalendarViewDayItemChanging += CalendarView_CalendarViewDayItemChanging;
        calendarView.ActualThemeChanged += CalendarView_ActualThemeChanged;
    }

    private void CalendarView_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
    {
        if (calendarView.SelectedDates.Count != 0)
        {
            SelectedDate = calendarView.SelectedDates[0].DateTime;
        }
        else
        {
            calendarView.SelectedDates.Add(SelectedDate.Date);
        }
        datePickerBorder.Flyout.Hide();
    }

    private void CalendarView_ActualThemeChanged(FrameworkElement sender, object args)
    {
        AddToolTipToAllDays();
    }

    private void CalendarView_CalendarViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
    {
        if (args.Phase == 0)
        {
            // Ensure we clear any previous content from reused items
            RemoveExistingToolTip(args.Item);

            // Register for the next phase
            args.RegisterUpdateCallback(CalendarView_CalendarViewDayItemChanging);
        }
        else if (args.Phase == 1)
        {
            DateTime date = args.Item.Date.Date;

            // Get the number of appointments for the current date
            if (AppointmentsData.TryGetValue(date, out int appointmentCount) && appointmentCount > 0)
            {
                // Add the appointment count ToolTip
                AddToolTip(args.Item, appointmentCount);
            }
        }
    }

    private void AddToolTip(CalendarViewDayItem dayItem, int count)
    {
        ToolTip toolTip;

        string dayLabel = dayItem.Date.Date == DateTime.Now.Date.AddDays(-1) ? "Yesterday" :
                          dayItem.Date.Date == DateTime.Now.Date ? "Today" :
                          dayItem.Date.Date == DateTime.Now.Date.AddDays(1) ? "Tomorrow" :
                          dayItem.Date.Date.ToString("d");

        string appointmentLabel = count == 1 ? "Appointment" : "Appointments";

        toolTip = new ToolTip
        {
            Content = $"{count} {appointmentLabel}\r\n{dayLabel}",
            Placement = PlacementMode.Mouse
        };

        // Associate the ToolTip with the CalendarViewDayItem
        ToolTipService.SetToolTip(dayItem, toolTip);
        if (dayItem.Date.Date != DateTime.Now.Date)
        {
            dayItem.SetDensityColors(GetColors(Windows.UI.Color.FromArgb(127, 0, 120, 212), count));
        }          
    }

    private IEnumerable<Windows.UI.Color> GetColors(Windows.UI.Color color, int count)
    {
        var colors = new List<Windows.UI.Color>();

        for (int i = 0; i < 10; i++)
        {
            if (i+1 > count) break;
            colors.Add(color);
        }

        return colors;
    }

    private void RemoveExistingToolTip(CalendarViewDayItem dayItem)
    {
        // Clear any existing ToolTip from the CalendarViewDayItem
        ToolTipService.SetToolTip(dayItem, null);
    }

    private void AddToolTipToAllDays()
    {
        foreach (var item in FindVisualChildren<CalendarViewDayItem>(calendarView))
        {
            DateTime date = item.Date.Date;
            if (AppointmentsData.TryGetValue(date, out int appointmentCount) && appointmentCount > 0)
            {
                // Remove any existing appointment count to prevent duplicates
                RemoveExistingToolTip(item);

                // Add the appointment count InfoBadge
                AddToolTip(item, appointmentCount);
            }
            else
            {
                // Ensure no residual InfoBadge exists for dates without appointments
                RemoveExistingToolTip(item);
            }
        }
    }

    // Helper method to recursively find children of a specific type
    private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
    {
        if (depObj == null) yield break;

        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
        {
            DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
            if (child is T t)
            {
                yield return t;
            }

            foreach (var childOfChild in FindVisualChildren<T>(child))
            {
                yield return childOfChild;
            }
        }
    }

    private static void OnAppointmentsDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (CustomCalendarDatePicker)d;
        var newData = (Dictionary<DateTime, int>)e.NewValue;

        // Handle the new data, e.g., refresh the UI
        control.RefreshCalendarView();
    }

    private static void OnBorderWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (CustomCalendarDatePicker)d;
        control.datePickerBorder.Width = (double)e.NewValue;
    }

    private void RefreshCalendarView()
    {
        // Update the CalendarView based on the new dictionary data
        AddToolTipToAllDays();
    }
}
