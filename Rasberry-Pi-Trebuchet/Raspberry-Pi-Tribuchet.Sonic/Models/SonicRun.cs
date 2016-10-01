using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Tribuchet.Sonic.Models
{
    public class SonicRun
    {
        public int SonicId { get; set; }

        public DateTime RunDate { get; set; }

        public List<SonicMeasurement> SonicMeasurements { get; set; }
    }
}
