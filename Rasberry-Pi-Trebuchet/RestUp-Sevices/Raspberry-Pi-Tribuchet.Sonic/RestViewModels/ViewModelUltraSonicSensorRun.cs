using Raspberry_Pi_Trebuchet.Common.Enums;
using Raspberry_Pi_Trebuchet.Sonic.Interfaces;
using Raspberry_Pi_Trebuchet.Sonic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.Sonic.RestViewModels
{
    public class ViewModelUltraSonicSensorRun : IUltraSonicSensorRun
    {

        public ViewModelUltraSonicSensorRun()
        { }

        public ViewModelUltraSonicSensorRun(UltraSonicSensorRun sensor)
        {
            RequestSentToAzure = sensor.RequestSentToAzure;
            MeasurementIn = sensor.MeasurementIn;
            TotalDistance = sensor.TotalDistance;
            PinGPIOTrigger = sensor.PinGPIOTrigger;
            PinGPIOEcho = sensor.PinGPIOEcho;
            SonicId = sensor.SonicId;
            RunDate = sensor.RunDate;
            SonicGUID = sensor.SonicGUID;
        }

        public bool RequestSentToAzure { get; set; }
        public string MeasurementIn { get; set; }
        public double TotalDistance { get; set; }
        public RaspberryPiGPI0Pin PinGPIOTrigger { get; set; }
        public RaspberryPiGPI0Pin PinGPIOEcho { get; set; }
        public int SonicId { get; set; }
        public string SonicGUID { get; set; }
        public DateTime RunDate { get; set; }
        public List<IUltraSonicSensorRunMeasurement> SonicMeasurements { get; set; }
    }
}
