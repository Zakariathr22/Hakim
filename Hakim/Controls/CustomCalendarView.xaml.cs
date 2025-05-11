using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;

namespace Hakim.Controls;

/// <summary>
/// Custom calendar view that shows appointment density using background color.
/// </summary>
public sealed partial class CustomCalendarView : UserControl
{
    public static readonly DependencyProperty AppointmentsDataProperty =
        DependencyProperty.Register(
            nameof(AppointmentsData),
            typeof(Dictionary<DateTime, int>),
            typeof(CustomCalendarView),
            new PropertyMetadata(new Dictionary<DateTime, int>(), OnAppointmentsDataChanged));

    public Dictionary<DateTime, int> AppointmentsData
    {
        get => (Dictionary<DateTime, int>)GetValue(AppointmentsDataProperty);
        set => SetValue(AppointmentsDataProperty, value);
    }

    public static readonly DependencyProperty SelectedDateProperty =
        DependencyProperty.Register(
            nameof(SelectedDate),
            typeof(DateTime),
            typeof(CustomCalendarView),
            new PropertyMetadata(DateTime.Now));

    public DateTime SelectedDate
    {
        get => (DateTime)GetValue(SelectedDateProperty);
        set => SetValue(SelectedDateProperty, value);
    }

    private static readonly Windows.UI.Color DensityColor = Windows.UI.Color.FromArgb(127, 0, 120, 212);
    private static readonly string Today = DateTime.Now.Date.ToString("d");
    private static readonly string Yesterday = DateTime.Now.Date.AddDays(-1).ToString("d");
    private static readonly string Tomorrow = DateTime.Now.Date.AddDays(1).ToString("d");

    public CustomCalendarView()
    {
        InitializeComponent();
        SelectedDate = DateTime.Now;
        calendarView.SelectedDates.Add(SelectedDate.Date);
        calendarView.SelectedDatesChanged += CalendarView_SelectedDatesChanged;
        calendarView.CalendarViewDayItemChanging += CalendarView_CalendarViewDayItemChanging;
        calendarView.ActualThemeChanged += CalendarView_ActualThemeChanged;
    }

    private void CalendarView_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
    {
        if (calendarView.SelectedDates.Count > 0)
        {
            SelectedDate = calendarView.SelectedDates[0].DateTime;
        }
        else
        {
            calendarView.SelectedDates.Add(SelectedDate.Date);
        }
    }

    private void CalendarView_ActualThemeChanged(FrameworkElement sender, object args) => ApplyToolTipsAndColors();

    private void CalendarView_CalendarViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
    {
        if (args.Phase == 0)
        {
            ToolTipService.SetToolTip(args.Item, null);
            args.RegisterUpdateCallback(CalendarView_CalendarViewDayItemChanging);
        }
        else if (args.Phase == 1 && AppointmentsData.TryGetValue(args.Item.Date.Date, out int count) && count > 0)
        {
            AddToolTip(args.Item, count);
        }
    }

    private void AddToolTip(CalendarViewDayItem item, int count)
    {
        string dateStr = item.Date.Date.ToString("d");
        string dayLabel = dateStr == Yesterday ? "Yesterday" :
                          dateStr == Today ? "Today" :
                          dateStr == Tomorrow ? "Tomorrow" :
                          dateStr;

        string tooltipText = $"{count} {(count == 1 ? "Appointment" : "Appointments")}\r\n{dayLabel}";

        ToolTipService.SetToolTip(item, new ToolTip
        {
            Content = tooltipText,
            Placement = PlacementMode.Mouse
        });

        if (dateStr != Today)
        {
            item.SetDensityColors(GetDensityColors(count));
        }
    }

    private static IEnumerable<Windows.UI.Color> GetDensityColors(int count)
    {
        int cappedCount = Math.Min(10, count);
        Windows.UI.Color[] colors = new Windows.UI.Color[cappedCount];
        for (int i = 0; i < cappedCount; i++)
        {
            colors[i] = DensityColor;
        }
        return colors;
    }

    private void ApplyToolTipsAndColors()
    {
        foreach (var item in FindVisualChildren<CalendarViewDayItem>(calendarView))
        {
            var date = item.Date.Date;
            if (AppointmentsData.TryGetValue(date, out int count) && count > 0)
            {
                ToolTipService.SetToolTip(item, null);
                AddToolTip(item, count);
            }
            else
            {
                ToolTipService.SetToolTip(item, null);
            }
        }
    }

    private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
    {
        if (depObj == null) yield break;

        var queue = new Queue<DependencyObject>();
        queue.Enqueue(depObj);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            int childCount = VisualTreeHelper.GetChildrenCount(current);
            for (int i = 0; i < childCount; i++)
            {
                var child = VisualTreeHelper.GetChild(current, i);
                if (child is T match)
                {
                    yield return match;
                }
                queue.Enqueue(child);
            }
        }
    }

    private static void OnAppointmentsDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is CustomCalendarView control && e.NewValue is Dictionary<DateTime, int>)
        {
            control.ApplyToolTipsAndColors();
        }
    }
}