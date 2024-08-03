using Microsoft.UI.Windowing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;

namespace Hakim.Model
{
    public partial class File 
    {
        public int id { get; set; }
        private Patient patient;
        private string title;
        private DateTime createdDate;
        private string url;
        private string type;
    }

    public partial class File : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string PropertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

        public Patient Patient
        {
            get => patient;
            set
            {
                patient = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public DateTime CreatedDate
        {
            get => createdDate;
            set
            {
                createdDate = value;
                OnPropertyChanged();
            }
        }

        public string Url
        {
            get => url;
            set
            {
                url = value;
                OnPropertyChanged();
            }
        }

        public string Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged();
            }
        }
    }
}
