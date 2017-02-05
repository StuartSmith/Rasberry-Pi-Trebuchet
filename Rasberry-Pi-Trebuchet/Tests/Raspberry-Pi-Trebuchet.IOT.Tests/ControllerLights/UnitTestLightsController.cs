using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Newtonsoft.Json;
using Rasberry_Pi_Trebuchet.Common.RestViewModels;
using Raspberry_Pi_Trebuchet.Common.Enums;
using Raspberry_Pi_Trebuchet.Lights.Controllers.api;
using Raspberry_Pi_Trebuchet.Lights.RestViewModels;
using Restup.HttpMessage.Models.Schemas;
using Restup.Webserver.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.IOT.Tests.ControllerLights
{
    [TestClass]
    public class UnitTestLightsController
    {
        [TestMethod]
        public void LightTest_GetMultipleLightStatuses()
        {
            //Create the Rest Rout handler to process the request
            var Lights = GetLightStatuses();

            Assert.AreEqual(Lights.Count(), 2, "There should be two lights in the collection but there are {Lights.Count()}");

        }


        [TestMethod]
        public void LightTest_TurnLeftLightOn()
        {
            //Create the Rest Rout handler to process the request
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<LightsController>();

            //Makes sure the left light is turned off...
            var leftLight = new LightRestViewModel()
            {
                Description = Common.Enums.LightType.LeftLight.ToString(),               
                IsLightOn = false
            };
            ChangeLightStatus(leftLight);
            var Lights = GetLightStatuses();            
            Assert.AreEqual(Lights.Where(x => (x.IsLightOn == false && x.Description == LightType.LeftLight.ToString())).Any(), true, "There should be two lights in the collection but there are {Lights.Count()}");


            //Turn Left Light On
            leftLight.IsLightOn = true;
            ChangeLightStatus(leftLight);
            Lights = GetLightStatuses();
            Assert.AreEqual(Lights.Where(x => (x.IsLightOn == true && x.Description == LightType.LeftLight.ToString())).Any(), true, "There should be two lights in the collection but there are {Lights.Count()}");


        }


        [TestMethod]
        public void LightTest_TurnRightLightOn()
        {
            //Create the Rest Rout handler to process the request
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<LightsController>();

            //Makes sure the Right light is turned off...          
            var rightLight = new LightRestViewModel()
            {
                Description = Common.Enums.LightType.RightLight.ToString(),
                IsLightOn = false
            };

            ChangeLightStatus(rightLight);
            var Lights = GetLightStatuses();
            Assert.AreEqual(Lights.Where(x => (x.IsLightOn == false && x.Description == LightType.RightLight.ToString())).Any(), true, "There should be two lights in the collection but there are {Lights.Count()}");

            rightLight.IsLightOn = true;
            ChangeLightStatus(rightLight);
            Lights = GetLightStatuses();
            Assert.AreEqual(Lights.Where(x => (x.IsLightOn == true && x.Description == LightType.RightLight.ToString())).Any(), true, "There should be two lights in the collection but there are {Lights.Count()}");

        }


        private void ChangeLightStatus(LightRestViewModel lightRestViewModel)
        {
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<LightsController>();
            var postRequest = SetLightStatus_PostRequest(lightRestViewModel);

           
            var request = restRouteHandler.HandleRequest(postRequest);

            
        }

        private List<LightRestViewModel> GetLightStatuses()
        {
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<LightsController>();
            var basicGet = GetMultipleLightStatuses_GetRequest();
            var request = restRouteHandler.HandleRequest(basicGet);

            var val = request.Result.Content.ToString();
            val = System.Text.Encoding.UTF8.GetString(request.Result.Content);
            var Lights = JsonConvert.DeserializeObject<List<LightRestViewModel>>(val);

            return Lights;
        }


        private RestUpHttpServerRequest GetMultipleLightStatuses_GetRequest()
        {
            RestUpHttpServerRequest basicGet = new RestUpHttpServerRequest()
            {
                Method = HttpMethod.GET,
                Uri = new Uri($"/lights/statuses?={DateTime.Now}", UriKind.RelativeOrAbsolute),
                AcceptMediaTypes = new[] { "application/json" },
                IsComplete = true
            };
            return basicGet;
        }


        private RestUpHttpServerRequest GetSingleLightStatuses_GetRequest()
        {
            RestUpHttpServerRequest basicGet = new RestUpHttpServerRequest()
            {
                Method = HttpMethod.GET,
                Uri = new Uri($"/lights/statuses?={DateTime.Now}", UriKind.RelativeOrAbsolute),
                AcceptMediaTypes = new[] { "application/json" },
                IsComplete = true
            };
            return basicGet;
        }


        private RestUpHttpServerRequest SetLightStatus_PostRequest(LightRestViewModel lightRestViewModel)
        {
            RestUpHttpServerRequest postRequest = new RestUpHttpServerRequest()
            {
                Method = HttpMethod.POST,
                Uri = new Uri($"/lights/statuses", UriKind.RelativeOrAbsolute),
                AcceptMediaTypes = new[] { "application/json" },
                IsComplete = true
            };

            postRequest.Content = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(lightRestViewModel));

            return postRequest;
        }

    }
}
