using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasberry_Pi_Trebuchet.Common.Models
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
