using Hakim.Model;
using Hakim.Service;
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
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static System.Net.Mime.MediaTypeNames;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Hakim.View.Clients
{
    public sealed partial class PatientCardControl : UserControl
    {
        private ClientsPage ParentPage;
        public PatientCardControl()
        {
            this.InitializeComponent();
            this.InitializeLocation();
            Loaded += PatientCardControl_Loaded;
        }

        private void PatientCardControl_Loaded(object sender, RoutedEventArgs e)
        {
            ParentPage = VisualTreeExtensionsService.FindParent<ClientsPage>(this);
            if (ParentPage != null)
            {
                // You now have access to the parent page
                Debug.WriteLine("Parent Page found: " + ParentPage.GetType().Name);
            }
        }

        private void ShowMenu()
        {
            FlyoutShowOptions myOption = new FlyoutShowOptions();
            myOption.ShowMode = FlyoutShowMode.TransientWithDismissOnPointerMoveAway;
            CommandBarFlyout.ShowAt(this, myOption);
        }

        private void CustomButton_ContextRequested(UIElement sender, ContextRequestedEventArgs args)
        {
            ShowMenu();
        }

        private void deletePatientButton_Click(object sender, RoutedEventArgs e)
        {
            CommandBarFlyout.Hide();
            if (this.DataContext is Model.Patient patient)
            {
                // Now you have access to the associated Patient object
                ParentPage.viewModel.DeletePatientById(patient.id);
                ParentPage.UpdatePatientSearchResults(ParentPage.SearchAutoSuggestBox);
            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            CommandBarFlyout.Hide();
            if (this.DataContext is Model.Patient patient)
            {
                // Now you have access to the associated Patient object
                ParentPage.ShowEditPatientDialog(patient);
            }
        }

        private void CustomButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is Model.Patient patient)
            {
                ParentPage.viewModel.SelectedPatient = patient;
                App.mainWindow.contentFrame.Navigate(typeof(PatientPage), ParentPage.viewModel.SelectedPatient);
            }
        }

        private void SetCardColor()
        {
            if (this.DataContext is Model.Patient patient)
            {
                // Check the patient's ID and apply the corresponding styles.
                if (patient.id % 4 == 0)
                {
                    // Apply Green Background and Green Foreground styles
                    var greenBackgroundStyle = (Style)App.Current.Resources["GreenBackgroundStyle"];
                    var greenForegroundStyle = (Style)App.Current.Resources["GreenForegroundStyle"];

                    personPicture.Style = greenBackgroundStyle;
                    fullNameAndAgeTextBlock.Style = greenForegroundStyle;
                }
                else if (patient.id % 3 == 0)
                {
                    // Apply Amber Background and Amber Foreground styles
                    var amberBackgroundStyle = (Style)App.Current.Resources["AmberBackgroundStyle"];
                    var amberForegroundStyle = (Style)App.Current.Resources["AmberForegroundStyle"];

                    personPicture.Style = amberBackgroundStyle;
                    fullNameAndAgeTextBlock.Style = amberForegroundStyle;
                }
                else if (patient.id % 2 == 0)
                {
                    // Apply Red Background and Red Foreground styles
                    var redBackgroundStyle = (Style)App.Current.Resources["RedBackgroundStyle"];
                    var redForegroundStyle = (Style)App.Current.Resources["RedForegroundStyle"];

                    personPicture.Style = redBackgroundStyle;
                    fullNameAndAgeTextBlock.Style = redForegroundStyle;
                }
                else
                {
                    // Apply Accent Background style only
                    var accentBackgroundStyle = (Style)App.Current.Resources["AccentBackgroundStyle"];
                    var accentForegroundStyle = (Style)App.Current.Resources["AccentForegroundStyle"];

                    personPicture.Style = accentBackgroundStyle;
                    fullNameAndAgeTextBlock.Style = accentForegroundStyle;
                }
            }

        }

        public void InitializeLocation()
        {
            displayButton.Label = LanguageService.GetResourceValue("Display2");
            ToolTipService.SetToolTip(displayButton, LanguageService.GetResourceValue("Display2"));

            editButton.Label = LanguageService.GetResourceValue("Edit");
            ToolTipService.SetToolTip(editButton, LanguageService.GetResourceValue("Edit"));

            deleteButton.Label = LanguageService.GetResourceValue("Delete");
            ToolTipService.SetToolTip(deleteButton, LanguageService.GetResourceValue("Delete"));

            deleteConfirmationText.Text = LanguageService.GetResourceValue("DeleteConfirmation");
            deletePatientButton.Content = LanguageService.GetResourceValue("YesDelete");
            dateAndTimeAddedTitle.Text = LanguageService.GetResourceValue("DateAndTimeAdded") + ":";
            dateOfBirthTitle.Text = LanguageService.GetResourceValue("DateOfBirth") + ":";
            medicalHistoryTitle.Text = LanguageService.GetResourceValue("MedicalHistory") + ":";
        }
    }
}
