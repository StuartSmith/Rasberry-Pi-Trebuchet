using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace Rasberry_Pi_Trebuchet.IOT.Sensors
{
    class LightSensor
    {

        private GpioPin _lightPin { get; set; }

        public LightSensor(int lightPin)
        {
            GpioController controller = GpioController.GetDefault();

            _lightPin = controller.OpenPin(lightPin);
           _lightPin.SetDriveMode(GpioPinDriveMode.Output);
            _lightPin.Write(GpioPinValue.Low);
        }

        private bool _lighton = false;
        public bool LightOn
        {
            get {
                    return _lighton;
               }
            set
                {
                    if (value != _lighton)
                    {
                        if (value)                        
                        _lightPin.Write(GpioPinValue.High);
                    
                        else
                        _lightPin.Write(GpioPinValue.Low);                   
                    }
                }
        }
    }
}
