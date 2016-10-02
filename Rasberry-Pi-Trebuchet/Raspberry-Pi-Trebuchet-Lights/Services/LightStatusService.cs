using Rasberry_Pi_Trebuchet.Common.Enums;
using Raspberry_Pi_Trebuchet.Lights.Interfaces;
using Raspberry_Pi_Trebuchet.Lights.Models;
using Raspberry_Pi_Trebuchet.Lights.RestViewModels;
using Raspberry_Pi_Trebuchet.Lights.Sensors;

using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace Rasberry_Pi_Trebuchet.IOT.Services
{

    /// <summary>
    /// Singleton class like all the service classes
    /// turns on and off the light 
    /// </summary>
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
                LightGPIO = RaspberryPiGPI0Pin.GPIO07,
                Lightsensor = new LightSensor((int) RaspberryPiGPI0Pin.GPIO07)
                

            });

            _Lights.Add(new Light()
            {
                IsLightOn = false,
                LightPosition = LightType.RightLight,
                Description = LightType.RightLight.ToString(),
                LightGPIO = RaspberryPiGPI0Pin.GPIO08,
                Lightsensor = new LightSensor((int)RaspberryPiGPI0Pin.GPIO08)
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
            light.Lightsensor.LightOn = light.IsLightOn;
        }


        public  Task<List<ILightRestViewModel>> RetrieveLightStatus(string LightType)
        {            

            Task<List<ILightRestViewModel>> RetrieveLights =  Task<List<ILightRestViewModel>>.Factory.StartNew(() =>
            {
                var query = from selectedLight in _Lights
                            where LightType.ToString().ToUpper() == selectedLight.Description.ToUpper()
                            select new LightRestViewModel(selectedLight);

                var LightToUpdate = query.ToList<ILightRestViewModel>();
                return LightToUpdate;
            });

            return RetrieveLights;
        }


        public async Task<List<ILightRestViewModel>> RetrieveLightStatuses()
        {
           var RetrieveLights = await Task<List<ILightRestViewModel>>.Factory.StartNew(() =>
            {
                List<ILightRestViewModel> ViewModelLights = new List<ILightRestViewModel>();

                foreach (var light in _Lights)
                {
                    var lightRestViewModel = new LightRestViewModel(light);
                    ViewModelLights.Add(lightRestViewModel);
                }
                return ViewModelLights;
            });

            return RetrieveLights;
        }

        /// <summary>
        /// Sets a light status to be on or OFF
        /// on the tribuchet
        /// </summary>
        /// <param name="light"></param>
        /// <returns></returns>
        public async Task<bool> SetLight(ILightRestViewModel light)
        {
            // Send light data to azure
            var lightList = new List<ILightRestViewModel>();

            lightList.Add(light);
            //await AzureConnectionService.Instance.SendLightData(lightList);

            bool SetLights =  await Task<bool>.Factory.StartNew(() =>
            {
                var query = from selectedLight in _Lights
                            where light.Description.ToUpper() == selectedLight.Description.ToUpper()
                            select selectedLight;

                var LightToUpdate = query.FirstOrDefault<Light>();
                LightToUpdate.IsLightOn = light.IsLightOn;
                SetPILightStatus(LightToUpdate);
              

                return true;
            });
            return SetLights;
        }


    }
}
