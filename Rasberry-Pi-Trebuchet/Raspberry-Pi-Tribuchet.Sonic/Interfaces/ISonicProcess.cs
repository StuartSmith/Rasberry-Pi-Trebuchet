using Raspberry_Pi_Tribuchet.Sonic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Tribuchet.Sonic.Interfaces
{
    public interface ISonicProcess
    {
        Task<List<SonicRun>> RetrieveLastRun();
        
        Task<List<SonicRun>> RetrieveAllRuns(int page);

        SonicRunStatus StartSonicSensorProcessing(int numbeOfSeconds);

        Task<ISonicRunStatus> IsSonicProcessRunning();
    }
}
