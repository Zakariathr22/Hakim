using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakim.Model
{
    public partial class MedicalConsultation : File
    {
        private string notes;
        private string prescription;
    }
    public partial class MedicalConsultation 
    {
        public string Notes
        {
            get => notes;
            set
            {
                notes = value;
                OnPropertyChanged();
            }
        }
        public string Prescription
        {
            get => prescription;
            set
            {
                prescription = value;
                OnPropertyChanged();
            }
        }
    }
}
