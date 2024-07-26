using CommunityToolkit.Mvvm.ComponentModel;
using Hakim.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakim.ViewModel
{
    public partial class ClientsViewModel : ObservableObject
    {
        [ObservableProperty] private Patient newPatient = new Patient();
        [ObservableProperty] ObservableCollection<Patient> patients = new ObservableCollection<Patient>();
    }
}
