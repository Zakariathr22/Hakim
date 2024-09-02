using CommunityToolkit.WinUI;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Hakim.View.Controls
{
    public sealed partial class CustomCalendarView : UserControl
    {
        public static readonly DependencyProperty AppointmentsDataProperty =
            DependencyProperty.Register(
                "AppointmentsData",
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
            }
        }

        public CustomCalendarView()
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
                Border criticalBackground;
                if (count < 6)
                    criticalBackground = (Border)this.Resources["FewAppointmentsBackground"];
                else if (count >= 6 && count < 11) 
                    criticalBackground = (Border)this.Resources["AverageAppointmentsBackground"];            
                else
                    criticalBackground = (Border)this.Resources["ManyAppointmentsBackground"];
                dayItem.Background = criticalBackground.Background;
            } 
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
            var control = (CustomCalendarView)d;
            var newData = (Dictionary<DateTime, int>)e.NewValue;

            // Handle the new data, e.g., refresh the UI
            control.RefreshCalendarView();
        }

        private void RefreshCalendarView()
        {
            // Update the CalendarView based on the new dictionary data
            AddToolTipToAllDays();
        }
    }
}
