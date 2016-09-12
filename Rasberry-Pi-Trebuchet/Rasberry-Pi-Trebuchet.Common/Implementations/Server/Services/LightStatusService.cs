using Rasberry_Pi_Trebuchet.Common.ServiceContracts;
using Rasberry_Pi_Trebuchet.Common.ServiceContracts.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasberry_Pi_Trebuchet.Common.Implementations.Server.Services
{

    /// <summary>
    /// Using the singleton pattern so only going to create
    /// one instance of the light status object
    /// </summary>
    public class LightStatusServer : ILightStatus
    {

        private static LightStatusServer instance;
        private List<Light> _Lights;

        private LightStatusServer()
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



        public static LightStatusServer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LightStatusServer();
                }
                return instance;
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


        public Task<List<Light>> RetrieveLightStatuses()
        {
            Task<List<Light>> RetrieveLights = Task<List<Light>>.Factory.StartNew(() =>
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
        Task<bool> ILightStatus.SetLight(Light light)
        {

            Task<bool> RetrieveLights = Task<bool>.Factory.StartNew(() =>
            {
                var query = from selectedLight in _Lights
                            where light.Description.ToUpper() == selectedLight.Description.ToUpper()
                            select selectedLight;

                var LightToUpdate = query.FirstOrDefault<Light>();

                LightToUpdate.IsLightOn = light.IsLightOn;

                SetPILightStatus(LightToUpdate);

                return true;

            });

            return RetrieveLights;
        }


    }
}
