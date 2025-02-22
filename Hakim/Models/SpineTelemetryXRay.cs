using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakim.Model
{
    public partial class SpineTelemetryXRay : XRay
    {
        private int vls;
        private int vli;
        private int cobb;
        private int bend;
        private int red;
    }

    public partial class SpineTelemetryXRay
    {
        public int VLS
        {
            get => vls;
            set
            {
                vls = value;
                OnPropertyChanged();
            }
        }

        public int VLI
        {
            get => vli;
            set
            {
                vli = value;
                OnPropertyChanged();
            }
        }

        public int COBB
        {
            get => cobb;
            set
            {
                cobb = value;
                OnPropertyChanged();
            }
        }

        public int BEND
        {
            get => bend;
            set
            {
                bend = value;
                OnPropertyChanged();
            }
        }

        public int RED
        {
            get => red;
            set
            {
                red = value;
                OnPropertyChanged();
            }
        }
    }
}
