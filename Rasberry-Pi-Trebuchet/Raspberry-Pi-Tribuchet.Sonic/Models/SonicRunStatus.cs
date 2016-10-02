using Raspberry_Pi_Tribuchet.Sonic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Tribuchet.Sonic.Models
{
    public class SonicRunStatus : ISonicRunStatus
    {
        public bool IsSonicRunRunning { get; set; }

        public int NumberofSecondsSonicSensorRecordsDataFor { get; set; }

        public bool WasSonicRunStarted { get; set; }
    }
}
