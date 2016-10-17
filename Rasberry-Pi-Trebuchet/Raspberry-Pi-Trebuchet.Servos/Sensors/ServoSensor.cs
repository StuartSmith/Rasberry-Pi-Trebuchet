using Rasberry_Pi_Trebuchet.Common.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Microsoft.IoT.Lightning.Providers;
using Windows.Devices;

namespace Rasberry_Pi_Trebuchet.IOT.Sensors
{

    /// <summary>
    /// The Lightning driver is required to move the sensor change the device driver to use lightning
    /// https://developer.microsoft.com/en-us/windows/iot/docs/lightningsetup
    /// </summary>
    public class ServoSensor
    {
        private GpioController _gpioController;
        private static  GpioPin _motorPin;
        private ulong _ticksPerMilliSecond = (ulong)(Stopwatch.Frequency) / 1000; //Number of ticks per millisecond this is different for different processor

        private Object thisLock = new Object();

        public RaspberryPiGPI0Pin RaspberryGPIOpin { get; }

        public bool GpioInitialized
        {
            get;
            private set;
        }

        #region Constructors
        /// <summary>
        /// Create a Motor contoller that is connected to 
        /// GPIO Pin 2
        /// </summary>
        public ServoSensor()
        {
            RaspberryGPIOpin = RaspberryPiGPI0Pin.GPIO05;
            GpioInit();
        }

        /// <summary>
        /// Create a Motor contoller that is connected to 
        /// a sepcified GPIO Pin
        /// </summary>
        /// <param name="gpioPin"></param>
        public ServoSensor(RaspberryPiGPI0Pin gpioPin)
        {
            RaspberryGPIOpin = gpioPin;
            GpioInit();
            //var task =  GpioInit();
            //task.Wait();
        }
        #endregion

        /// <summary>
        /// Initialize the GPIO pin
        /// </summary>
        //private async Task<bool> GpioInit()
       private void GpioInit()
        {
            try
            {
                if (LightningProvider.IsLightningEnabled)
                {
                    LowLevelDevicesController.DefaultProvider = LightningProvider.GetAggregateProvider();
                }


                GpioInitialized = false;
                _gpioController = GpioController.GetDefault(); 
                _motorPin = _gpioController.OpenPin(Convert.ToInt32(RaspberryGPIOpin));
                _motorPin.SetDriveMode(GpioPinDriveMode.Output);             
                GpioInitialized = true;

               
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR: GpioInit failed - " + ex.ToString());
            }

            //return true;
        }

        /// <summary>
        /// Sends a pulse to the server motor that will 
        /// turn it in one direction or another
        /// </summary>
        /// <param name="rotateServer">Enumeration for rotating the server</param>        
        public void PulseMotor(RotateServer rotateServer)
        {
            PulseMotor(ServoPulseTime(rotateServer));
        }

        /// <summary>
        //Function to wait so many milliseconds, this is required because a task.delay
        // time to execute is too long. This is a blocking thread but since the time
        // to wait are so small for the SG90 it may not matter
        /// </summary>
        /// <param name="millisecondsToWait">Number of milliseconds before the function returns</param>
        private void MillisecondToWait(double millisecondsToWait)
        {
            var sw = new Stopwatch();
            double durationTicks = _ticksPerMilliSecond * millisecondsToWait;
            sw.Start();
            while (sw.ElapsedTicks < durationTicks)
            {
                int x = 3;
                x = x + x;
            }
        }

        /// <summary>
        /// Sends enough pulses to the server motor that will 
        /// turn it all the way in one direction or another or towards the center.
        /// </summary>
        /// <param name="motorPulse">number of milliseconds to wait to pulse the servo</param>
        public void PulseMotor(double motorPulse)
        {
            lock (thisLock)
            {
                try
                {
                    if (_motorPin == null)
                        GpioInit();

                    //Total amount of time for a pulse
                    double TotalPulseTime;
                    double timeToWait;

                    TotalPulseTime = 25;
                    timeToWait = TotalPulseTime - motorPulse;


                    //Send the pulse to move the servo over a given time span
                    _motorPin?.Write(GpioPinValue.High);
                    MillisecondToWait(motorPulse);
                    _motorPin?.Write(GpioPinValue.Low);
                    MillisecondToWait(timeToWait);
                    _motorPin?.Write(GpioPinValue.Low);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Oops the unthinkable happened ");
                }
            }
        }

        /// <summary>
        /// 
        /// Retrieves the number of milliconds to send as a pulse to turn the motor
        /// to the left right or middle
        /// 
        /// Values from Specification 
        ///     Position "0"   (1.5 ms pulse) is middle,
        ///     Position "90"  (~2 ms pulse) is all the way to the right
        ///     Position"-90" (~1 ms pulse) is all the way to the left
        ///     
        /// Values that were found to actually work
        ///     Position "0"   (1.2 ms pulse) is middle,
        ///     Position "90"  (~2 ms pulse) is all the way to the right
        ///     Position"-90" (~.4 ms pulse) is all the way to the left

        /// 
        /// </summary>
        /// <returns>
        /// The number of milliseconds to send as a pulse to the servo to move the motor
        /// </returns>
        private double ServoPulseTime(RotateServer rotateServer)
        {
            switch (rotateServer)
            {
                case Common.Enums.RotateServer.RotateToLeft:
                    return 2;
                case RotateServer.RotateToMiddle:
                    return 1.2;
                case RotateServer.RotateToRight:
                    return .8;

            }
            return -1;
        }
    }
}
