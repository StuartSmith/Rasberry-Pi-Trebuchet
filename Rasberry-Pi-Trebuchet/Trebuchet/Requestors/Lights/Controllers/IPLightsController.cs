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

namespace Trebuchet.Requestors.Lights.Controllers
{
    public class IPLightsController : ILightsController
    {
        IMainPaigeFlipViewModel _mainPageFlipViewModel;
        HttpClient client = new HttpClient();

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

        public IPLightsController(IMainPaigeFlipViewModel mainPaigeFlipViewModel)
        {
            _mainPageFlipViewModel = mainPaigeFlipViewModel;
        }

        public Task<List<LightRestViewModel>> GetLightStatuses()
        {
            throw new NotImplementedException();
        }

        public Task<List<LightRestViewModel>> TurnBothLightsOff()
        {
            throw new NotImplementedException();
        }

        public Task<List<LightRestViewModel>> TurnBothLightsOn()
        {
            throw new NotImplementedException();
        }

        public Task<LightRestViewModel> TurnLeftLightOff()
        {
            throw new NotImplementedException();
        }

        public async Task<LightRestViewModel> TurnLeftLightOn()
        {
            HttpClient client = SetupHttpClient();

            LightRestViewModel lightObject = new LightRestViewModel();
            lightObject.IsLightOn = true;
            lightObject.LightPosition = LightType.LeftLight;
            lightObject.Description = LightType.LeftLight.ToString();

            StringContent JSONStringContent = new StringContent(JsonConvert.SerializeObject(lightObject));
            HttpResponseMessage response = await client.PostAsync("/api/lights/statuses", JSONStringContent);
            response.EnsureSuccessStatusCode();


            throw new NotImplementedException();
        }

        public Task<LightRestViewModel> TurnRightLightOff()
        {
            throw new NotImplementedException();
        }

        public Task<LightRestViewModel> TurnRightLightOn()
        {
            throw new NotImplementedException();
        }
    }
}
