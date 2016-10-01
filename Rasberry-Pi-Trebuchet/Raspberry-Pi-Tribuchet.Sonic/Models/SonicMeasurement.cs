using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Tribuchet.Sonic.Models
{
    public class SonicMeasurement
    {
        public int SonicMeasurementId { get; set; }
        public DateTime TimeOfMeasurment { get; set; }
        public long MeasurementDistance { get; set; }

        public int RunId { get; set; }
        public SonicRun Run { get; set; }
    }
}
