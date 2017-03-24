using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Newtonsoft.Json;
using Rasberry_Pi_Trebuchet.Common.RestViewModels;
using Raspberry_Pi_Trebuchet.Common.Enums;
using Raspberry_Pi_Trebuchet.RestUp.Servos.Controllers.api;
using Raspberry_Pi_Trebuchet.RestUp.Servos.Enums;
using Raspberry_Pi_Trebuchet.RestUp.Servos.RestViewModels;
using Raspberry_Pi_Trebuchet.RestUp.Servos.RestupHttpRequests;
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
            var ServoPosition = GetServoPostions();
            Assert.AreEqual(ServoPosition[0].ServoGPIO, RaspberryPiGPI0Pin.GPIO13, "Servo GPIO Pin should be GPIO 13");
        }

        private List<ServoRestViewModel> GetServoPostions()
        {
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<ServoController>();
            var basicGet = HttpRequestsServo.GetRequestServoStatus();
            var request = restRouteHandler.HandleRequest(basicGet);

            var val = request.Result.Content.ToString();
            val = System.Text.Encoding.UTF8.GetString(request.Result.Content);
            var ServoPosition = JsonConvert.DeserializeObject<List<ServoRestViewModel>>(val);

            return ServoPosition;
        }


       

        [TestMethod]
        public void ServoTest_SetServerPosition()
        {
            Test_SetServerPosition(ServoWhereAbouts.NinetyDegrees);
            Test_SetServerPosition(ServoWhereAbouts.OneEightyDegrees);
            Test_SetServerPosition(ServoWhereAbouts.ZeroDegrees);

        }


        private void Test_SetServerPosition(ServoWhereAbouts servoWhereAbouts)
        {
            //Set the servo Status
            ChangeServoStatus(new ServoRestViewModel()
            {
                ServoStatus = servoWhereAbouts.ToString(),
                Description = ServoType.LaunchServo.ToString()
            });
            //Retrieve the Servo Status
            var ServoPosition = GetServoPostions();
            Assert.AreEqual(ServoPosition[0].ServoStatus, servoWhereAbouts.ToString(), $"Servo Status should be {servoWhereAbouts.ToString()} , but was {ServoPosition[0].ServoStatus}");


        }


        private void ChangeServoStatus(ServoRestViewModel content)
        {
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<ServoController>();
            var postRequest = HttpRequestsServo.PostRequestSetServerPosition(content);

            var request = restRouteHandler.HandleRequest(postRequest);


        }

      
    }
}
