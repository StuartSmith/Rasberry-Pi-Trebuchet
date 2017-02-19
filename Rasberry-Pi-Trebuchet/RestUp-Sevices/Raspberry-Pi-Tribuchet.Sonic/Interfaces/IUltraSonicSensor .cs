using Raspberry_Pi_Trebuchet.Sonic.Models;
using Raspberry_Pi_Trebuchet.Sonic.RestViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.Sonic.Interfaces
{
    public interface IUltraSonicSensor 
    {
        ViewModelUltraSonicSensorRun RetrieveLatestUltraSonicRun();
        List<ViewModelUltraSonicSensorRun> RetrieveAllRuns();
        ViewModelUltraSonicSensorRun RetrieveUltraSonicRun(long RunId);
        bool StartUltraSonicRun(UltraSonicRunRequest runrequest);
        bool IsUltraSonicServiceRunning();

        long RemoveAllUltraSonicRuns();
    }
}
