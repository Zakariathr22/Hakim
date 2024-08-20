using Hakim.Model;
using Hakim.Service;
using Hakim.View.Clients.EditPatient;
using Hakim.View.Controls;
using Hakim.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace Hakim.View.Clients
{
    public sealed partial class ClientsPage : Page
    {
        public ClientsViewModel viewModel = new ClientsViewModel();
        private bool IsSuggestionChosen = false;
        private ObservableCollection<Model.Patient> Patients = new ObservableCollection<Model.Patient>();
        public ClientsPage()
        {
            this.InitializeComponent();
            this.InitializeLocalization();

            DataContext = viewModel;

            GetTheOrder();

            itemsRepeater.ItemsSource = viewModel.Patients;
        }

        private void AddPatientButton_Click(object sender, RoutedEventArgs e)
        {
            ShowAddPatientDialog();
        }

        private async void ShowAddPatientDialog()
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = Content.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = new TitleControl(LanguageService.GetResourceValue("AddPatient"), addPatientIcon);
            dialog.PrimaryButtonText = LanguageService.GetResourceValue("Next"); ;
            dialog.CloseButtonText = LanguageService.GetResourceValue("Cancel"); ;
            viewModel.Patient = new Model.Patient();
            dialog.Content = new AddPatientPage(dialog,viewModel);
            dialog.RequestedTheme = ThemeSelectorService.GetTheme(App.mainWindow);
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Secondary)
            {
                viewModel.AddPatient(viewModel.Patient);
                itemsRepeater.ItemsSource = viewModel.Patients;
            }
        }

        private void OrderByDate_Click(object sender, RoutedEventArgs e)
        {
            if (OrderByDate.IsChecked == true)
            {
                if(CroissantOrder.IsChecked == true)
                {
                    viewModel.PatientsOrder = 1;
                }
                else if (DecroissantOrder.IsChecked == true)
                {
                    viewModel.PatientsOrder = 0;
                }
                OrderChanged();
            }
            OrderByDate.IsChecked = true;
            OrderByLastName.IsChecked = false;
            OrderByFirstName.IsChecked = false;
            OrderByAge.IsChecked = false;
        }

        private void OrderByLastName_Click(object sender, RoutedEventArgs e)
        {
            if (OrderByLastName.IsChecked == true)
            {
                if (CroissantOrder.IsChecked == true)
                {
                    viewModel.PatientsOrder = 2;
                }
                else if (DecroissantOrder.IsChecked == true)
                {
                    viewModel.PatientsOrder = 3;
                }
                OrderChanged();
            }
            OrderByDate.IsChecked = false;
            OrderByLastName.IsChecked = true;
            OrderByFirstName.IsChecked = false;
            OrderByAge.IsChecked = false;
        }

        private void OrderByFirstName_Click(object sender, RoutedEventArgs e)
        {
            if (OrderByFirstName.IsChecked == true)
            {
                if (CroissantOrder.IsChecked == true)
                {
                    viewModel.PatientsOrder = 4;
                }
                else if (DecroissantOrder.IsChecked == true)
                {
                    viewModel.PatientsOrder = 5;
                }
                OrderChanged();
            }
            OrderByDate.IsChecked = false;
            OrderByLastName.IsChecked = false;
            OrderByFirstName.IsChecked = true;
            OrderByAge.IsChecked = false;
        }

        private void OrderByAge_Click(object sender, RoutedEventArgs e)
        {
            if (OrderByAge.IsChecked == true)
            {
                if (CroissantOrder.IsChecked == true)
                {
                    viewModel.PatientsOrder = 6;
                }
                else if (DecroissantOrder.IsChecked == true)
                {
                    viewModel.PatientsOrder = 7;
                }
                OrderChanged();
            }
            OrderByDate.IsChecked = false;
            OrderByLastName.IsChecked = false;
            OrderByFirstName.IsChecked = false;
            OrderByAge.IsChecked = true;
        }

        private void CroissantOrder_Click(object sender, RoutedEventArgs e)
        {
            if(CroissantOrder.IsChecked == true)
            {
                if (OrderByDate.IsChecked == true)
                {
                    viewModel.PatientsOrder = 1;
                } 
                else if (OrderByLastName.IsChecked == true)
                {
                    viewModel.PatientsOrder = 2;
                }
                else if (OrderByFirstName.IsChecked == true)
                {
                    viewModel.PatientsOrder = 4;
                }
                else if (OrderByAge.IsChecked == true)
                {
                    viewModel.PatientsOrder = 6;
                }
                OrderChanged();
            }
            CroissantOrder.IsChecked = true;
            DecroissantOrder.IsChecked = false;
        }

        private void DecroissantOrder_Click(object sender, RoutedEventArgs e)
        {
            if (DecroissantOrder.IsChecked == true)
            {
                if (OrderByDate.IsChecked == true)
                {
                    viewModel.PatientsOrder = 0;
                }
                else if (OrderByLastName.IsChecked == true)
                {
                    viewModel.PatientsOrder = 3;
                }
                else if (OrderByFirstName.IsChecked == true)
                {
                    viewModel.PatientsOrder = 5;
                }
                else if (OrderByAge.IsChecked == true)
                {
                    viewModel.PatientsOrder = 7;
                }
                OrderChanged();
            }
            CroissantOrder.IsChecked = false;
            DecroissantOrder.IsChecked = true;
        }

        private void GetTheOrder()
        {
            if (viewModel.PatientsOrder == 0)
            {
                OrderByDate.IsChecked = true;
                OrderByLastName.IsChecked = false;
                OrderByFirstName.IsChecked = false;
                OrderByAge.IsChecked = false;

                CroissantOrder.IsChecked = false;
                DecroissantOrder.IsChecked = true;
            }
            else if (viewModel.PatientsOrder == 1)
            {
                OrderByDate.IsChecked = true;
                OrderByLastName.IsChecked = false;
                OrderByFirstName.IsChecked = false;
                OrderByAge.IsChecked = false;

                CroissantOrder.IsChecked = true;
                DecroissantOrder.IsChecked = false;
            }
            else if (viewModel.PatientsOrder == 2)
            {
                OrderByDate.IsChecked = false;
                OrderByLastName.IsChecked = true;
                OrderByFirstName.IsChecked = false;
                OrderByAge.IsChecked = false;

                CroissantOrder.IsChecked = true;
                DecroissantOrder.IsChecked = false;
            }
            else if (viewModel.PatientsOrder == 3)
            {
                OrderByDate.IsChecked = false;
                OrderByLastName.IsChecked = true;
                OrderByFirstName.IsChecked = false;
                OrderByAge.IsChecked = false;

                CroissantOrder.IsChecked = false;
                DecroissantOrder.IsChecked = true;
            }
            else if (viewModel.PatientsOrder == 4)
            {
                OrderByDate.IsChecked = false;
                OrderByLastName.IsChecked = false;
                OrderByFirstName.IsChecked = true;
                OrderByAge.IsChecked = false;

                CroissantOrder.IsChecked = true;
                DecroissantOrder.IsChecked = false;
            }
            else if (viewModel.PatientsOrder == 5)
            {
                OrderByDate.IsChecked = false;
                OrderByLastName.IsChecked = false;
                OrderByFirstName.IsChecked = true;
                OrderByAge.IsChecked = false;

                CroissantOrder.IsChecked = false;
                DecroissantOrder.IsChecked = true;
            }
            else if (viewModel.PatientsOrder == 6)
            {
                OrderByDate.IsChecked = false;
                OrderByLastName.IsChecked = false;
                OrderByFirstName.IsChecked = false;
                OrderByAge.IsChecked = true;

                CroissantOrder.IsChecked = true;
                DecroissantOrder.IsChecked = false;
            }
            else if (viewModel.PatientsOrder == 7)
            {
                OrderByDate.IsChecked = false;
                OrderByLastName.IsChecked = false;
                OrderByFirstName.IsChecked = false;
                OrderByAge.IsChecked = true;

                CroissantOrder.IsChecked = false;
                DecroissantOrder.IsChecked = true;
            }
        }

        private void OrderChanged()
        {
            viewModel.OrderChangedCommand.Execute(null);
            UpdatePatientSearchResults(SearchAutoSuggestBox);
        }

        private Dictionary<string, Model.Patient> _patientDictionary = new Dictionary<string, Model.Patient>();

        private void SearchAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                UpdatePatientSearchResults(sender);
            }
        }

        private void SearchAutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            IsSuggestionChosen = true;
            // Retrieve the chosen patient from the dictionary
            var chosenFullName = args.SelectedItem as string;
            if (chosenFullName != null && _patientDictionary.ContainsKey(chosenFullName))
            {
                Patients.Clear();
                Patients.Add(_patientDictionary[chosenFullName]);
                itemsRepeater.ItemsSource = Patients;
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

        public void UpdatePatientSearchResults(AutoSuggestBox autoSuggestBox)
        {
            if (autoSuggestBox.Text == "")
            {
                itemsRepeater.ItemsSource = viewModel.Patients;
            }
            else
            {
                // Clear the dictionary
                _patientDictionary.Clear();

                // Filter patient last names based on user input
                var filteredPatients = viewModel.Patients
                    .Where(patient => patient.FirstName.Contains(autoSuggestBox.Text, StringComparison.OrdinalIgnoreCase)
                                   || patient.LastName.Contains(autoSuggestBox.Text, StringComparison.OrdinalIgnoreCase)
                                   || patient.Phone1.Contains(autoSuggestBox.Text, StringComparison.OrdinalIgnoreCase)
                                   || patient.Phone2.Contains(autoSuggestBox.Text, StringComparison.OrdinalIgnoreCase)
                                   || patient.fullName.Contains(autoSuggestBox.Text, StringComparison.OrdinalIgnoreCase));

                // Populate the dictionary with full names as keys and patient objects as values
                foreach (var patient in filteredPatients)
                {
                    var fullName = $"({patient.id}) {patient.fullName}";
                    if (!_patientDictionary.ContainsKey(fullName))
                    {
                        _patientDictionary.Add(fullName, patient);
                    }
                }

                // Set the ItemsSource to the list of full names
                autoSuggestBox.ItemsSource = _patientDictionary.Keys.ToList();
                itemsRepeater.ItemsSource = _patientDictionary.Values.ToList();
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            SearchAutoSuggestBox.Text = "";
            viewModel.getAllPatientsCommand.Execute(null);
            UpdatePatientSearchResults(SearchAutoSuggestBox);
        }

        public async void ShowEditPatientDialog(Model.Patient patient)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = Content.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.SecondaryButtonText = LanguageService.GetResourceValue("Close");
            viewModel.Patient = new Model.Patient();
            dialog.Content = new EdidPatientPage(dialog, patient);
            dialog.RequestedTheme = ThemeSelectorService.GetTheme(App.mainWindow);
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Secondary)
            {
                viewModel.UpdatePatient(patient);
                UpdatePatientSearchResults(SearchAutoSuggestBox);
            }
        }

        public void InitializeLocalization()
        {
            AddPatientButton.Label = LanguageService.GetResourceValue("Add");
            ToolTipService.SetToolTip(AddPatientButton, LanguageService.GetResourceValue("AddPatient"));

            sortButton.Label = LanguageService.GetResourceValue("Sort");
            ToolTipService.SetToolTip(sortButton, LanguageService.GetResourceValue("SortPatients"));

            OrderByDate.Text = LanguageService.GetResourceValue("ByDateAdded");
            OrderByLastName.Text = LanguageService.GetResourceValue("ByLastname");
            OrderByFirstName.Text = LanguageService.GetResourceValue("ByFirstname");
            OrderByAge.Text = LanguageService.GetResourceValue("ByAge");

            CroissantOrder.Text = LanguageService.GetResourceValue("Ascending");
            DecroissantOrder.Text = LanguageService.GetResourceValue("Descending");

            refreshButton.Label = LanguageService.GetResourceValue("Refresh");
            ToolTipService.SetToolTip(refreshButton, LanguageService.GetResourceValue("Refresh"));

            SearchAutoSuggestBox.PlaceholderText = LanguageService.GetResourceValue("SearchForAPatient");
        }

    }
}
