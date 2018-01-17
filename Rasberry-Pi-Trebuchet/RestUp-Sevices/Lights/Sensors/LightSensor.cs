using Microsoft.IoT.Lightning.Providers;
using System;
using Windows.Devices;
using Windows.Devices.Gpio;
using Windows.System.Profile;

namespace Raspberry_Pi_Trebuchet.RestUp.Lights.Sensors
{
    public class LightSensor
    {
      
        public  GpioPin _lightPin { get; private set; }
        private Object thisLock = new Object();
        private bool RunningOnPi = false;

        public LightSensor(int lightPin)
        {
            try
            {
                if (LightningProvider.IsLightningEnabled)
                {
                    LowLevelDevicesController.DefaultProvider = LightningProvider.GetAggregateProvider();
                }

                if (AnalyticsInfo.VersionInfo.DeviceFamily.ToUpper() == "Windows.IoT".ToUpper())
                {
                    RunningOnPi = true;
                }

                //determines that the code is running on a Raspberry pi
                if (RunningOnPi == true)
                {
                    GpioController controller = GpioController.GetDefault();

                    lock (this)
                    {
                        _lightPin = controller.OpenPin(lightPin);
                        _lightPin.SetDriveMode(GpioPinDriveMode.Output);
                        _lightPin.Write(GpioPinValue.Low);
                    }
                }
            }
            catch 
            {

            }
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
                    if (RunningOnPi)
                    {
                        if (value)
                            _lightPin.Write(GpioPinValue.High);

                        else
                            _lightPin.Write(GpioPinValue.Low);
                    }

                    _lighton = value;
                }              
                    //}
                }
        }


      
    }
}
