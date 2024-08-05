using Hakim.Model;
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
            Loaded += PatientGeneralInfoPage_Loaded;
        }

        private void PatientGeneralInfoPage_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = p.viewModel;
            GetGender(p.viewModel.NewPatient);
            GetPhoneOwner(p.viewModel.NewPatient.Phone1Owner, Phone1OwnerComboBox);
            GetPhoneOwner(p.viewModel.NewPatient.Phone2Owner, Phone2OwnerComboBox);
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

        private void GetGender(Model.Patient patient)
        {
            if (patient.Gender == "Masculin")
            {
                GenderComboBox.SelectedIndex = 0;
            } else if (patient.Gender == "Féminin")
            {
                GenderComboBox.SelectedIndex = 1;
            }
        }

        private void GetPhoneOwner(string PhoneOwner, ComboBox comboBox)
        {
            if (PhoneOwner == "Personnel")
            {
                comboBox.SelectedIndex = 0;
            }
            else if (PhoneOwner == "Le mari")
            {
                comboBox.SelectedIndex = 1;
            }
            else if (PhoneOwner == "La femme")
            {
                comboBox.SelectedIndex = 2;
            }
            else if (PhoneOwner == "Le fils")
            {
                comboBox.SelectedIndex = 3;
            }
            else if (PhoneOwner == "La fille")
            {
                comboBox.SelectedIndex = 4;
            }
            else if (PhoneOwner == "Le père")
            {
                comboBox.SelectedIndex = 5;
            }
            else if (PhoneOwner == "La mère")
            {
                comboBox.SelectedIndex = 6;
            }
            else if (PhoneOwner == "Le frère")
            {
                comboBox.SelectedIndex = 7;
            }
            else if (PhoneOwner == "La sœur")
            {
                comboBox.SelectedIndex = 8;
            }
            else if (PhoneOwner == "Proche")
            {
                comboBox.SelectedIndex = 9;
            }
            else if (PhoneOwner == "Ami")
            {
                comboBox.SelectedIndex = 10;
            }
        }

        private void GenderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GenderComboBox.SelectedIndex == 0)
            {
                p.viewModel.NewPatient.Gender = "Masculin";
            }
            else if (GenderComboBox.SelectedIndex == 1)
            {
                p.viewModel.NewPatient.Gender = "Féminin";
            }
            UpdateDialogButtonState();
        }

        private void Phone1OwnerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Phone1OwnerComboBox.SelectedIndex == 0)
            {
                p.viewModel.NewPatient.Phone1Owner = "Personnel";
            }
            else if (Phone1OwnerComboBox.SelectedIndex == 1)
            {
                p.viewModel.NewPatient.Phone1Owner = "Le mari";
            }
            else if (Phone1OwnerComboBox.SelectedIndex == 2)
            {
                p.viewModel.NewPatient.Phone1Owner = "La femme";
            }
            else if (Phone1OwnerComboBox.SelectedIndex == 3)
            {
                p.viewModel.NewPatient.Phone1Owner = "Le fils";
            }
            else if (Phone1OwnerComboBox.SelectedIndex == 4)
            {
                p.viewModel.NewPatient.Phone1Owner = "La fille";
            }
            else if (Phone1OwnerComboBox.SelectedIndex == 5)
            {
                p.viewModel.NewPatient.Phone1Owner = "Le père";
            }
            else if (Phone1OwnerComboBox.SelectedIndex == 6)
            {
                p.viewModel.NewPatient.Phone1Owner = "La mère";
            }
            else if (Phone1OwnerComboBox.SelectedIndex == 7)
            {
                p.viewModel.NewPatient.Phone1Owner = "Le frère";
            }
            else if (Phone1OwnerComboBox.SelectedIndex == 8)
            {
                p.viewModel.NewPatient.Phone1Owner = "La sœur";
            }
            else if (Phone1OwnerComboBox.SelectedIndex == 9)
            {
                p.viewModel.NewPatient.Phone1Owner = "Proche";
            }
            else if (Phone1OwnerComboBox.SelectedIndex == 10)
            {
                p.viewModel.NewPatient.Phone1Owner = "Ami";
            }
            UpdateDialogButtonState();
        }

        private void Phone2OwnerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Phone2OwnerComboBox.SelectedIndex == 0)
            {
                p.viewModel.NewPatient.Phone2Owner = "Personnel";
            }
            else if (Phone2OwnerComboBox.SelectedIndex == 1)
            {
                p.viewModel.NewPatient.Phone2Owner = "Le mari";
            }
            else if (Phone2OwnerComboBox.SelectedIndex == 2)
            {
                p.viewModel.NewPatient.Phone2Owner = "La femme";
            }
            else if (Phone2OwnerComboBox.SelectedIndex == 3)
            {
                p.viewModel.NewPatient.Phone2Owner = "Le fils";
            }
            else if (Phone2OwnerComboBox.SelectedIndex == 4)
            {
                p.viewModel.NewPatient.Phone2Owner = "La fille";
            }
            else if (Phone2OwnerComboBox.SelectedIndex == 5)
            {
                p.viewModel.NewPatient.Phone2Owner = "Le père";
            }
            else if (Phone2OwnerComboBox.SelectedIndex == 6)
            {
                p.viewModel.NewPatient.Phone2Owner = "La mère";
            }
            else if (Phone2OwnerComboBox.SelectedIndex == 7)
            {
                p.viewModel.NewPatient.Phone2Owner = "Le frère";
            }
            else if (Phone2OwnerComboBox.SelectedIndex == 8)
            {
                p.viewModel.NewPatient.Phone2Owner = "La sœur";
            }
            else if (Phone2OwnerComboBox.SelectedIndex == 9)
            {
                p.viewModel.NewPatient.Phone2Owner = "Proche";
            }
            else if (Phone2OwnerComboBox.SelectedIndex == 10)
            {
                p.viewModel.NewPatient.Phone2Owner = "Ami";
            }
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
    }
}
