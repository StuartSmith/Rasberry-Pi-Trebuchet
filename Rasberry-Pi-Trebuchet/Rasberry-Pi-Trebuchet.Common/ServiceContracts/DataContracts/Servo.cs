using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasberry_Pi_Trebuchet.Common.ServiceContracts.DataContracts
{
    public enum ServoWhereAbouts
    {
        ZeroDegrees,
        NinetyDegrees,
        OneEightyDegrees
    }

    public enum ServoType
    {
        LaunchServo
    }

    public class Servo
    {
        public string ServoStatus { get; set; }

        public string Description { get; set; }

        public RaspberryPiGPI0Pin ServoGPIO { get; set; }

    }
}
