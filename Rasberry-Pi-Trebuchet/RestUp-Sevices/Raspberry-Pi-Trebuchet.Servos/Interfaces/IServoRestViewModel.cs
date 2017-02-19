using Raspberry_Pi_Trebuchet.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.Servos.Interfaces
{
    public interface IServoRestViewModel
    {
       string ServoStatus { get; set; }

        string Description { get; set; }

        RaspberryPiGPI0Pin ServoGPIO { get; set; }

    }
}
