﻿using Raspberry_Pi_Trebuchet.Common.Enums;
using Raspberry_Pi_Trebuchet.RestUp.Servos.Sensors;
using Raspberry_Pi_Trebuchet.RestUp.Servos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.Servos.Models
{

    public class Servo : IServoRestViewModel
    {
        public string ServoStatus { get; set; }

        public string Description { get; set; }

        public RaspberryPiGPI0Pin ServoGPIO { get; set; }

        public ServoSensor Servosensor { get; set; }

    }
}
