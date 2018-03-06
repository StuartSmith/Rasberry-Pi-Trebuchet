using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raspberry_Pi_Trebuchet.RestUp.Configuration.Controllers.api;
using Raspberry_Pi_Trebuchet.RestUp.Configuration.RestupHttpRequests;
using Restup.Webserver.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.Tests.IOT.ControllerConfiguration
{
    [TestClass]
    public class UnitTestConfigurationRestUp
    {
        [TestMethod]
        public void RetrieveConfiguration()
        {
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<PiConfigurationController>();

            var basicGet = HttpRequestsConfiguration.GetRequestPIConfigurationStatuses();           
            var request = restRouteHandler.HandleRequest(basicGet);

        }
    }
}
