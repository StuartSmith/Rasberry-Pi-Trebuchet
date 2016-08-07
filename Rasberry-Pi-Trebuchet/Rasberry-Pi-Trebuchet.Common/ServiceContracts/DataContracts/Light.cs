using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasberry_Pi_Trebuchet.Common.ServiceContracts.DataContracts
{
    public enum LightType
    {
        LeftLight,
        RightLight
    }



    public  class Light
    {
        public bool IsLightOn { get; set; }

        public string Description { get; set; }

        public LightType LightPosition { get; set;}

        public RaspberryPiGPI0Pin LightGPIO { get; set; }
    }
}
