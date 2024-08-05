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
using Hakim.Model;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Hakim.View.Clients.EditPatient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within PatientDetailsDisplay Frame.
    /// </summary>
    public sealed partial class EdidPatientPage : Page
    {
        Model.Patient patient;
        ContentDialog dialog;
        public EdidPatientPage()
        {
            this.InitializeComponent();
        }

        public EdidPatientPage(ContentDialog dialog, Model.Patient patient)
        {
            this.InitializeComponent();
            this.dialog = dialog;
            this.patient = patient;
            DataContext = patient;
            GetGender(patient);
            GetPhoneOwner(patient.Phone1Owner, Phone1OwnerComboBox);
            GetPhoneOwner(patient.Phone2Owner, Phone2OwnerComboBox);
            Loaded += EdidPatientPage_Loaded;
        }

        private void EdidPatientPage_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDialogButtonState();
        }

        private void GetGender(Model.Patient patient)
        {
            if (patient.Gender == "Masculin")
            {
                GenderComboBox.SelectedIndex = 0;
            }
            else if (patient.Gender == "Féminin")
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
                patient.Gender = "Masculin";
            }
            else if (GenderComboBox.SelectedIndex == 1)
            {
                patient.Gender = "Féminin";
            }
            UpdateDialogButtonState();
        }

        private void Phone1OwnerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Phone1OwnerComboBox.SelectedIndex == 0)
            {
                patient.Phone1Owner = "Personnel";
            }
            else if (Phone1OwnerComboBox.SelectedIndex == 1)
            {
                patient.Phone1Owner = "Le mari";
            }
            else if (Phone1OwnerComboBox.SelectedIndex == 2)
            {
                patient.Phone1Owner = "La femme";
            }
            else if (Phone1OwnerComboBox.SelectedIndex == 3)
            {
                patient.Phone1Owner = "Le fils";
            }
            else if (Phone1OwnerComboBox.SelectedIndex == 4)
            {
                patient.Phone1Owner = "La fille";
            }
            else if (Phone1OwnerComboBox.SelectedIndex == 5)
            {
                patient.Phone1Owner = "Le père";
            }
            else if (Phone1OwnerComboBox.SelectedIndex == 6)
            {
                patient.Phone1Owner = "La mère";
            }
            else if (Phone1OwnerComboBox.SelectedIndex == 7)
            {
                patient.Phone1Owner = "Le frère";
            }
            else if (Phone1OwnerComboBox.SelectedIndex == 8)
            {
                patient.Phone1Owner = "La sœur";
            }
            else if (Phone1OwnerComboBox.SelectedIndex == 9)
            {
                patient.Phone1Owner = "Proche";
            }
            else if (Phone1OwnerComboBox.SelectedIndex == 10)
            {
                patient.Phone1Owner = "Ami";
            }
            UpdateDialogButtonState();
        }

        private void Phone2OwnerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Phone2OwnerComboBox.SelectedIndex == 0)
            {
                patient.Phone2Owner = "Personnel";
            }
            else if (Phone2OwnerComboBox.SelectedIndex == 1)
            {
                patient.Phone2Owner = "Le mari";
            }
            else if (Phone2OwnerComboBox.SelectedIndex == 2)
            {
                patient.Phone2Owner = "La femme";
            }
            else if (Phone2OwnerComboBox.SelectedIndex == 3)
            {
                patient.Phone2Owner = "Le fils";
            }
            else if (Phone2OwnerComboBox.SelectedIndex == 4)
            {
                patient.Phone2Owner = "La fille";
            }
            else if (Phone2OwnerComboBox.SelectedIndex == 5)
            {
                patient.Phone2Owner = "Le père";
            }
            else if (Phone2OwnerComboBox.SelectedIndex == 6)
            {
                patient.Phone2Owner = "La mère";
            }
            else if (Phone2OwnerComboBox.SelectedIndex == 7)
            {
                patient.Phone2Owner = "Le frère";
            }
            else if (Phone2OwnerComboBox.SelectedIndex == 8)
            {
                patient.Phone2Owner = "La sœur";
            }
            else if (Phone2OwnerComboBox.SelectedIndex == 9)
            {
                patient.Phone2Owner = "Proche";
            }
            else if (Phone2OwnerComboBox.SelectedIndex == 10)
            {
                patient.Phone2Owner = "Ami";
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
                && (Phone1OwnerComboBox.SelectedIndex >= 0 && Phone1OwnerComboBox.SelectedIndex <= 10))
            {
                dialog.IsSecondaryButtonEnabled = true;
            }
            else dialog.IsSecondaryButtonEnabled = false;
        }
    }
}
