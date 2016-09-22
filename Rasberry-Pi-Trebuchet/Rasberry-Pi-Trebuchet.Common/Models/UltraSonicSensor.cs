using Rasberry_Pi_Trebuchet.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasberry_Pi_Trebuchet.Common.Models
{
    public class UltraSonicSensor : UltraSonicSensorRequest
    {

        public UltraSonicSensor()
        {
            SendRequestToAzure = false;
            RequestSentToAzure = false;
            MeasurementIn = "Inches";
            TimeOfMeasureMent = DateTime.UtcNow;

            PinGPIOTrigger = RaspberryPiGPI0Pin.GPIO20;
            PinGPIOEcho = RaspberryPiGPI0Pin.GPIO21;



        }


        public bool RequestSentToAzure { get; set; }
        public string MeasurementIn { get; set; }
        public double CurrentDistance { get; set; }
        public DateTime TimeOfMeasureMent { get; set; }
        public RaspberryPiGPI0Pin PinGPIOTrigger { get; set; }
        public RaspberryPiGPI0Pin PinGPIOEcho { get; set; }

    }
}
