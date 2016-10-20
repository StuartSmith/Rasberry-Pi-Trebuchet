using Rasberry_Pi_Trebuchet.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.Sonic.Interfaces
{
    public interface IUltraSonicSensorRun
    {
        bool RequestSentToAzure { get; set; }
        string MeasurementIn { get; set; }
        double TotalDistance { get; set; }
        RaspberryPiGPI0Pin PinGPIOTrigger { get; set; }
        RaspberryPiGPI0Pin PinGPIOEcho { get; set; }
        int SonicId { get; set; }
        string SonicGUID { get; set; }
        DateTime RunDate { get; set; }
        List<IUltraSonicSensorRunMeasurement> SonicMeasurements { get; set; }
    }
}
