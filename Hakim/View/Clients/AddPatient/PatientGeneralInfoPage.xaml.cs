using Hakim.Model;
using Hakim.Service;
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
using Windows.ApplicationModel.VoiceCommands;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Hakim.View.Clients.AddPatient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within patientDetailsDisplay Frame.
    /// </summary>
    public sealed partial class PatientGeneralInfoPage : Page
    {
        AddPatientPage p;
        public PatientGeneralInfoPage()
        {
            this.InitializeComponent();
            this.InitializeLocation();
            Loaded += PatientGeneralInfoPage_Loaded;
        }

        private void PatientGeneralInfoPage_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = p.viewModel;
            p.dialog.PrimaryButtonClick += (sender, args) =>
            {
                // Perform validation or other logic here
                bool isValid = false; // Replace with your validation logic

                p.NavigateWithSlideTransition(typeof(MedicalInfoPage));

                if (!isValid)
                {
                    // If validation fails, cancel the button click event,
                    // which will prevent the dialog from closing
                    args.Cancel = true;
                }
            };
            p.dialog.IsPrimaryButtonEnabled = false;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is AddPatientPage)
            {
                p = e.Parameter as AddPatientPage;
            }
            base.OnNavigatedTo(e);
        }

        private void GenderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDialogButtonState();
        }

        private void Phone1OwnerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDialogButtonState();
        }

        private void lastNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDialogButtonState();
        }

        private void firstNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDialogButtonState();
        }

        private void phone1TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDialogButtonState();
        }

        private void UpdateDialogButtonState()
        {
            if (lastNameTextBox.Text != "" 
                && firstNameTextBox.Text != "" 
                && (GenderComboBox.SelectedIndex == 0 || GenderComboBox.SelectedIndex == 1)
                && phone1TextBox.Text != ""
                && (Phone1OwnerComboBox.SelectedIndex >= 0 && Phone1OwnerComboBox.SelectedIndex <=10))
            {
                p.dialog.IsPrimaryButtonEnabled = true;
            }
            else p.dialog.IsPrimaryButtonEnabled = false;
        }

        public void InitializeLocation()
        {
            personalInformationSubtitle.Text = LanguageService.GetResourceValue("PersonalInformation");

            lastNameTextBox.Header = LanguageService.GetResourceValue("Lastname");
            lastNameTextBox.PlaceholderText = LanguageService.GetResourceValue("Lastname(Required)");
            firstNameTextBox.Header = LanguageService.GetResourceValue("Firstname");
            firstNameTextBox.PlaceholderText = LanguageService.GetResourceValue("Firstname(Required)");

            dateOfBirthPicker.Header = LanguageService.GetResourceValue("DateOfBirth");

            GenderComboBox.Header = LanguageService.GetResourceValue("Gender");
            GenderComboBox.PlaceholderText = LanguageService.GetResourceValue("Gender(Required)");
            maleComboBoxItem.Content = LanguageService.GetResourceValue("Male");
            femaleComboBoxItem.Content = LanguageService.GetResourceValue("Female");

            contactSubtitle.Text = LanguageService.GetResourceValue("Contact");

            phone1TextBox.Header = LanguageService.GetResourceValue("PhoneNumber1");
            phone1TextBox.PlaceholderText = LanguageService.GetResourceValue("PhoneNumber1(Required)");

            Phone1OwnerComboBox.Header = LanguageService.GetResourceValue("Owner");
            Phone1OwnerComboBox.PlaceholderText = LanguageService.GetResourceValue("Owner(Required)");
            personalPhone1.Content = LanguageService.GetResourceValue("Personal");
            husbandPhone1.Content = LanguageService.GetResourceValue("Husband");
            wifePhone1.Content = LanguageService.GetResourceValue("Wife");
            sonPhone1.Content = LanguageService.GetResourceValue("Son");
            daughterPhone1.Content = LanguageService.GetResourceValue("Daughter");
            fatherPhone1.Content = LanguageService.GetResourceValue("Father");
            motherPhone1.Content = LanguageService.GetResourceValue("Mother");
            brotherPhone1.Content = LanguageService.GetResourceValue("Brother");
            sisterPhone1.Content = LanguageService.GetResourceValue("Sister");
            relativePhone1.Content = LanguageService.GetResourceValue("Relative");
            friendPhone1.Content = LanguageService.GetResourceValue("Friend");

            phone2TextBox.Header = LanguageService.GetResourceValue("PhoneNumber2");
            phone2TextBox.PlaceholderText = LanguageService.GetResourceValue("PhoneNumber2");

            Phone2OwnerComboBox.Header = LanguageService.GetResourceValue("Owner");
            Phone2OwnerComboBox.PlaceholderText = LanguageService.GetResourceValue("Owner");
            personalPhone2.Content = LanguageService.GetResourceValue("Personal");
            husbandPhone2.Content = LanguageService.GetResourceValue("Husband");
            wifePhone2.Content = LanguageService.GetResourceValue("Wife");
            sonPhone2.Content = LanguageService.GetResourceValue("Son");
            daughterPhone2.Content = LanguageService.GetResourceValue("Daughter");
            fatherPhone2.Content = LanguageService.GetResourceValue("Father");
            motherPhone2.Content = LanguageService.GetResourceValue("Mother");
            brotherPhone2.Content = LanguageService.GetResourceValue("Brother");
            sisterPhone2.Content = LanguageService.GetResourceValue("Sister");
            relativePhone2.Content = LanguageService.GetResourceValue("Relative");
            friendPhone2.Content = LanguageService.GetResourceValue("Friend");

            emailTextBox.Header = LanguageService.GetResourceValue("Email");
            emailTextBox.PlaceholderText = LanguageService.GetResourceValue("Email");

            addressInformationSubtitle.Text = LanguageService.GetResourceValue("AddressInformation");

            AddressTextBox.Header = LanguageService.GetResourceValue("Address");
            AddressTextBox.PlaceholderText = LanguageService.GetResourceValue("Address");

            stateTextBox.Header = LanguageService.GetResourceValue("State");
            stateTextBox.PlaceholderText = LanguageService.GetResourceValue("State");

            cityTextBox.Header = LanguageService.GetResourceValue("City");
            cityTextBox.PlaceholderText = LanguageService.GetResourceValue("City");

            zipCodeTextBox.Header = LanguageService.GetResourceValue("ZipCode");
            zipCodeTextBox.PlaceholderText = LanguageService.GetResourceValue("ZipCode");

            insuranceInformationSubtitle.Text = LanguageService.GetResourceValue("InsuranceInformation");

            insuranceProviderTextBox.Header = LanguageService.GetResourceValue("InsuranceProvider");
            insuranceProviderTextBox.PlaceholderText = LanguageService.GetResourceValue("InsuranceProvider");

            insuranceNumberTextBox.Header = LanguageService.GetResourceValue("InsuranceNumber");
            insuranceNumberTextBox.PlaceholderText = LanguageService.GetResourceValue("InsuranceNumber");
        }
    }
}
