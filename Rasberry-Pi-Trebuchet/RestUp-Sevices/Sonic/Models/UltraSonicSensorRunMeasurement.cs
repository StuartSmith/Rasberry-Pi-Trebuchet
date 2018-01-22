using Raspberry_Pi_Trebuchet.RestUp.Sonic.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.Sonic.Models
{
    [DebuggerDisplay("SonicMeasurementId {SonicMeasurementId}  MeasurementDistance {MeasurementDistance} ")]
    public class UltraSonicSensorRunMeasurement: IUltraSonicSensorRunMeasurement
    {
        [Key]
        public int SonicMeasurementId { get; set; }
        public DateTime TimeOfMeasurment { get; set; }
        public double MeasurementDistance { get; set; }
        public int UltraSonicSensorRunId { get; set; }
        public UltraSonicSensorRun Run { get; set; }
        public string SonicGUID { get; set; }
        public string MeasurementGUID { get; set; }
    }
}
