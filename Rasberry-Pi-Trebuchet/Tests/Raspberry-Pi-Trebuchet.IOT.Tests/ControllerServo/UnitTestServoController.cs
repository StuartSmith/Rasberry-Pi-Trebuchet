using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Newtonsoft.Json;
using Rasberry_Pi_Trebuchet.Common.RestViewModels;
using Raspberry_Pi_Trebuchet.Servos.Controllers.api;
using Raspberry_Pi_Trebuchet.Servos.RestViewModels;
using Restup.HttpMessage.Models.Schemas;
using Restup.Webserver.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.IOT.Tests.ControllerServo
{
    [TestClass]
    public class UnitTestServoController
    {
        [TestMethod]
        public void ServoTest_GetServerPosition()
        {
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<ServoController>();
            var basicGet = GetServoStatus_GetRequest();
            var request = restRouteHandler.HandleRequest(basicGet);

            var val = request.Result.Content.ToString();
            val = System.Text.Encoding.UTF8.GetString(request.Result.Content);
            var ServoPosition = JsonConvert.DeserializeObject<List<ServoRestViewModel>>(val);

        }

        private RestUpHttpServerRequest GetServoStatus_GetRequest()
        {
            RestUpHttpServerRequest basicGet = new RestUpHttpServerRequest()
            {
                Method = HttpMethod.GET,
                Uri = new Uri($"/servo/statuses?={DateTime.Now}", UriKind.RelativeOrAbsolute),
                AcceptMediaTypes = new[] { "application/json" },
                IsComplete = true
            };

            return basicGet;
        }
    }
}
