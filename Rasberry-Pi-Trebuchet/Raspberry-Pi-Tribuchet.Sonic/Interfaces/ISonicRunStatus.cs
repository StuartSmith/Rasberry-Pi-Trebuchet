using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Tribuchet.Sonic.Interfaces
{
    public interface ISonicRunStatus
    {
        bool IsSonicRunRunning { get; set; }

        int NumberofSecondsSonicSensorRecordsDataFor { get; set; }
    }
}
