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
    public partial class SurgeryProtocol : File
    {
        private string surgeon;
        private string operating_assistant;
        private string instrument_technician;
        private string anesthetist;
        private string scrub_nurse;
        private DateTime intervention_date;
        private TimeSpan intervention_time;
        private string diagnosis;
        private string intervention;
        private string operative_report;
    }
    public partial class SurgeryProtocol
    {
        public string Surgeon
        {
            get => surgeon;
            set
            {
                surgeon = value;
                OnPropertyChanged();
            }
        }

        public string Operating_assistant
        {
            get => operating_assistant;
            set
            {
                operating_assistant = value;
                OnPropertyChanged();
            }
        }

        public string Instrument_technician
        {
            get => instrument_technician;
            set
            {
                instrument_technician = value;
                OnPropertyChanged();
            }
        }

        public string Anesthetist
        {
            get => anesthetist;
            set
            {
                anesthetist = value;
                OnPropertyChanged();
            }
        }

        public string Scrub_nurse
        {
            get => scrub_nurse;
            set
            {
                scrub_nurse = value;
                OnPropertyChanged();
            }
        }

        public DateTime Intervention_date
        {
            get => intervention_date;
            set
            {
                intervention_date = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan Intervention_time
        {
            get => intervention_time;
            set
            {
                intervention_time = value;
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

        public string Intervention
        {
            get => intervention;
            set
            {
                intervention = value;
                OnPropertyChanged();
            }
        }

        public string Operative_report
        {
            get => operative_report;
            set
            {
                operative_report = value;
                OnPropertyChanged();
            }
        }

    }
}
