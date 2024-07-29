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
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EdidPatientPage : Page
    {
        Patient patient;
        ContentDialog dialog;
        public EdidPatientPage()
        {
            this.InitializeComponent();
        }

        public EdidPatientPage(ContentDialog dialog, Patient patient)
        {
            this.InitializeComponent();
            this.dialog = dialog;
            this.patient = patient;
            DataContext = patient;
            GetGender(patient);
            GetPhoneOwner(patient.Phone1Owner, Phone1OwnerComboBox);
            GetPhoneOwner(patient.Phone2Owner, Phone2OwnerComboBox);
        }

        private void GetGender(Patient patient)
        {
            if (patient.gender == "Masculin")
            {
                GenderComboBox.SelectedIndex = 0;
            }
            else if (patient.gender == "Féminin")
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
    }
}
