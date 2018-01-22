using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Rasberry_Pi_Trebuchet.Common.RestViewModels;
using Raspberry_Pi_Trebuchet.RestUp.Azure.Client;
using Raspberry_Pi_Trebuchet.RestUp.Azure.Controllers.api;
using Raspberry_Pi_Trebuchet.RestUp.Azure.Enums;
using Raspberry_Pi_Trebuchet.RestUp.Azure.RestupHttpRequests;
using Raspberry_Pi_Trebuchet.RestUp.Azure.RestViewModels;
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
            var restRouteHandler = AzureUnitTestHelper.SetupRestRouteHandlerForAzure();

            bool isRunning = AzureUnitTestHelper.IsMsgListenerRunning(restRouteHandler);
        }


        [TestMethod]
        public void AzureMsgListener_StartMsgListener()
        {
            RestRouteHandler restRouteHandler = AzureUnitTestHelper.SetupRestRouteHandlerForAzure();

            AzureUnitTestHelper.StartAzureMsgListener(restRouteHandler);

            OperationResult<bool> opResult;
            opResult = AzureUnitTestHelper.SendRestMSGtoDevice(restRouteHandler, HttpRequestsAzureMsgListener.PutRequest_StartAzureMsgListener());
            Assert.IsTrue(AzureUnitTestHelper.IsMsgListenerRunning(restRouteHandler), $"Message listener should be running, but it is not last operation result Message { opResult.Message}");
        }


        [TestMethod]
        public void AzureMsgListener_StopMsgListener()
        {
            var restRouteHandler = AzureUnitTestHelper.SetupRestRouteHandlerForAzure();

            AzureUnitTestHelper.StartAzureMsgListener(restRouteHandler);

            OperationResult<bool> opResult;
            opResult = AzureUnitTestHelper.SendRestMSGtoDevice(restRouteHandler, HttpRequestsAzureMsgListener.PutRequest_StopAzureMsgListener());

        }


        //[TestMethod]
        //public void AzureMsgListener_RegisterDevice()
        //{
        //    //configure Mock device for Azure Tests
        //    var restRouteHandler = AzureUnitTestHelper.SetupRestRouteHandlerForAzure();
        //    AzureUnitTestHelper.SetPIAzureConnectionString(restRouteHandler, AzureControllerTestData.IOTConnectionString());

        //    var basicPut = HttpRequestsAzureMsgListener.PutRequest_AzureMsgListenerRegisterDevice();
        //    var request = restRouteHandler.HandleRequest(basicPut);
        //    Assert.AreEqual(request.Result.ResponseStatus, HttpResponseStatus.OK, "Could not set Register Device bad HTTP Request ");

        //    var val = System.Text.Encoding.UTF8.GetString(request.Result.Content);
        //    var opResult = JsonConvert.DeserializeAnonymousType(val, new OperationResult<RegisterDeviceStatus>());

        //}


        //[TestMethod]
        //public void AzureMsgListener_TestSendMsgToDevice()
        //{
        //    var restRouteHandler = AzureUnitTestHelper.SetupRestRouteHandlerForAzure();
        //    AzureUnitTestHelper.SetPIAzureConnectionString(restRouteHandler, AzureControllerTestData.IOTConnectionString());

        //    AzureMsgListener_StartMsgListener();

        //    AzurePiConfiguration AzConfig = new AzurePiConfiguration();
        //    var deviceName = AzConfig.DeviceName;

        //    var sndmsgdevice = new SendMsgToDeviceThroughAzure(AzureControllerTestData.IOTConnectionString());

        //    ///Create the Message to send to the Device


        //    Task<MsgContentToAzure> msgcontentTask = sndmsgdevice.Send(deviceName, HttpRequestsTrebuchet.PostRequest_TrebuchetFire());
        //    msgcontentTask.Wait();

        //    MsgContentToAzure msgcontent = msgcontentTask.Result;

        //    Task twait = Task.Delay(500);
        //    twait.Wait();
        //}

    }
}

