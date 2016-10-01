using Raspberry_Pi_Tribuchet.Sonic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Tribuchet.Sonic.RestViewModels
{
    public class SonicRunRestStatusViewModel : ISonicRunStatus
    {
        public bool IsSonicRunRunning { get; set; }        

        public int NumberOfSocondsSonicRunwillRunFor { get; set; }
       
    }
}
