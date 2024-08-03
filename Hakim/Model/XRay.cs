using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakim.Model
{
    public partial class XRay : File
    {
        private DateTime xray_date;
        private string radiologist;
        private string diagnosis;
        private string xray_type;
    }
    public partial class XRay
    {
        public DateTime Xray_date
        {
            get => xray_date;
            set
            {
                xray_date = value;
                OnPropertyChanged();
            }
        }

        public string Radiologist
        {
            get => radiologist;
            set
            {
                radiologist = value;
                OnPropertyChanged();
            }
        }

        public string Diagnosis
        {
            get => diagnosis;
            set
            {
                diagnosis = value;
                OnPropertyChanged();
            }
        }

        public string Xray_type
        {
            get => xray_type;
            set
            {
                diagnosis = xray_type;
                OnPropertyChanged();
            }
        }
    }
}
