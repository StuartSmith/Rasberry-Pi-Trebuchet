using System;
using Windows.Devices.Gpio;


namespace Raspberry_Pi_Trebuchet.Lights.Sensors
{
    public class LightSensor
    {

        public  GpioPin _lightPin { get; private set; }

        private Object thisLock = new Object();

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
                //if (value != _lighton)
                //{
                lock (thisLock)
                {
                    if (value)
                        _lightPin.Write(GpioPinValue.High);
                    
                    else
                        _lightPin.Write(GpioPinValue.Low);

                    _lighton = value;
                }              
                    //}
                }
        }


      
    }
}
