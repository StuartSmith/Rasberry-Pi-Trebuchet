using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.Sonic.Models
{
    public class UltraSonicSensorRequest
    {
        public UltraSonicSensorRequest()
        {
            SendRequestToAzure = false;
        }

        public bool SendRequestToAzure { get; set; }
    }
}
