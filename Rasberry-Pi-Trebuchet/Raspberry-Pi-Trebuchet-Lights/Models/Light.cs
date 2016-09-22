using Rasberry_Pi_Trebuchet.Common.Enums;
using Raspberry_Pi_Trebuchet.Lights.Interfaces;
using Raspberry_Pi_Trebuchet.Lights.Sensors;

namespace Raspberry_Pi_Trebuchet.Lights.Models
{
    public class Light : ILightRestViewModel
    {
        public bool IsLightOn { get; set; }

        public string Description { get; set; }

        public LightType LightPosition { get; set; }

        public RaspberryPiGPI0Pin LightGPIO { get; set; }

        public LightSensor Lightsensor { get; set; }


    }
}
