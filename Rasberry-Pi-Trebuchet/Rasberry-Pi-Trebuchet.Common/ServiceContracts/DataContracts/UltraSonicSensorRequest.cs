using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasberry_Pi_Trebuchet.Common.ServiceContracts.DataContracts
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
