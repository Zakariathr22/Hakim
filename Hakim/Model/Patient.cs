using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hakim.Model
{
    public class Patient : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int id { get; set; }
        private string lastName { get; set; }
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(profitionalName));
            }
        }
        private string firstName { get; set; }
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(profitionalName));
            }
        }
        public string profitionalName
        {
            get
            {
                return $"{lastName} {firstName}";
            }
        }
        public DateTime dateOfBirth { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string wilaya { get; set; }
        public string commune { get; set; }
        public string postalCode { get; set; }
        public string phone1 { get; set; }
        public string phone1Owner { get; set; }
        public string phone2 { get; set; }
        public string phone2Owner { get; set; }
        public string email { get; set; }
        public string medicalHistory { get; set; }
        public string allergies { get; set; }
        public string currentMedications { get; set; }
        public string insuranceProvider { get; set; }
        public string insuranceNumber { get; set; }
        public DateTime dateOfRegistration { get; set; }

        void OnPropertyChanged([CallerMemberName] string PropertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
    }
}
