using Raspberry_Pi_Tribuchet.Sonic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Tribuchet.Sonic.Interfaces
{
    public interface ISonicStatus
    {
        Task<List<SonicRun>> RetrieveLastRun();
        
        Task<List<SonicRun>> RetrieveAllRuns(int page);

        Task<ISonicRunStatus> StartSonicRun(int numbeOfSeconds);

        Task<ISonicRunStatus> IsSonicRunRunning();
    }
}
