using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using Windows.UI;
using Hakim.View.Clients.AddPatient;
using Hakim.View.Clients.Patient;
using Hakim.ViewModel;
using System.Collections.ObjectModel;
using Hakim.Service;
using Hakim.View.Clients.EditPatient;
using Hakim.View.Controls;
using Hakim.View.Clients.Patient.Consultations;
using Hakim.View.Clients.Patient.XRay_s;
using Hakim.View.Clients.Patient.SurgeryProtocols;
using Hakim.Model;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Hakim.View.Clients
{
    public sealed partial class PatientPage : Page
    {
        ClientsViewModel viewModel;
        ScrollViewer scrollView1 = new ScrollViewer();
        Grid mainPanel = new Grid();
        ScrollViewer scrollView2 = new ScrollViewer();
        Grid InternalPanel = new Grid();
        Expander expander = new Expander();

        PatientInfoEditorControl patientInfoEditor;
        PatientDetailsDisplayControl patientDetailsDisplay;
        PatientRecordsControl patientRecords;

        public PatientPage()
        {
            this.InitializeComponent();
            Loaded += PatientPage_Loaded;
        }

        private void PatientPage_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = viewModel;

            viewModel.getFilesByPatientCommand.Execute(null);

            viewModel.getAppointmentsByPatientCommand.Execute(null);

            patientInfoEditor = new PatientInfoEditorControl(viewModel.SelectedPatient);
            patientDetailsDisplay = new PatientDetailsDisplayControl(viewModel.SelectedPatient);
            patientRecords = new PatientRecordsControl(viewModel.SelectedPatient);

            Grid.SetRow(scrollView1, 1);
            Grid.SetColumn(scrollView1, 0);
            PagePanel.Children.Add(scrollView1);

            mainPanel.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Second row takes 2/3 of available space

            mainPanel.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            mainPanel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // First column takes 1/3 of available space

            scrollView1.Content = mainPanel;

            Grid.SetRow(expander, 0);
            Grid.SetColumn(expander, 0);
            mainPanel.Children.Add(expander);

            expander.Header = patientInfoEditor;
            expander.Content = patientDetailsDisplay;
            expander.HorizontalAlignment = HorizontalAlignment.Stretch;

            patientDetailsDisplay.HorizontalAlignment = HorizontalAlignment.Stretch;
            patientInfoEditor.HorizontalAlignment = HorizontalAlignment.Stretch;
            patientRecords.HorizontalAlignment = HorizontalAlignment.Stretch;

            patientRecords.VerticalAlignment = VerticalAlignment.Top;

            Grid.SetRow(patientRecords, 1);
            Grid.SetColumn(patientRecords, 0);
            mainPanel.Children.Add(patientRecords);

            InternalPanel.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            InternalPanel.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            scrollView1.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            scrollView2.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            scrollView1.Padding = new Thickness(24, 0, 24, 0);
            expander.Margin = new Thickness(0, 24, 0, 16);
            patientInfoEditor.Margin = new Thickness(16, 16, 0, 16);
            patientDetailsDisplay.Margin = new Thickness(0);
            patientRecords.Margin = new Thickness(0);

            Page_SizeChanged(null, null);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.ActualWidth >= 832 && mainPanel.RowDefinitions.Count == 2)
            {
                mainPanel.RowDefinitions.Clear();
                mainPanel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                mainPanel.ColumnDefinitions[0].Width = new GridLength(450);
                mainPanel.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                mainPanel.Children.Clear();
                expander.Header = null;
                expander.Content = null;
                PagePanel.Children.RemoveAt(1);
                scrollView1.Content = null;

                Grid.SetRow(mainPanel, 1);
                PagePanel.Children.Add(mainPanel);

                Grid.SetRow(scrollView1, 0);
                Grid.SetColumn(scrollView1, 0);
                mainPanel.Children.Add(scrollView1);
                Grid.SetRow(scrollView2, 0);
                Grid.SetColumn(scrollView2, 1);
                mainPanel.Children.Add(scrollView2);

                scrollView1.Content = InternalPanel;
                Grid.SetRow(patientInfoEditor, 0);
                Grid.SetColumn(patientInfoEditor, 0);
                InternalPanel.Children.Add(patientInfoEditor);
                Grid.SetRow(patientDetailsDisplay, 1);
                Grid.SetColumn(patientDetailsDisplay, 0);
                InternalPanel.Children.Add(patientDetailsDisplay);
                scrollView2.Content = patientRecords;

                scrollView1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                scrollView1.Padding = new Thickness(12, 0, 16, 0);
                scrollView2.Padding = new Thickness(12, 0, 16, 0);
                patientInfoEditor.Margin = new Thickness(16, 24, 0, 0);
                patientDetailsDisplay.Margin = new Thickness(0, 16, 0, 24);
                patientRecords.Margin = new Thickness(0, 12, 0, 0);
            }
            else if (this.ActualWidth < 832 && mainPanel.ColumnDefinitions.Count == 2)
            {
                scrollView1.ChangeView(null, 0, null, true);
                mainPanel.ColumnDefinitions.RemoveAt(1);
                mainPanel.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                mainPanel.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                mainPanel.RowDefinitions[0].Height = GridLength.Auto;
                Grid.SetRow(patientRecords, 1);
                Grid.SetColumn(patientRecords, 0);

                InternalPanel.Children.Clear();
                mainPanel.Children.Clear();
                PagePanel.Children.RemoveAt(1);
                scrollView1.Content = null;
                scrollView2.Content = null;

                Grid.SetColumn(scrollView1, 0);
                Grid.SetRow(scrollView1, 1);
                PagePanel.Children.Add(scrollView1);

                scrollView1.Content = mainPanel;

                Grid.SetColumn(patientRecords, 0);
                Grid.SetRow(patientRecords, 1);
                mainPanel.Children.Add(patientRecords);

                Grid.SetColumn(expander, 0);
                Grid.SetRow(expander, 0);
                mainPanel.Children.Add(expander);

                expander.Header = patientInfoEditor;
                expander.Content = patientDetailsDisplay;

                scrollView1.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                scrollView1.Padding = new Thickness(24, 0, 24, 0);
                expander.Margin = new Thickness(0, 24, 0, 24);
                patientInfoEditor.Margin = new Thickness(16, 16, 0, 16);
                patientDetailsDisplay.Margin = new Thickness(0);
                patientRecords.Margin = new Thickness(0);
            } 
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is ClientsViewModel)
            {
                viewModel = e.Parameter as ClientsViewModel;
            }
            base.OnNavigatedTo(e);
        }

        private void OrderByDate_Click(object sender, RoutedEventArgs e)
        {
            OrderByDate.IsChecked = true;
            OrderByTitle.IsChecked = false;
            OrderByType.IsChecked = false;
        }

        private void OrderByTitle_Click(object sender, RoutedEventArgs e)
        {
            OrderByDate.IsChecked = false;
            OrderByTitle.IsChecked = true;
            OrderByType.IsChecked = false;
        }

        private void OrderByType_Click(object sender, RoutedEventArgs e)
        {
            OrderByDate.IsChecked = false;
            OrderByTitle.IsChecked = false;
            OrderByType.IsChecked = true;
        }

        private void CroissantOrder_Click(object sender, RoutedEventArgs e)
        {
            CroissantOrder.IsChecked = true;
            DecroissantOrder.IsChecked = false;
        }

        private void DecroissantOrder_Click(object sender, RoutedEventArgs e)
        {
            CroissantOrder.IsChecked = false;
            DecroissantOrder.IsChecked = true;
        }

        private void CroissantAppointmentOrder_Click(object sender, RoutedEventArgs e)
        {
            CroissantAppointmentOrder.IsChecked = true;
            DecroissantAppointmentOrder.IsChecked = false;
        }

        private void DecroissantAppointmentOrder_Click(object sender, RoutedEventArgs e)
        {
            CroissantAppointmentOrder.IsChecked = false;
            DecroissantAppointmentOrder.IsChecked = true;
        }

        private void AllFiles_Click(object sender, RoutedEventArgs e)
        {
            AllFiles.IsChecked = true;
            Consultations.IsChecked = true;
            Xray.IsChecked = true;
            TXray.IsChecked = true;
            SurgeryProtocol.IsChecked = true;
        }

        private void Consultations_Click(object sender, RoutedEventArgs e)
        {
            AllFiles.IsChecked = false;
            Consultations.IsChecked = true;
            Xray.IsChecked = false;
            TXray.IsChecked = false;
            SurgeryProtocol.IsChecked = false;
        }

        private void Xray_Click(object sender, RoutedEventArgs e)
        {
            AllFiles.IsChecked = false;
            Consultations.IsChecked = false;
            Xray.IsChecked = true;
            TXray.IsChecked = false;
            SurgeryProtocol.IsChecked = false;
        }

        private void TXray_Click(object sender, RoutedEventArgs e)
        {
            AllFiles.IsChecked = false;
            Consultations.IsChecked = false;
            Xray.IsChecked = false;
            TXray.IsChecked = true;
            SurgeryProtocol.IsChecked = false;
        }

        private void SurgeryProtocol_Click(object sender, RoutedEventArgs e)
        {
            AllFiles.IsChecked = false;
            Consultations.IsChecked = false;
            Xray.IsChecked = false;
            TXray.IsChecked = false;
            SurgeryProtocol.IsChecked = true;
        }

        private void AllAppointments_Click(object sender, RoutedEventArgs e)
        {
            AllAppointments.IsChecked = true;
            PresntAndFutureAppointments.IsChecked = true;
            PastAppointments.IsChecked = true;
        }

        private void PresntAndFutureAppointments_Click(object sender, RoutedEventArgs e)
        {
            AllAppointments.IsChecked = false;
            PresntAndFutureAppointments.IsChecked = true;
            PastAppointments.IsChecked = false;
        }

        private void PastAppointments_Click(object sender, RoutedEventArgs e)
        {
            AllAppointments.IsChecked = false;
            PresntAndFutureAppointments.IsChecked = false;
            PastAppointments.IsChecked = true;
        }

        public async void ShowEditPatientDialog(Model.Patient patient)
        {
            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of PatientDetailsDisplay ContentDialog running in PatientDetailsDisplay Desktop app
            dialog.XamlRoot = Content.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.SecondaryButtonText = "Fermer";
            viewModel.Patient = new Model.Patient();
            dialog.Content = new EdidPatientPage(dialog, patient);
            dialog.RequestedTheme = ThemeSelectorService.GetTheme(App.mainWindow);
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Secondary)
            {
                viewModel.UpdatePatient(patient);
                //UpdatePatientSearchResults(SearchAutoSuggestBox);
            }
        }

        private void AddConsultationItem_Click(object sender, RoutedEventArgs e)
        {
            ShowAddConsultationDialog();
        }

        public async void ShowAddConsultationDialog()
        {
            ContentDialog dialog = new ContentDialog();
            dialog.Title = new TitleControl("Ajouter une consultation", new FontIcon { Glyph = "\uED0E" });
            dialog.XamlRoot = Content.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.CloseButtonText = "Annuler";
            dialog.PrimaryButtonText = "Sauvgarder"; //AccentButtonStyle
            dialog.PrimaryButtonStyle = Application.Current.Resources["AccentButtonStyle"] as Style;
            viewModel.Consultation = new MedicalConsultation { Patient = viewModel.SelectedPatient,Title = $"Consultation {viewModel.SelectedPatient.files.Count(file => file.Type == 0) + 1} - {viewModel.SelectedPatient.fullName}", CreationDate = DateTime.Now};
            dialog.Content = new AddEditConsultaionFilePage(dialog,viewModel.Consultation);
            dialog.RequestedTheme = ThemeSelectorService.GetTheme(App.mainWindow);
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                viewModel.AddMedicalConsultation();
                patientRecords.UpdateFilesDisplayVisibility(viewModel.SelectedPatient);
                patientRecords.PatientFiles.ItemsSource = viewModel.SelectedPatient.files;
            }
        }

        private void AddXRayItem_Click(object sender, RoutedEventArgs e)
        {
            ShowAddXRayDialog();
        }

        public async void ShowAddXRayDialog()
        {
            ContentDialog dialog = new ContentDialog();
            dialog.Title = new TitleControl("Ajouter une radiographie", new FontIcon { Glyph = "\uED0E" });
            dialog.XamlRoot = Content.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.CloseButtonText = "Annuler";
            dialog.PrimaryButtonText = "Sauvgarder"; //AccentButtonStyle
            dialog.PrimaryButtonStyle = Application.Current.Resources["AccentButtonStyle"] as Style;
            viewModel.XRay = new XRay { Patient = viewModel.SelectedPatient, Title = $"Radiographie {viewModel.SelectedPatient.files.Count(file => file.Type == 1) + 1} du ....... - {viewModel.SelectedPatient.fullName}", CreationDate = DateTime.Now, Xray_date = DateTime.Now };
            dialog.Content = new AddXRayPage(dialog, viewModel.XRay);
            dialog.RequestedTheme = ThemeSelectorService.GetTheme(App.mainWindow);
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                viewModel.AddXRay();
                patientRecords.UpdateFilesDisplayVisibility(viewModel.SelectedPatient);
                patientRecords.PatientFiles.ItemsSource = viewModel.SelectedPatient.files;
            }
        }

        private void TelemetryXRayItem_Click(object sender, RoutedEventArgs e)
        {
            ShowAddTelemetryXRayDialog();
        }

        public async void ShowAddTelemetryXRayDialog()
        {
            ContentDialog dialog = new ContentDialog();
            dialog.Title = new TitleControl("Ajouter une radiographie télémétrie", new FontIcon { Glyph = "\uED0E" });
            dialog.XamlRoot = Content.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.CloseButtonText = "Annuler";
            dialog.PrimaryButtonText = "Sauvgarder"; //AccentButtonStyle
            dialog.PrimaryButtonStyle = Application.Current.Resources["AccentButtonStyle"] as Style;
            viewModel.SpineTelemetryXRay = new SpineTelemetryXRay { Patient = viewModel.SelectedPatient, Title = $"Radiographie Telemetrie {viewModel.SelectedPatient.files.Count(file => file.Type == 1) + 1} du colone vertibrale - {viewModel.SelectedPatient.fullName}", CreationDate = DateTime.Now, Xray_date = DateTime.Now };
            dialog.Content = new AddTelemetryXRayPage(dialog, viewModel.SpineTelemetryXRay);
            dialog.RequestedTheme = ThemeSelectorService.GetTheme(App.mainWindow);
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                viewModel.AddSpineTelemetryXRay();
                patientRecords.UpdateFilesDisplayVisibility(viewModel.SelectedPatient);
                patientRecords.PatientFiles.ItemsSource = viewModel.SelectedPatient.files;
            }
        }

        private void SurgeryProtocolItem_Click(object sender, RoutedEventArgs e)
        {
            ShowAddSurgeryProtocolDialog();
        }

        public async void ShowAddSurgeryProtocolDialog()
        {
            ContentDialog dialog = new ContentDialog();
            dialog.Title = new TitleControl("Ajouter un protocol opératoire", new FontIcon { Glyph = "\uED0E" });
            dialog.XamlRoot = Content.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.CloseButtonText = "Annuler";
            dialog.IsPrimaryButtonEnabled = false;
            dialog.PrimaryButtonText = "Sauvgarder"; //AccentButtonStyle
            dialog.PrimaryButtonStyle = Application.Current.Resources["AccentButtonStyle"] as Style;
            //viewModel.NewPatient = new Model.Patient();
            dialog.Content = new AddSurgeryProtocolPage(dialog);
            dialog.RequestedTheme = ThemeSelectorService.GetTheme(App.mainWindow);
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Secondary)
            {

            }
        }
    }
}
