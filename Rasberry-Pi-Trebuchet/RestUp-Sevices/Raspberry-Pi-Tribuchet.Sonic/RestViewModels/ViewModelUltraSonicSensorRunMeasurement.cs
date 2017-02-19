using Raspberry_Pi_Trebuchet.Sonic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.Sonic.RestViewModels
{
    public class ViewModelUltraSonicSensorRunMeasurement : IUltraSonicSensorRunMeasurement
    {
        public ViewModelUltraSonicSensorRunMeasurement()
        {
            MeasurementGUID = Guid.NewGuid().ToString();
        }

        public ViewModelUltraSonicSensorRunMeasurement(IUltraSonicSensorRunMeasurement RunMeasurement)
        {
            MeasurementDistance = RunMeasurement.MeasurementDistance;
            SonicMeasurementId = RunMeasurement.SonicMeasurementId;
            TimeOfMeasurment = RunMeasurement.TimeOfMeasurment;
            UltraSonicSensorRunId = RunMeasurement.UltraSonicSensorRunId;
            MeasurementGUID = RunMeasurement.MeasurementGUID;
            SonicGUID = RunMeasurement.SonicGUID;
        }

        public double MeasurementDistance { get; set; }
        public string SonicGUID { get; set; }
        public string MeasurementGUID { get; set; }
        public int SonicMeasurementId { get; set; }
        public DateTime TimeOfMeasurment { get; set; }
        public int UltraSonicSensorRunId { get; set; }

    }
}
