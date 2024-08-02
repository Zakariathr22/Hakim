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
                OnPropertyChanged(nameof(fullName));
                OnPropertyChanged(nameof(fullNameAndAge));
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
                OnPropertyChanged(nameof(fullName));
                OnPropertyChanged(nameof(fullNameAndAge));
            }
        }
        public string fullName
        {
            get
            {
                return $"{lastName.ToUpper()} {firstName}";
            }
        }
        private DateTimeOffset dateOfBirth { get; set; }
        public DateTimeOffset DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                dateOfBirth = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(fullNameAndAge));
            }
        }
        public string fullNameAndAge { 
            get
            {
                return $"{lastName.ToUpper()} {firstName} ({GetAgeAsString(dateOfBirth)})";
            } 
        }
        public string gender { get; set; }
        public string address { get; set; }
        public string wilaya { get; set; }
        public string commune { get; set; }
        public string postalCode { get; set; }
        private string phone1 { get; set; }
        public string Phone1
        {
            get => phone1;
            set
            {
                phone1 = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(phone1Details));
            }
        }
        private string phone1Owner { get; set; }
        public string Phone1Owner
        {
            get => phone1Owner;
            set
            {
                phone1Owner = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(phone1Details));
            }
        }
        public string phone1Details { 
            get
            {
                if (phone1 != "" && phone1 != null)
                    return $"{AddSpacesBetweenDigits(phone1)} ({phone1Owner})";
                else return "Non saisi";
            } 
        }
        private string phone2 { get; set; }
        public string Phone2
        {
            get => phone2;
            set
            {
                phone2 = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(phone2Details));
            }
        }
        private string phone2Owner { get; set; }
        public string Phone2Owner
        {
            get => phone2Owner;
            set
            {
                phone2Owner = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(phone1Details));
            }
        }
        public string phone2Details
        {
            get
            {
                if (phone2 != "" && phone2 != null)
                    return $"{AddSpacesBetweenDigits(phone2)} ({phone2Owner})";
                else return "Non saisi";
            }
        }
        public string email { get; set; }
        public string medicalHistory { get; set; }
        public string allergies { get; set; }
        public string currentMedications { get; set; }
        public string insuranceProvider { get; set; }
        public string insuranceNumber { get; set; }
        public DateTime dateOfRegistration { get; set; }

        void OnPropertyChanged([CallerMemberName] string PropertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

        private string GetAgeAsString(DateTimeOffset birthDate)
        {
            TimeSpan age = DateTime.Now - birthDate;

            if (age.TotalDays < 1)
            {
                // Less than 1 day old
                return $"{age.TotalHours:F0} heures";
            }
            else if (age.TotalDays < 30)
            {
                // Less than 1 month old
                return $"{age.TotalDays:F0} jours";
            }
            else if (age.TotalDays < 365)
            {
                // Less than 1 year old
                int months = (int)(age.TotalDays / 30);
                if (months == 0)
                    return $"{months} mois";
                else
                    return $"{months} moi";
            }
            else
            {
                // 1 year or older
                int years = (int)(age.TotalDays / 365);
                if (years == 1)
                    return $"{years} an";
                else
                    return $"{years} ans";
            }
        }

        private string AddSpacesBetweenDigits(string input)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < input.Length; i += 2)
            {
                if (i + 1 < input.Length)
                {
                    result.Append(input[i]);
                    result.Append(input[i + 1]);
                    result.Append(' '); // Add PatientDetailsDisplay space
                }
                else
                {
                    result.Append(input[i]);
                }
            }

            return result.ToString();
        }

        public Patient()
        {
            LastName = "";
            FirstName = "";
            DateOfBirth = DateTime.Now;
            gender = "";
            address = "";
            wilaya = "";
            commune = "";
            postalCode = "";
            Phone1 = "";
            Phone1Owner = "";
            Phone2 = "";
            Phone2Owner = "";
            email = "";
            medicalHistory = "";
            allergies = "";
            currentMedications = "";
            insuranceProvider = "";
            insuranceNumber = "";
        }
    }
}
