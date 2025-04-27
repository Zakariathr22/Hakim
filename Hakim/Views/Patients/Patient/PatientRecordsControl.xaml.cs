using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Windows.UI;
using Hakim.Services;
using System.Diagnostics;

namespace Hakim.Views.Patients.Patient;

public sealed partial class PatientRecordsControl : UserControl
{
    PatientPage ParentPage;
    public PatientRecordsControl(Models.Patient patient)
    {
        this.InitializeComponent();
        PatientFiles.ItemsSource = patient.files;
        PatientAppointments.ItemsSource = patient.appointments;
        this.Loaded += PatientRecordsControl_Loaded;
        UpdateFilesDisplayVisibility(patient);
        UpdateAppointmentsDisplayVisibility(patient);
    }

    private void PatientRecordsControl_Loaded(object sender, RoutedEventArgs e)
    {
        ParentPage = VisualTreeExtensionsService.FindParent<PatientPage>(this);
        if (ParentPage != null)
        {
            // You now have access to the parent page
            Debug.WriteLine("Parent Page found: " + ParentPage.GetType().Name);
        }
    }

    private void FilesHeader_Tapped(object sender, TappedRoutedEventArgs e)
    {
        ParentPage.AddFileButton.Visibility = Visibility.Visible;
        ParentPage.SortFilesButton.Visibility = Visibility.Visible;
        ParentPage.FilterFilesButton.Visibility = Visibility.Visible;
        ParentPage.SearchAutoSuggestBox.Visibility = Visibility.Visible;
        ParentPage.AddAppointmentButton.Visibility = Visibility.Collapsed;
        ParentPage.SortAppintmentsButton.Visibility = Visibility.Collapsed;
        ParentPage.FilterAppointmentsButton.Visibility = Visibility.Collapsed;

        docsImageD.Visibility = Visibility.Collapsed;
        docsImage.Visibility = Visibility.Visible;
        appointmentsImageD.Visibility = Visibility.Visible;
        appointmentsImage.Visibility = Visibility.Collapsed;

        FilesHeaderTextBlock.Style = App.Current.Resources["SelectedTextBlockStyle"] as Style;
        AppointmentsHeaderTextBlock.Style = App.Current.Resources["UnselectedTextBlockStyle"] as Style;
    }

    private void AppointmentsHeader_Tapped(object sender, TappedRoutedEventArgs e)
    {
        ParentPage.AddFileButton.Visibility = Visibility.Collapsed;
        ParentPage.SortFilesButton.Visibility = Visibility.Collapsed;
        ParentPage.FilterFilesButton.Visibility = Visibility.Collapsed;
        ParentPage.SearchAutoSuggestBox.Visibility = Visibility.Collapsed;
        ParentPage.AddAppointmentButton.Visibility = Visibility.Visible;
        ParentPage.SortAppintmentsButton.Visibility = Visibility.Visible;
        ParentPage.FilterAppointmentsButton.Visibility = Visibility.Visible;

        docsImageD.Visibility = Visibility.Visible;
        docsImage.Visibility = Visibility.Collapsed;
        appointmentsImageD.Visibility = Visibility.Collapsed;
        appointmentsImage.Visibility = Visibility.Visible;

        AppointmentsHeaderTextBlock.Style = App.Current.Resources["SelectedTextBlockStyle"] as Style;
        FilesHeaderTextBlock.Style = App.Current.Resources["UnselectedTextBlockStyle"] as Style;
    }

    public void UpdateFilesDisplayVisibility(Models.Patient patient)
    {
        if (patient.files.Count == 0)
        {
            if (noFilesTextBlock.Visibility == Visibility.Collapsed)
                noFilesTextBlock.Visibility = Visibility.Visible;
            if (filesGrid.Visibility == Visibility.Visible)
                filesGrid.Visibility = Visibility.Collapsed;
        }
        else
        {
            if (noFilesTextBlock.Visibility == Visibility.Visible)
                noFilesTextBlock.Visibility = Visibility.Collapsed;
            if (filesGrid.Visibility == Visibility.Collapsed)
                filesGrid.Visibility = Visibility.Visible;
        }
    }

    public void UpdateAppointmentsDisplayVisibility(Models.Patient patient)
    {
        if (patient.appointments.Count == 0)
        {
            if (noAppointmentsTextBlock.Visibility == Visibility.Collapsed)
                noAppointmentsTextBlock.Visibility = Visibility.Visible;
            if (appointmentsGrid.Visibility == Visibility.Visible)
                appointmentsGrid.Visibility = Visibility.Collapsed;
        }
        else
        {
            if (noAppointmentsTextBlock.Visibility == Visibility.Visible)
                noAppointmentsTextBlock.Visibility = Visibility.Collapsed;
            if (appointmentsGrid.Visibility == Visibility.Collapsed)
                appointmentsGrid.Visibility = Visibility.Visible;
        }
    }

    private void PatientFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        PatientFiles.DeselectRange(new ItemIndexRange(PatientFiles.SelectedIndex,1));
    }

    private void Button_PointerEntered(object sender, PointerRoutedEventArgs e)
    {
        if (sender is Button button)
        {
            button.Background = new SolidColorBrush(Color.FromArgb(128, 128, 128, 128));
        }
    }
}
