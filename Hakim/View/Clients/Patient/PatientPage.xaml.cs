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
using Hakim.View.Clients.Patient.Appointments;
using Hakim.Converters;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Hakim.View.Clients
{
    public sealed partial class PatientPage : Page
    {
        public PatientViewModel viewModel = new PatientViewModel();
        ScrollViewer scrollView1 = new ScrollViewer();
        Grid mainPanel = new Grid();
        ScrollViewer scrollView2 = new ScrollViewer();
        Grid InternalPanel = new Grid();
        Expander expander = new Expander();
        private Dictionary<string, Model.File> _filesDictionary = new Dictionary<string, Model.File>();
        private bool IsSuggestionChosen = false;
        ObservableCollection<Model.File> Files = new ObservableCollection<Model.File>();

        PatientInfoEditorControl patientInfoEditor;
        PatientDetailsDisplayControl patientDetailsDisplay;
        public PatientRecordsControl patientRecords;

        public PatientPage()
        {
            this.InitializeComponent();
            Loaded += PatientPage_Loaded;
            viewModel.getNumberOfAppointmentsPerDayCommand.Execute(null);
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
            expander.Margin = new Thickness(0, 24, 0, 8);
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
                expander.Margin = new Thickness(0, 24, 0, 8);
                patientInfoEditor.Margin = new Thickness(16, 16, 0, 16);
                patientDetailsDisplay.Margin = new Thickness(0);
                patientRecords.Margin = new Thickness(0);
            } 
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Model.Patient)
            {
                viewModel.SelectedPatient = e.Parameter as Model.Patient;
            }
            base.OnNavigatedTo(e);
        }

        private void SetOrder(int order)
        {
            // Reset all checkboxes
            OrderByDate.IsChecked = false;
            OrderByTitle.IsChecked = false;
            OrderByType.IsChecked = false;
            CroissantOrder.IsChecked = false;
            DecroissantOrder.IsChecked = false;

            // Set the appropriate checkboxes based on the order value
            switch (order)
            {
                case 0:
                    OrderByDate.IsChecked = true;
                    DecroissantOrder.IsChecked = true;
                    break;
                case 1:
                    OrderByDate.IsChecked = true;
                    CroissantOrder.IsChecked = true;
                    break;
                case 2:
                    OrderByTitle.IsChecked = true;
                    CroissantOrder.IsChecked = true;
                    break;
                case 3:
                    OrderByTitle.IsChecked = true;
                    DecroissantOrder.IsChecked = true;
                    break;
                case 4:
                    OrderByType.IsChecked = true;
                    CroissantOrder.IsChecked = true;
                    break;
                case 5:
                    OrderByType.IsChecked = true;
                    DecroissantOrder.IsChecked = true;
                    break;
            }
        }

        private void SetFilter(int filter)
        {
            // Reset all checkboxes
            AllFiles.IsChecked = false;
            Consultations.IsChecked = false;
            Xray.IsChecked = false;
            TXray.IsChecked = false;
            SurgeryProtocol.IsChecked = false;

            // Set the appropriate checkbox based on the filter value
            switch (filter)
            {
                case 1:
                    Consultations.IsChecked = true;
                    break;
                case 2:
                    Xray.IsChecked = true;
                    break;
                case 3:
                    TXray.IsChecked = true;
                    break;
                case 4:
                    SurgeryProtocol.IsChecked = true;
                    break;
                default:
                    AllFiles.IsChecked = true;
                    break;
            }
        }

        private void OrderByDate_Click(object sender, RoutedEventArgs e)
        {
            viewModel.FilesOrder = CroissantOrder.IsChecked == true ? 1 : 0;
            FilesOrderOrFilterChanged();
            SetOrder(viewModel.FilesOrder);
        }

        private void OrderByTitle_Click(object sender, RoutedEventArgs e)
        {
            viewModel.FilesOrder = CroissantOrder.IsChecked == true ? 2 : 3;
            FilesOrderOrFilterChanged();
            SetOrder(viewModel.FilesOrder);
        }

        private void OrderByType_Click(object sender, RoutedEventArgs e)
        {
            viewModel.FilesOrder = CroissantOrder.IsChecked == true ? 4 : 5;
            FilesOrderOrFilterChanged();
            SetOrder(viewModel.FilesOrder);
        }

        private void CroissantOrder_Click(object sender, RoutedEventArgs e)
        {
            if (CroissantOrder.IsChecked == true)
            {
                viewModel.FilesOrder = OrderByDate.IsChecked == true ? 1 :
                                  OrderByTitle.IsChecked == true ? 2 :
                                  OrderByType.IsChecked == true ? 4 : viewModel.FilesOrder;
                FilesOrderOrFilterChanged();
            }
            SetOrder(viewModel.FilesOrder);
        }

        private void DecroissantOrder_Click(object sender, RoutedEventArgs e)
        {
            if (DecroissantOrder.IsChecked == true)
            {
                viewModel.FilesOrder = OrderByDate.IsChecked == true ? 0 :
                                  OrderByTitle.IsChecked == true ? 3 :
                                  OrderByType.IsChecked == true ? 5 : viewModel.FilesOrder;
                FilesOrderOrFilterChanged();
            }
            SetOrder(viewModel.FilesOrder);
        }
        
        private void AllFiles_Click(object sender, RoutedEventArgs e)
        {
            viewModel.FilesFilter = 0; // AllFiles
            FilterFilesButton.IsChecked = false;
            SetFilter(viewModel.FilesFilter);
            Consultations.IsChecked = true;
            Xray.IsChecked = true;
            TXray.IsChecked = true;
            SurgeryProtocol.IsChecked = true;
            FilesOrderOrFilterChanged();

            AddConsultationItem.Visibility = Visibility.Visible;
            AddXRayItem.Visibility = Visibility.Visible;
            TelemetryXRayItem.Visibility = Visibility.Visible;
            SurgeryProtocolItem.Visibility = Visibility.Visible;
        }

        private void Consultations_Click(object sender, RoutedEventArgs e)
        {
            viewModel.FilesFilter = 1; // Consultation
            FilterFilesButton.IsChecked = true;
            SetFilter(viewModel.FilesFilter);
            FilesOrderOrFilterChanged();

            AddConsultationItem.Visibility = Visibility.Visible;
            AddXRayItem.Visibility = Visibility.Collapsed;
            TelemetryXRayItem.Visibility = Visibility.Collapsed;
            SurgeryProtocolItem.Visibility = Visibility.Collapsed;
        }

        private void Xray_Click(object sender, RoutedEventArgs e)
        {
            viewModel.FilesFilter = 2; // Xray
            FilterFilesButton.IsChecked = true;
            SetFilter(viewModel.FilesFilter);
            FilesOrderOrFilterChanged();

            AddConsultationItem.Visibility = Visibility.Collapsed;
            AddXRayItem.Visibility = Visibility.Visible;
            TelemetryXRayItem.Visibility = Visibility.Collapsed;
            SurgeryProtocolItem.Visibility = Visibility.Collapsed;
        }

        private void TXray_Click(object sender, RoutedEventArgs e)
        {
            viewModel.FilesFilter = 3; // TXray
            FilterFilesButton.IsChecked = true;
            SetFilter(viewModel.FilesFilter);
            FilesOrderOrFilterChanged();

            AddConsultationItem.Visibility = Visibility.Collapsed;
            AddXRayItem.Visibility = Visibility.Collapsed;
            TelemetryXRayItem.Visibility = Visibility.Visible;
            SurgeryProtocolItem.Visibility = Visibility.Collapsed;
        }

        private void SurgeryProtocol_Click(object sender, RoutedEventArgs e)
        {
            viewModel.FilesFilter = 4; // SurgeryProtocol
            FilterFilesButton.IsChecked = true;
            SetFilter(viewModel.FilesFilter);
            FilesOrderOrFilterChanged();

            AddConsultationItem.Visibility = Visibility.Collapsed;
            AddXRayItem.Visibility = Visibility.Collapsed;
            TelemetryXRayItem.Visibility = Visibility.Collapsed;
            SurgeryProtocolItem.Visibility = Visibility.Visible;
        }

        private void FilterFilesButton_Click(object sender, RoutedEventArgs e)
        {
            FilterFilesButton.ContextFlyout.ShowAt(FilterFilesButton,
                new FlyoutShowOptions { Placement = FlyoutPlacementMode.Bottom });
            if (AllFiles.IsChecked)
                FilterFilesButton.IsChecked = false;
            else FilterFilesButton.IsChecked = true;
        }

        private void FilesOrderOrFilterChanged()
        {
            viewModel.FilesOrderOrFilterChangedCommand.Execute(null);
            patientRecords.PatientFiles.ItemsSource = viewModel.SelectedPatient.files;
            patientRecords.UpdateFilesDisplayVisibility(viewModel.SelectedPatient);
        }

        private void CroissantAppointmentOrder_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AppointmentsOrder = 0;
            AppointmentsOrderOrFilterChanged();

            CroissantAppointmentOrder.IsChecked = true;
            DecroissantAppointmentOrder.IsChecked = false;
        }

        private void DecroissantAppointmentOrder_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AppointmentsOrder = 1;
            AppointmentsOrderOrFilterChanged();

            CroissantAppointmentOrder.IsChecked = false;
            DecroissantAppointmentOrder.IsChecked = true;
        }

        private void AllAppointments_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AppointmentsFilter = 0;
            AppointmentsOrderOrFilterChanged();
            FilterAppointmentsButton.IsChecked = false;

            AllAppointments.IsChecked = true;
            PresntAndFutureAppointments.IsChecked = true;
            PastAppointments.IsChecked = true;
        }

        private void PresntAndFutureAppointments_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AppointmentsFilter = 1;
            AppointmentsOrderOrFilterChanged();
            FilterAppointmentsButton.IsChecked = true;

            AllAppointments.IsChecked = false;
            PresntAndFutureAppointments.IsChecked = true;
            PastAppointments.IsChecked = false;
        }

        private void PastAppointments_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AppointmentsFilter = 2;
            AppointmentsOrderOrFilterChanged();
            FilterAppointmentsButton.IsChecked = true;

            AllAppointments.IsChecked = false;
            PresntAndFutureAppointments.IsChecked = false;
            PastAppointments.IsChecked = true;
        }

        private void AppointmentsOrderOrFilterChanged()
        {
            viewModel.AppointmentsOrderOrFilterChangedCommand.Execute(null);
            patientRecords.PatientAppointments.ItemsSource = viewModel.SelectedPatient.appointments;
            patientRecords.UpdateAppointmentsDisplayVisibility(viewModel.SelectedPatient);
        }

        public async void ShowEditPatientDialog(Model.Patient patient)
        {
            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of PatientDetailsDisplay ContentDialog running in PatientDetailsDisplay Desktop app
            dialog.XamlRoot = Content.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.SecondaryButtonText = "Fermer";
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
            viewModel.XRay = new XRay { Patient = viewModel.SelectedPatient, Title = $"Radiographie {viewModel.SelectedPatient.files.Count(file => file.Type == 1) + 1} - {viewModel.SelectedPatient.fullName}", CreationDate = DateTime.Now, Xray_date = DateTime.Now };
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
            viewModel.SpineTelemetryXRay = new SpineTelemetryXRay { Patient = viewModel.SelectedPatient, Title = $"Radiographie télémétrie {viewModel.SelectedPatient.files.Count(file => file.Type == 2) + 1} du colonne vertébrale - {viewModel.SelectedPatient.fullName}", CreationDate = DateTime.Now, Xray_date = DateTime.Now };
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
            dialog.PrimaryButtonText = "Sauvgarder"; //AccentButtonStyle
            dialog.PrimaryButtonStyle = Application.Current.Resources["AccentButtonStyle"] as Style;
            viewModel.SurgeryProtocol = new SurgeryProtocol { Patient = viewModel.SelectedPatient, Title = $"Protocol opératoire {viewModel.SelectedPatient.files.Count(file => file.Type == 3) + 1} - {viewModel.SelectedPatient.fullName}", CreationDate = DateTime.Now, Surgeon = App.user.profitionalName };
            dialog.Content = new AddSurgeryProtocolPage(dialog, viewModel.SurgeryProtocol);
            dialog.RequestedTheme = ThemeSelectorService.GetTheme(App.mainWindow);
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                viewModel.AddSurgeryProtocol();
                patientRecords.UpdateFilesDisplayVisibility(viewModel.SelectedPatient);
                patientRecords.PatientFiles.ItemsSource = viewModel.SelectedPatient.files;
            }
        }

        private void AddAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            ShowAddAppointmentDialog();
        }

        public async void ShowAddAppointmentDialog()
        {
            ContentDialog dialog = new ContentDialog();
            dialog.Title = new TitleControl("Ajouter un rendez-vous", new FontIcon { Glyph = "\uEC92" });
            dialog.XamlRoot = Content.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.CloseButtonText = "Annuler";
            dialog.PrimaryButtonText = "Sauvgarder"; //AccentButtonStyle
            dialog.PrimaryButtonStyle = Application.Current.Resources["AccentButtonStyle"] as Style;
            viewModel.Appointment = new Appointment { Patient = viewModel.SelectedPatient, AppointmentDate = DateTime.Now.Date, AppointmentTime = TimeSpan.FromHours(8.5), Purpose = "Routine Checkup" };
            dialog.Content = new AddAppointmentPage(dialog, viewModel.Appointment, viewModel.AppointmentCounts);
            dialog.RequestedTheme = ThemeSelectorService.GetTheme(App.mainWindow);
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                viewModel.AddAppointment();
                patientRecords.UpdateAppointmentsDisplayVisibility(viewModel.SelectedPatient);
                patientRecords.PatientAppointments.ItemsSource = viewModel.SelectedPatient.appointments;
            }
        }

        private void SearchAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                UpdatePatientSearchResults(sender);
            }
        }

        public void UpdatePatientSearchResults(AutoSuggestBox autoSuggestBox)
        {
            if (autoSuggestBox.Text == "")
            {
               patientRecords.PatientFiles.ItemsSource = viewModel.SelectedPatient.files;
            }
            else
            {
                _filesDictionary.Clear();

                var filteredFiles = viewModel.SelectedPatient.files
                    .Where(file => file.Title.Contains(autoSuggestBox.Text, StringComparison.OrdinalIgnoreCase)
                                   || file.CreationDate.ToShortDateString().Contains(autoSuggestBox.Text, StringComparison.OrdinalIgnoreCase));

                foreach (var file in filteredFiles)
                {
                    var fileDetails = $"{file.Title}";
                    if (!_filesDictionary.ContainsKey(fileDetails))
                    {
                        _filesDictionary.Add(fileDetails, file);
                    }
                }

                autoSuggestBox.ItemsSource = _filesDictionary.Keys.ToList();
                patientRecords.PatientFiles.ItemsSource = _filesDictionary.Values.ToList();
            }
        }

        public void UpdateAppointmentsList()
        {
            patientRecords.PatientAppointments.ItemsSource = viewModel.SelectedPatient.appointments;
        }

        private void SearchAutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            IsSuggestionChosen = true;
            var chosenFullName = args.SelectedItem as string;
            if (chosenFullName != null && _filesDictionary.ContainsKey(chosenFullName))
            {
                Files.Clear();
                Files.Add(_filesDictionary[chosenFullName]);
                patientRecords.PatientFiles.ItemsSource = Files;
            }
        }

        private void SearchAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (IsSuggestionChosen == false)
            {
                Console.WriteLine();
            }
            else
                IsSuggestionChosen = false;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.getFilesByPatientCommand.Execute(null);
            UpdatePatientSearchResults(SearchAutoSuggestBox);

            viewModel.getAppointmentsByPatientCommand.Execute(null);
            patientRecords.PatientAppointments.ItemsSource = viewModel.SelectedPatient.appointments;
            patientRecords.UpdateAppointmentsDisplayVisibility(viewModel.SelectedPatient);
        }

        private void FilterAppointmentsButton_Click(object sender, RoutedEventArgs e)
        {
            FilterAppointmentsButton.ContextFlyout.ShowAt(FilterAppointmentsButton,
            new FlyoutShowOptions { Placement = FlyoutPlacementMode.Bottom });
            if (AllAppointments.IsChecked)
                FilterAppointmentsButton.IsChecked = false;
            else FilterAppointmentsButton.IsChecked = true;
        }
    }
}
