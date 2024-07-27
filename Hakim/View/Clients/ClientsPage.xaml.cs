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

            GetTheOrder();

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
            itemsRepeater.ItemsSource = viewModel.Patients;
        }
    }
}
