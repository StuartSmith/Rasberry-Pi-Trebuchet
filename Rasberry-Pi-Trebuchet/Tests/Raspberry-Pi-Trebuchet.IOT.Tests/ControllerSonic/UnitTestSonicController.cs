using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Newtonsoft.Json;
using Raspberry_Pi_Trebuchet.IOT.Controllers.api;
using Raspberry_Pi_Trebuchet.Sonic.Models;
using Raspberry_Pi_Tribuchet.Sonic.RetupHttpRequests;
using Restup.Webserver.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.IOT.Tests.ControllerSonic
{
    [TestClass]
    public class UnitTestSonicController
    {
        [TestMethod]
        public void UltraSonicTest_StartUltraSonicRun()
        {
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<UltraSonicController>();

            var Isrunning = IsUltraSonicRunning(restRouteHandler);
            Assert.IsFalse(Isrunning);

            var RunHasStarted = StartUltraSonicRun(restRouteHandler);
            Assert.IsTrue(RunHasStarted);

            Task.Delay(2000).Wait();
            Isrunning = IsUltraSonicRunning(restRouteHandler);
            while (Isrunning)
            {
                Isrunning = IsUltraSonicRunning(restRouteHandler);
                Task.Delay(2000).Wait();
            }
            Assert.IsFalse(Isrunning);



        }


        private bool IsUltraSonicRunning(RestRouteHandler restRouteHandler)
        {
            var basicGet = HttpRequestsSonic.GetRequestIsSonicRunning();
            var request = restRouteHandler.HandleRequest(basicGet);
            var val = System.Text.Encoding.UTF8.GetString(request.Result.Content);

            var DefIsRunning = new { returnvalue = "" };
            var RetIsRunning = JsonConvert.DeserializeAnonymousType(val, DefIsRunning);

            return( Convert.ToBoolean(RetIsRunning.returnvalue));
        }

        private bool StartUltraSonicRun(RestRouteHandler restRouteHandler)
        {
            var basicPost = HttpRequestsSonic.PostRequestStartRun(new UltraSonicRunRequest() { TimeInSecondsToRunSensor = 1 });
            var defRunStarted = new { runstarted = "" };
            var request = restRouteHandler.HandleRequest(basicPost);
            var val = System.Text.Encoding.UTF8.GetString(request.Result.Content);
            var RetHasStarted = JsonConvert.DeserializeAnonymousType(val, defRunStarted);

            return (Convert.ToBoolean(RetHasStarted.runstarted));
        }
    }
}
