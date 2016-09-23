using Rasberry_Pi_Trebuchet.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.Servos.Models
{

    public class Servo
    {
        public string ServoStatus { get; set; }

        public string Description { get; set; }

        public RaspberryPiGPI0Pin ServoGPIO { get; set; }

    }
}
