using Raspberry_Pi_Tribuchet.Sonic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raspberry_Pi_Tribuchet.Sonic.Models;

namespace Raspberry_Pi_Tribuchet.Sonic.Services
{
    public class UltraSonicSensorService : ISonicProcess
    {

        private static UltraSonicSensorService _instance;
        private bool _retrievingSonicSensorData;
        private int _numbeOfSecondsToRun;
        private Task _taskforSonicSensor;

        private UltraSonicSensorService()
        {
            _retrievingSonicSensorData = false;
            _numbeOfSecondsToRun = 0;
        }

        

        public static UltraSonicSensorService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UltraSonicSensorService();
                }
                return _instance;
            }
        }


        public Task<ISonicRunStatus> IsSonicProcessRunning()
        {
            throw new NotImplementedException();
        }

        public Task<List<SonicRun>> RetrieveAllRuns(int page)
        {
            throw new NotImplementedException();
        }

        public Task<List<SonicRun>> RetrieveLastRun()
        {
            throw new NotImplementedException();
        }

        public SonicRunStatus StartSonicSensorProcessing(int numbeOfSecondsToRun)
        {

            if (!_retrievingSonicSensorData)
            {
                _taskforSonicSensor = Task.Factory.StartNew(() =>
               {
                   DateTime current = DateTime.Now;
                   _retrievingSonicSensorData = true;

                   while (current.AddSeconds(numbeOfSecondsToRun) > DateTime.Now)
                   {
                       Task.Delay(100);
                   }

                   _retrievingSonicSensorData = false;
               });

                return new SonicRunStatus()
                {
                    NumberofSecondsSonicSensorRecordsDataFor = numbeOfSecondsToRun,
                    IsSonicRunRunning = true,
                    WasSonicRunStarted = true
                };
            }
            else
            {
                return new SonicRunStatus()
                {
                    NumberofSecondsSonicSensorRecordsDataFor = _numbeOfSecondsToRun,
                    IsSonicRunRunning = true,
                    WasSonicRunStarted = false
                };

            }


           
       }

       
    }
}
