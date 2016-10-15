using Rasberry_Pi_Trebuchet.Common.Enums;
using Raspberry_Pi_Tribuchet.Sonic.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Tribuchet.Sonic.Models
{
    public class UltraSonicSensorRun
    {
        public UltraSonicSensorRun()
        {
            
            RequestSentToAzure = false;
            MeasurementIn = "Inches";
            RunDate = DateTime.UtcNow;
            PinGPIOTrigger = RaspberryPiGPI0Pin.GPIO20;
            PinGPIOEcho = RaspberryPiGPI0Pin.GPIO21;
            List<UltraSonicSensorRunMeasurement> SonicMeasurements = new List<UltraSonicSensorRunMeasurement>();
            SonicGUID = Guid.NewGuid().ToString();
        }


        public bool RequestSentToAzure { get; set; }
        public string MeasurementIn { get; set; }
        public double TotalDistance { get; set; }
        public RaspberryPiGPI0Pin PinGPIOTrigger { get; set; }
        public RaspberryPiGPI0Pin PinGPIOEcho { get; set; }
        [Key]
        public int SonicId { get; set; }
        public string SonicGUID { get; set; }
        public DateTime RunDate { get; set; }
        public List<UltraSonicSensorRunMeasurement> SonicMeasurements { get; set; }
    }
}
