using Hakim.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hakim.Model
{
    public partial class Patient 
    { 
        public int id { get; set; }
        private string lastName;
        private string firstName;
        private DateTimeOffset dateOfBirth;
        private int gender;
        private string address;
        private string wilaya;
        private string commune;
        private string postalCode;
        private string phone1;
        private int phone1Owner;
        private string phone2;
        private int phone2Owner;
        private string email;
        private string medicalHistory;
        private string allergies;
        private string currentMedications;
        private string insuranceProvider;
        private string insuranceNumber;
        public DateTime dateOfRegistration { get; set; }
        public ObservableCollection<File> files { get; set; }
        public ObservableCollection<Appointment> appointments { get; set; }
        private string GetAgeAsString(DateTimeOffset birthDate)
        {
            TimeSpan age = DateTime.Now - birthDate;

            if (age.TotalDays < 1)
            {
                // Less than 1 day old
                return $"{age.TotalHours:F0} {LanguageService.GetResourceValue("hours")}";
            }
            else if (age.TotalDays < 30)
            {
                // Less than 1 month old
                return $"{age.TotalDays:F0} {LanguageService.GetResourceValue("days")}";
            }
            else if (age.TotalDays < 365)
            {
                // Less than 1 year old
                int months = (int)(age.TotalDays / 30);
                if (months == 1)
                    return $"{months} {LanguageService.GetResourceValue("month")}";
                else
                    return $"{months} {LanguageService.GetResourceValue("months")}";
            }
            else
            {
                // 1 year or older
                int years = (int)(age.TotalDays / 365);
                if (years == 1)
                    return $"{years} {LanguageService.GetResourceValue("year")}";
                else
                    return $"{years} {LanguageService.GetResourceValue("years")}";
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
        private string GetOwner(int phoneOwner)
        {
            if (phoneOwner == 0)
            {
                return LanguageService.GetResourceValue("Personal");
            } else if (phoneOwner == 1)
            {
                return LanguageService.GetResourceValue("Husband");
            }
            else if (phoneOwner == 2)
            {
                return LanguageService.GetResourceValue("Wife");
            }
            else if (phoneOwner == 3)
            {
                return LanguageService.GetResourceValue("Son");
            }
            else if (phoneOwner == 4)
            {
                return LanguageService.GetResourceValue("Daughter");
            }
            else if (phoneOwner == 5)
            {
                return LanguageService.GetResourceValue("Father");
            }
            else if (phoneOwner == 6)
            {
                return LanguageService.GetResourceValue("Mother");
            }
            else if (phoneOwner == 7)
            {
                return LanguageService.GetResourceValue("Brother");
            }
            else if (phoneOwner == 8)
            {
                return LanguageService.GetResourceValue("Sister");
            }
            else if (phoneOwner == 9)
            {
                return LanguageService.GetResourceValue("Relative");
            }
            else if (phoneOwner == 10)
            {
                return LanguageService.GetResourceValue("Friend");
            }
            else 
                return LanguageService.GetResourceValue("NotEntered");
        }
        /*
        <ComboBoxItem x:Name="fatherPhone1"/>
        <ComboBoxItem x:Name="motherPhone1"/>
        <ComboBoxItem x:Name="brotherPhone1"/>
        <ComboBoxItem x:Name="sisterPhone1"/>
        <ComboBoxItem x:Name="relativePhone1"/>
        <ComboBoxItem x:Name="friendPhone1"/>
         */
    }

    public partial class Patient : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string PropertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

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

        public string fullNameAndAge
        {
            get
            {
                return $"{lastName.ToUpper()} {firstName} ({GetAgeAsString(dateOfBirth)})";
            }
        }

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

        public int Gender
        {
            get => gender;
            set
            {
                gender = value;
                OnPropertyChanged();
            }
        }

        public string Address
        {
            get => address;
            set
            {
                address = value;
                OnPropertyChanged();
            }
        }

        public string Wilaya
        {
            get => wilaya;
            set
            {
                wilaya = value;
                OnPropertyChanged();
            }
        }

        public string Commune
        {
            get => commune;
            set
            {
                commune = value;
                OnPropertyChanged();
            }
        }

        public string PostalCode
        {
            get => postalCode;
            set
            {
                postalCode = value;
                OnPropertyChanged();
            }
        }

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

        public int Phone1Owner
        {
            get => phone1Owner;
            set
            {
                phone1Owner = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(phone1Details));
            }
        }

        public string phone1Details
        {
            get
            {
                if (phone1 != "" && phone1 != null)
                    return $"{AddSpacesBetweenDigits(phone1)} ({GetOwner(phone1Owner)})";
                else return "Non saisi";
            }
        }

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

        public int Phone2Owner
        {
            get => phone2Owner;
            set
            {
                phone2Owner = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(phone2Details));
            }
        }

        public string phone2Details
        {
            get
            {
                if (phone2 != "" && phone2 != null)
                    return $"{AddSpacesBetweenDigits(phone2)} ({GetOwner(phone2Owner)})";
                else return "Non saisi";
            }
        }

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        public string MedicalHistory
        {
            get => medicalHistory;
            set
            {
                medicalHistory = value;
                OnPropertyChanged();
            }
        }

        public string Allergies
        {
            get => allergies;
            set
            {
                allergies = value;
                OnPropertyChanged();
            }
        }

        public string CurrentMedications
        {
            get => currentMedications;
            set
            {
                currentMedications = value;
                OnPropertyChanged();
            }
        }

        public string InsuranceProvider
        {
            get => insuranceProvider;
            set
            {
                insuranceProvider = value;
                OnPropertyChanged();
            }
        }

        public string InsuranceNumber
        {
            get => insuranceNumber;
            set
            {
                insuranceNumber = value;
                OnPropertyChanged();
            }
        }

        public Patient()
        {
            LastName = "";
            FirstName = "";
            DateOfBirth = DateTime.Now;
            gender = -1;
            Address = "";
            Wilaya = "";
            Commune = "";
            PostalCode = "";
            Phone1 = "";
            Phone1Owner = -1;
            Phone2 = "";
            Phone2Owner = -1;
            Email = "";
            MedicalHistory = "";
            Allergies = "";
            CurrentMedications = "";
            InsuranceProvider = "";
            InsuranceNumber = "";
        }
    }
}
