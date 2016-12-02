using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raspberry_Pi_Trebuchet.Lights.RestViewModels;
using Trebuchet.Requestors.Lights.Interfaces;
using Trebuchet.Interfaces;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Raspberry_Pi_Trebuchet.Common.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Trebuchet.Requestors.Lights.Controllers
{
    public class DirectIPLightsController : ILightsController
    {
        IMainPaigeFlipViewModel _mainPageFlipViewModel;
        HttpClient client = new HttpClient();

        public DirectIPLightsController(IMainPaigeFlipViewModel mainPaigeFlipViewModel)
        {
            _mainPageFlipViewModel = mainPaigeFlipViewModel;
        }

        private HttpClient SetupHttpClient()
        {
            HttpClient client = new HttpClient();
            if (_mainPageFlipViewModel.UseIP ==true)
                client.BaseAddress = new Uri($"http://{_mainPageFlipViewModel.PiIp}/");
            else
                client.BaseAddress = new Uri($"http://{_mainPageFlipViewModel.PiName}/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private async  Task<HttpResponseMessage> SendLightStatus(LightRestViewModel lightObject)
        {
            HttpClient client = SetupHttpClient();
            return await SendLightStatus(lightObject,  client);
        }


        private async Task<HttpResponseMessage> SendLightStatus(LightRestViewModel lightObject, HttpClient client)
        {
            HttpClient localclient = client;
            StringContent JSONStringContent = new StringContent(JsonConvert.SerializeObject(lightObject));
            HttpResponseMessage response = await localclient.PostAsync("/api/lights/statuses", JSONStringContent);

            return response;
        }


        public async  Task<List<LightRestViewModel>> GetLightStatuses()
        {
            HttpClient client = SetupHttpClient();
            string PIURL = $"/api/lights/statuses?= {DateTime.Now.ToString()}";
            HttpResponseMessage response = await client.GetAsync(PIURL);

            string content = await response.Content.ReadAsStringAsync();

            List<LightRestViewModel> Lights = DeserializeToList<LightRestViewModel>(content);


            return Lights;
        }

        private List<T> DeserializeToList<T>(string jsonString)
        {
            //InvalidJsonElements = null;
            var array = JArray.Parse(jsonString);
            List<T> objectsList = new List<T>();


            foreach (var item in array)
            {
                try
                {
                    // CorrectElements
                    objectsList.Add(item.ToObject<T>());
                }
                catch (Exception ex)
                {
                   // InvalidJsonElements = InvalidJsonElements ?? new List<string>();
                   //InvalidJsonElements.Add(item.ToString());
                }
            }

            return objectsList;
        }

        public async  Task<LightRestViewModel> TurnLeftLightOff()
        {
            LightRestViewModel lightObject = new LightRestViewModel()
            {
                IsLightOn =false,
                Description = LightType.LeftLight.ToString()
            };  

            HttpResponseMessage response = await SendLightStatus(lightObject);

            if (response.StatusCode == HttpStatusCode.Created)
                return lightObject;

            return new LightRestViewModel() { IsLightOn = false };
        }


        public async Task<LightRestViewModel> TurnLeftLightOn()
        {
            LightRestViewModel lightObject = new LightRestViewModel()
            {
                IsLightOn = true,
                Description = LightType.LeftLight.ToString()
            };

            HttpResponseMessage response = await SendLightStatus(lightObject);

            if (response.StatusCode == HttpStatusCode.Created)
                return lightObject;

            return new LightRestViewModel() { IsLightOn = false };
        }


        public async  Task<LightRestViewModel> TurnRightLightOff()
        {
            LightRestViewModel lightObject = new LightRestViewModel()
            {
                IsLightOn = false,
                Description = LightType.RightLight.ToString()
            };

            HttpResponseMessage response = await SendLightStatus(lightObject);

            if (response.StatusCode == HttpStatusCode.Created)
                return lightObject;

            return new LightRestViewModel() { IsLightOn = false };
        }


        public async Task<LightRestViewModel> TurnRightLightOn()
        {
            LightRestViewModel lightObject = new LightRestViewModel()
            {
                IsLightOn = true,
                Description = LightType.RightLight.ToString()
            };

            HttpResponseMessage response = await SendLightStatus(lightObject);

            if (response.StatusCode == HttpStatusCode.Created)
                return lightObject;

            return new LightRestViewModel() { IsLightOn = false };
        }
    }
}
