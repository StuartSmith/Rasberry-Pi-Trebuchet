using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Newtonsoft.Json;
using Rasberry_Pi_Trebuchet.Common.RestViewModels;
using Raspberry_Pi_Trebuchet.RestUp.Azure.Controllers.api;
using Raspberry_Pi_Trebuchet.RestUp.Azure.Enums;
using Raspberry_Pi_Trebuchet.RestUp.Azure.RestupHttpRequests;
using Raspberry_Pi_Trebuchet.RestUp.Azure.Services;
using Raspberry_Pi_Trebuchet.RestUp.Common.RestViewModels;
using Raspberry_Pi_Trebuchet.RestUp.Configuration.Controllers.api;
using Raspberry_Pi_Trebuchet.RestUp.Configuration.RestupHttpRequests;
using Raspberry_Pi_Trebuchet.RestUp.Configuration.Services;
using Raspberry_Pi_Trebuchet.RestUp.Servos.RestupHttpRequests;
using Raspberry_Pi_Trebuchet.RestUp.Trebuchet.RetupHttpRequests;
using Restup.HttpMessage.Models.Schemas;
using Restup.Webserver.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.Tests.IOT.ControllerAzure
{
    [TestClass]
    public class UnitTestAzureController
    {
        [TestMethod]
        public void AzureMsgListener_GetMsgListenerStatus()
        {
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<AzureMsgController>();

            bool isRunning = IsMsgListenerRunning(restRouteHandler);
        }


        [TestMethod]
        public void AzureMsgListener_StartMsgListener()
        {
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<AzureMsgController>();

            OperationResult<bool> opResult;
            if (IsMsgListenerRunning(restRouteHandler))
            {
                opResult = MsgListenerAction(restRouteHandler, HttpRequestsAzureMsgListener.PutRequest_AzureMsgListenerStop());
                Task.Delay(100);
            }

            opResult = MsgListenerAction(restRouteHandler, HttpRequestsAzureMsgListener.PutRequest_AzureMsgListenerStart());
            Assert.IsTrue(IsMsgListenerRunning(restRouteHandler), $"Message listener should be running, but it is not last operation result Message { opResult.Message}");
        }


        [TestMethod]
        public void AzureMsgListener_StopMsgListener()
        {
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<AzureMsgController>();

            OperationResult<bool> opResult;
            //if the msglistener is not running start it
            if (!(IsMsgListenerRunning(restRouteHandler)))
            {
                opResult = MsgListenerAction(restRouteHandler, HttpRequestsAzureMsgListener.PutRequest_AzureMsgListenerStart());
                Task.Delay(100);
            }
            opResult = MsgListenerAction(restRouteHandler, HttpRequestsAzureMsgListener.PutRequest_AzureMsgListenerStop());

        }


        [TestMethod]
        public void AzureMsgListener_RegisterDevice()
        {
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<AzureMsgController>();
            restRouteHandler.RegisterController<PiConfigurationController>();

            //Set the pi connection string value
            RestUpHttpServerRequest basicPut = HttpRequestsConfiguration.PostReqestPiConfigurationStatus("AzureIOTConnectionString", $"\"{AzureControllerTestData.IOTConnectionString()}\"");
            var request = restRouteHandler.HandleRequest(basicPut);

            Assert.AreEqual(request.Result.ResponseStatus, HttpResponseStatus.OK, "Could not set AzureIOTConnectionString ");

            basicPut = HttpRequestsAzureMsgListener.PutRequest_AzureMsgListenerRegister();
            request = restRouteHandler.HandleRequest(basicPut);

            Assert.AreEqual(request.Result.ResponseStatus, HttpResponseStatus.OK, "Could not set Register Device bad HTTP Request ");

            var val = System.Text.Encoding.UTF8.GetString(request.Result.Content);
            var opResult = JsonConvert.DeserializeAnonymousType(val, new OperationResult<RegisterDeviceStatus>());

        }


        [TestMethod]
        public void AzureMsgListener_DeviceListener()
        {
            AzureMsgListener_StartMsgListener();

            Task t = Task.Delay(1000);
            t.Wait();

            var sndmsgdevice = SendMsgToDevice.Instance;
            sndmsgdevice.SetConnectionString(AzureControllerTestData.IOTConnectionString());
            AzurePiConfiguration AzConfig = new AzurePiConfiguration();

            t = sndmsgdevice.Send(AzConfig.DeviceName, HttpRequestsTrebuchet.PostRequest_TrebuchetFire());
            t.Wait();
        }


        private bool  IsMsgListenerRunning(RestRouteHandler restRouteHandler)
        {
          
            var basicGet = HttpRequestsAzureMsgListener.GetRequest_AzureMsgListenerStatus();
            var request = restRouteHandler.HandleRequest(basicGet);
            var val = System.Text.Encoding.UTF8.GetString(request.Result.Content);            
            var RetIsRunning = JsonConvert.DeserializeAnonymousType(val, new { IsAzureMsgListenerRunning = "" });

            return (Convert.ToBoolean(RetIsRunning.IsAzureMsgListenerRunning));         
        }


        private OperationResult<bool> MsgListenerAction(RestRouteHandler restRouteHandler, RestUpHttpServerRequest basicPut)
        {
            var request = restRouteHandler.HandleRequest(basicPut);
            var val = System.Text.Encoding.UTF8.GetString(request.Result.Content);           
            var opResult = JsonConvert.DeserializeAnonymousType(val, new OperationResult<bool>());

            return opResult;
        }        
    }
}
