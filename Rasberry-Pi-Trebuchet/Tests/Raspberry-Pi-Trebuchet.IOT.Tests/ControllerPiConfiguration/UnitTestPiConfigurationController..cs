using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Newtonsoft.Json;
using Rasberry_Pi_Trebuchet.Common.RestViewModels;
using Raspberry_Pi_Trebuchet.Configuration.Controllers.api;
using Raspberry_Pi_Trebuchet.Configuration.RestViewModels;
using Raspberry_Pi_Trebuchet.Configuration.Services;
using Restup.HttpMessage.Models.Schemas;
using Restup.Webserver.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Raspberry_Pi_Trebuchet.IOT.Tests.ControllerPiConfiguration
{
    /// <summary>
    /// Class for  test the piconfiguration service
    /// 
    ///  Something to NOTE is that the calling from code the "api" in
    /// /api/piconfiguration is not used, instead only use the route
    /// /piconfiguration ... All routes must be in lower case Restup
    /// is case sensitive.
    /// </summary>

    [TestClass]
    public class UnitTestPiConfigurationController
    { 

        /// <summary>
        /// Retrieve the list pi configuration data from the PiConfiguration 
        /// service.       
        /// </summary>
        [TestMethod]
        public void GetPIConfigurationStatuses()
        {
            //Create the Rest Rout handler to process the request
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<PiConfigurationController>();

            //Send the Request to the route Handler
            RestUpHttpServerRequest basicGet =  GetPIConfigurationStatuses_GetRequest();
            var request = restRouteHandler.HandleRequest(basicGet);

            //Deserialize the returned Values
            var val = request.Result.Content.ToString();
            val = System.Text.Encoding.UTF8.GetString(request.Result.Content);
            var PiConfigurations = JsonConvert.DeserializeObject<List<ViewModelRestNameValuePair>>(val);


            AzurePiConfiguraton azPiConfig = new AzurePiConfiguraton();
            GetPIConfigurationStatusesValueCheck(nameof(azPiConfig.AllowSendingofData), PiConfigurations);
            GetPIConfigurationStatusesValueCheck(nameof(azPiConfig.AllowSendingToastLightData), PiConfigurations);
            GetPIConfigurationStatusesValueCheck(nameof(azPiConfig.AllowSendingToastServoData), PiConfigurations);
            GetPIConfigurationStatusesValueCheck(nameof(azPiConfig.AllowSendingUltraSonicData), PiConfigurations);
            GetPIConfigurationStatusesValueCheck(nameof(azPiConfig.AzureIOTConnectionString), PiConfigurations);
            GetPIConfigurationStatusesValueCheck(nameof(azPiConfig.ToastWebSendURL), PiConfigurations);

        }

        private RestUpHttpServerRequest GetPIConfigurationStatuses_GetRequest()
        {
            RestUpHttpServerRequest basicGet = new RestUpHttpServerRequest()
            {
                Method = HttpMethod.GET,
                Uri = new Uri($"/piconfiguration?={DateTime.Now}", UriKind.RelativeOrAbsolute),
                AcceptMediaTypes = new[] { "application/json" },
                IsComplete = true
            };

            return basicGet;
        }


        private void GetPIConfigurationStatusesValueCheck(string ValueToCheckAgainst, List<ViewModelRestNameValuePair> PiConfigurations)
        {
            Assert.AreEqual(PiConfigurations.Where(x => x.name.ToUpper() == ValueToCheckAgainst.ToUpper()).Any(), true, $"Could not find configuration value '{ValueToCheckAgainst}'");
        }

        /// <summary>
        /// Tests the Getting and setting of the PI Configuration 
        /// Status code.
        /// </summary>
        [TestMethod]
        public void SetGetPIConfigurationStatus_AllowSendingofData()
        {
            //Create the Rest Rout handler to process the request
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<PiConfigurationController>();

            //Send the Request to the route Handler
            RestUpHttpServerRequest basicPut;
            basicPut = SetPIConfigurationStatus_AllowSendingofData_PutRequest();
            var request = restRouteHandler.HandleRequest(basicPut);
            Assert.AreEqual(request.Result.ResponseStatus, HttpResponseStatus.OK, "Failed to Set  AllowSendingofData to false");

            basicPut.Content = Encoding.UTF8.GetBytes("\"true\"");
            request = restRouteHandler.HandleRequest(basicPut);
            Assert.AreEqual(request.Result.ResponseStatus, HttpResponseStatus.OK, "Failed to Set AllowSendingofData to true");

            RestUpHttpServerRequest basicGet = SetPIConfigurationStatus_AllowSendingofData_GetRequest();
            request = restRouteHandler.HandleRequest(basicGet);

            //Deserialize the returned Values
            var val = request.Result.Content.ToString();
            val = System.Text.Encoding.UTF8.GetString(request.Result.Content);
            String piConfigValue =(String) JsonConvert.DeserializeObject(val,typeof(String));

            Assert.AreEqual(piConfigValue.ToUpper(), "true".ToUpper(), "AllowSendingofData should be true after test run");
        }


        /// <summary>
        /// Creates the Get Response for the PI Configuration Service
        /// </summary>
        /// <returns></returns>
        private RestUpHttpServerRequest SetPIConfigurationStatus_AllowSendingofData_GetRequest()
        {
            RestUpHttpServerRequest basicGet = new RestUpHttpServerRequest()
            {
                Method = HttpMethod.GET,
                Uri = new Uri($"/piconfiguration/AllowSendingofData?={DateTime.Now}", UriKind.RelativeOrAbsolute),
                AcceptMediaTypes = new[] { "application/json" },
                IsComplete = true
            };
            return basicGet;
        }


        /// <summary>
        /// Creates the Put Response for the SetPIConfigurationStatus 
        /// Configuration Service Tests
        /// </summary>
        private RestUpHttpServerRequest SetPIConfigurationStatus_AllowSendingofData_PutRequest()
        {
            RestUpHttpServerRequest basicPut = new RestUpHttpServerRequest()
            {
                Method = HttpMethod.PUT,
                Uri = new Uri($"/piconfiguration/AllowSendingofData", UriKind.RelativeOrAbsolute),
                AcceptMediaTypes = new[] { "application/json" },
                Content = Encoding.UTF8.GetBytes("\"false\""),
                IsComplete = true
            };
            return basicPut;
        }
    }
}
