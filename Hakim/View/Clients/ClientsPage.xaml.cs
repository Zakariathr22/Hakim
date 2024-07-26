using Hakim.Model;
using Hakim.Service;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Hakim.View.Clients
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClientsPage : Page
    {
        public ClientsViewModel viewModel = new ClientsViewModel();
        public ClientsPage()
        {
            this.InitializeComponent();

            DataContext = viewModel;

            Patient patient = new Patient();
            patient.FirstName = "Prénom de Patient";
            patient.LastName = "Nom";
            patient.DateOfBirth = DateTime.Now.AddYears(-25);
            patient.Phone1 = "0999999999";
            patient.Phone1Owner = "Personnel";
            patient.Phone2 = "0999999999";
            patient.Phone2Owner = "Le Père";

            Patient patient2 = new Patient();
            patient2.LastName = "Nom";
            patient2.FirstName = "Prénom de Patient2";
            patient2.DateOfBirth = DateTime.Now.AddYears(-60);
            patient2.Phone1 = "0888888888";
            patient2.Phone1Owner = "Personnel";
            patient2.Phone2 = "0888888888";
            patient2.Phone2Owner = "La Famme";
            viewModel.Patients.Add(patient);
            viewModel.Patients.Add(patient2);
            viewModel.Patients.Add(patient);
            viewModel.Patients.Add(patient2);
            viewModel.Patients.Add(patient);
            viewModel.Patients.Add(patient2);
            viewModel.Patients.Add(patient);
            viewModel.Patients.Add(patient2);
            viewModel.Patients.Add(patient);
            viewModel.Patients.Add(patient2);
            viewModel.Patients.Add(patient);

            //var sortedPatients = new ObservableCollection<Patient>(viewModel.Patients.OrderBy(p => p.FirstName));
            //viewModel.Patients = sortedPatients;

            itemsRepeater.ItemsSource = viewModel.Patients;
        }

        private void AddPatientButton_Click(object sender, RoutedEventArgs e)
        {
            ShowEditNameDialog();
        }

        private async void ShowEditNameDialog()
        {
            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = Content.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = new TitleControl("Ajouter un patient", addPatientIcon);
            dialog.PrimaryButtonText = "Suivant";
            dialog.CloseButtonText = "Annuler";
            viewModel.NewPatient = new Patient();
            dialog.Content = new AddPatientPage(dialog,viewModel);
            dialog.RequestedTheme = ThemeSelectorService.GetTheme(App.mainWindow);
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Secondary)
            {
                viewModel.AddPatient(viewModel.NewPatient);
            }
        }
    }
}
