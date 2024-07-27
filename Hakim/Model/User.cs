using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Update;

namespace Hakim.Model
{
    public class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string rank { get; set; }
        public string Rank
        {
            get => rank;
            set
            {
                rank = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(profitionalName));
            }
        }
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
                return $"{rank} {lastName.ToUpper()} {firstName}";
            }
        }
        void OnPropertyChanged([CallerMemberName] string PropertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
    }
}
