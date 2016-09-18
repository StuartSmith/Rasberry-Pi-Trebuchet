using Rasberry_Pi_Trebuchet.Common.Interfaces;
using Rasberry_Pi_Trebuchet.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasberry_Pi_Trebuchet.IOT.Services
{
    public class LightStatusService : ILightStatus
    {

        private static LightStatusService _instance;
        private List<Light> _Lights;

        private LightStatusService()
        {

            _Lights = new List<Light>();
            _Lights.Add(new Light()
            {
                IsLightOn = false,
                LightPosition = LightType.LeftLight,
                Description = LightType.LeftLight.ToString(),
                LightGPIO = RaspberryPiGPI0Pin.GPIO07

            });

            _Lights.Add(new Light()
            {
                IsLightOn = false,
                LightPosition = LightType.RightLight,
                Description = LightType.RightLight.ToString(),
                LightGPIO = RaspberryPiGPI0Pin.GPIO08
            });

        }



        public static LightStatusService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LightStatusService();
                }
                return _instance;
            }
        }


        //Turns on or off the lights on the Rasberry Pi
        private void SetPILightStatus(Light light)
        {

        }


        public Task<List<Light>> RetrieveLightStatus(string LightType)
        {


            Task<List<Light>> RetrieveLights = Task<List<Light>>.Factory.StartNew(() =>
            {
                var query = from selectedLight in _Lights
                            where LightType.ToString().ToUpper() == selectedLight.Description.ToUpper()
                            select selectedLight;

                var LightToUpdate = query.ToList<Light>();
                return LightToUpdate;
            });

            return RetrieveLights;
        }


        public async Task<List<Light>> RetrieveLightStatuses()
        {
           var RetrieveLights = await Task<List<Light>>.Factory.StartNew(() =>
            {
                return _Lights;
            });

            return RetrieveLights;
        }

        /// <summary>
        /// Sets a light status to be on or OFF
        /// on the tribuchet
        /// </summary>
        /// <param name="light"></param>
        /// <returns></returns>
        public async Task<bool> SetLight(Light light)
        {
            // Send light data to azure
            var lightList = new List<Light>();
            lightList.Add(light);
            await AzureConnectionService.Instance.SendLightData(lightList);

            Task<bool> SetLights = Task<bool>.Factory.StartNew(() =>
            {
                var query = from selectedLight in _Lights
                            where light.Description.ToUpper() == selectedLight.Description.ToUpper()
                            select selectedLight;

                var LightToUpdate = query.FirstOrDefault<Light>();
                LightToUpdate.IsLightOn = light.IsLightOn;
                SetPILightStatus(LightToUpdate);

                return true;
            });
            return SetLights.Result;
        }


    }
}
