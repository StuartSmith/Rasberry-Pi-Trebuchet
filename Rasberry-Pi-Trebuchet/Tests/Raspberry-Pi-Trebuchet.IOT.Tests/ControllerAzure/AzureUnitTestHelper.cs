using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Newtonsoft.Json;
using Rasberry_Pi_Trebuchet.Common.RestViewModels;
using Raspberry_Pi_Trebuchet.RestUp.Azure.Controllers.api;
using Raspberry_Pi_Trebuchet.RestUp.Azure.RestupHttpRequests;
using Raspberry_Pi_Trebuchet.RestUp.Common.RestViewModels;
using Raspberry_Pi_Trebuchet.RestUp.Configuration.Controllers.api;
using Raspberry_Pi_Trebuchet.RestUp.Configuration.RestupHttpRequests;
using Restup.HttpMessage.Models.Schemas;
using Restup.Webserver.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.Tests.IOT.ControllerAzure
{
    class AzureUnitTestHelper
    {
        internal static bool IsMsgListenerRunning(RestRouteHandler restRouteHandler)
        {

            var basicGet = HttpRequestsAzureMsgListener.GetRequest_AzureMsgListenerStatus();
            var request = restRouteHandler.HandleRequest(basicGet);
            var val = System.Text.Encoding.UTF8.GetString(request.Result.Content);
            var RetIsRunning = JsonConvert.DeserializeAnonymousType(val, new { IsAzureMsgListenerRunning = "" });

            return (Convert.ToBoolean(RetIsRunning.IsAzureMsgListenerRunning));
        }

        internal static OperationResult<bool> SendRestMSGtoDevice(RestRouteHandler restRouteHandler, RestUpHttpServerRequest restMSG)
        {
            var request = restRouteHandler.HandleRequest(restMSG);
            var val = System.Text.Encoding.UTF8.GetString(request.Result.Content);
            var opResult = JsonConvert.DeserializeAnonymousType(val, new OperationResult<bool>());

            return opResult;
        }

        internal static void SetPIAzureConnectionString(RestRouteHandler restRouteHandler, string IOTConnectionString)
        {
            RestUpHttpServerRequest basicPut = HttpRequestsConfiguration.PostReqestPiConfigurationStatus("AzureIOTConnectionString", $"\"{IOTConnectionString}\"");
            var request = restRouteHandler.HandleRequest(basicPut);
            Assert.AreEqual(request.Result.ResponseStatus, HttpResponseStatus.OK, "Could not set AzureIOTConnectionString ");
        }

        internal static RestRouteHandler SetupRestRouteHandlerForAzure()
        {
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<AzureMsgController>();
            restRouteHandler.RegisterController<PiConfigurationController>();

            return restRouteHandler;
        }

        internal static void StartAzureMsgListener(RestRouteHandler restRouteHandler)
        {
            OperationResult<bool> opResult;
            if (AzureUnitTestHelper.IsMsgListenerRunning(restRouteHandler))
            {
                opResult = AzureUnitTestHelper.SendRestMSGtoDevice(restRouteHandler, HttpRequestsAzureMsgListener.PutRequest_StopAzureMsgListener());
                Task.Delay(100);
            }
        }

        

     
    }
}
