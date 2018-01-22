using Microsoft.IoT.Lightning.Providers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices;
using Windows.Devices.Gpio;
using Windows.System.Profile;

namespace Raspberry_Pi_Trebuchet.RestUp.Sonic.Sensors
{
    class UltraSonicSensor
    {
        private GpioPin triggerPin { get; set; }
        private GpioPin echoPin { get; set; }
        private Stopwatch timeWatcher;
        private bool RunningOnPi = false;


        public UltraSonicSensor(int triggerPin, int echoPin)
        {
            if (LightningProvider.IsLightningEnabled)
            {
                LowLevelDevicesController.DefaultProvider = LightningProvider.GetAggregateProvider();
            }



            GpioController controller = GpioController.GetDefault();
            timeWatcher = new Stopwatch();

            if (AnalyticsInfo.VersionInfo.DeviceFamily.ToUpper() == "Windows.IoT".ToUpper())
            {
                RunningOnPi = true;

            }

            if (RunningOnPi)
            {
                //initialize trigger pin.
                this.triggerPin = controller.OpenPin(triggerPin);
                this.triggerPin.SetDriveMode(GpioPinDriveMode.Output);
                this.triggerPin.Write(GpioPinValue.Low);
                //initialize echo pin.
                this.echoPin = controller.OpenPin(echoPin);
                this.echoPin.SetDriveMode(GpioPinDriveMode.Input);
            }
        }

        private double RandomNumberDistanceGenerator()
        {
            var list = new[] { new { value = 0.0, Id = Guid.NewGuid() } }.ToList();
            for (double i = 0.0001; i < .01; i = i + .0001)
            {
                list.Add(new { value =i, Id = Guid.NewGuid() });
            }

            var ReturnValue = (from item in list orderby item.Id select item.value).FirstOrDefault<double>();

            return ReturnValue;
        }

        private double GetDistance()
        {
            if (RunningOnPi ==false)
            {
                return RandomNumberDistanceGenerator();
            }


            ManualResetEvent mre = new ManualResetEvent(false);
            mre.WaitOne(100);
            Stopwatch pulseLength = new Stopwatch();
            Stopwatch TotalTime = new Stopwatch();


            TotalTime.Start();

            //Send pulse
            this.triggerPin.Write(GpioPinValue.High);
            mre.WaitOne(TimeSpan.FromMilliseconds(0.01));
            this.triggerPin.Write(GpioPinValue.Low);

            //Recieve pusle
            while (this.echoPin.Read() == GpioPinValue.Low && TotalTime.ElapsedMilliseconds < 5000)
            {
            }
            pulseLength.Start();

            while (this.echoPin.Read() == GpioPinValue.High && TotalTime.ElapsedMilliseconds < 5000)
            {
            }
            pulseLength.Stop();

            if (TotalTime.ElapsedMilliseconds >= 5000)
                return -1;


            //Calculating distance
            TimeSpan timeBetween = pulseLength.Elapsed;
            //Debug.WriteLine(timeBetween.ToString());

            return timeBetween.TotalSeconds;
        }

        public double GetDistanceInCentimeters => GetDistance() * 17000;

        public double GetDistanceInInches => GetDistance() * 17000 / 2.5;





        private double PulseIn(GpioPin echoPin, GpioPinValue value)
        {
            var t = Task.Run(() =>
            {
                //Recieve pusle
                while (this.echoPin.Read() != value)
                {
                }
                timeWatcher.Start();

                while (this.echoPin.Read() == value)
                {
                }
                timeWatcher.Stop();
                //Calculating distance
                double distance = timeWatcher.Elapsed.TotalSeconds * 17000;
                return distance;
            });
            bool didComplete = t.Wait(5000);
            if (didComplete)
            {
                return t.Result;
            }
            else
            {
                return 0.0;
            }
        }




    }
}
