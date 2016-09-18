using Rasberry_Pi_Trebuchet.Common.Models;
using Rasberry_Pi_Trebuchet.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasberry_Pi_Trebuchet.IOT.Services
{
    public class UltraSonicSensorService : IUltraSonicSensor
    {

        private static UltraSonicSensorService _instance;

        private UltraSonicSensorService()
        {
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


        private UltraSonicSensor CreateUltraSonicSensorFromRequest(UltraSonicSensorRequest request)
        {
            UltraSonicSensor ultrasonicSensor = new UltraSonicSensor();
            ultrasonicSensor.SendRequestToAzure = request.SendRequestToAzure;
            return ultrasonicSensor;

        }


        private async Task<List<UltraSonicSensor>> GetDatafromSensor(UltraSonicSensorRequest request)
        {

            Task<List<UltraSonicSensor>> RetrieveSensors = Task<List<UltraSonicSensor>>.Factory.StartNew(() =>
            {
                UltraSonicSensor ultrasonicSensor = CreateUltraSonicSensorFromRequest(request);
                var sensors = new List<UltraSonicSensor>();
                sensors.Add(ultrasonicSensor);
                return sensors;

            });

            if (request.SendRequestToAzure)
            {
                await AzureConnectionService.Instance.SendUltraSonicData(RetrieveSensors.Result);
            }

            return RetrieveSensors.Result;
        }


        public async Task<List<UltraSonicSensor>> RetrieveSensorsData(UltraSonicSensorRequest request)
        {
            return await GetDatafromSensor(request);
        }


    }
}
