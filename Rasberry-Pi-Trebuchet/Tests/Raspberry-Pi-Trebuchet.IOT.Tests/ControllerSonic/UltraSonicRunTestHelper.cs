using Newtonsoft.Json;
using Raspberry_Pi_Trebuchet.RestUp.Sonic.Models;
using Raspberry_Pi_Trebuchet.RestUp.Sonic.RestViewModels;
using Raspberry_Pi_Trebuchet.RestUp.Sonic.RetupHttpRequests;
using Raspberry_Pi_Trebuchet.RestUp.Sonic.Interfaces;
using Restup.HttpMessage;
using Restup.Webserver.Rest;
using System;
using System.Collections.Generic;
using Raspberry_Pi_Trebuchet.RestUp.Sonic.JsonParams;

namespace Raspberry_Pi_Trebuchet.Tests.IOT.ControllerSonic
{
    internal class UltraSonicRunTestHelper
    {
        internal static List<ViewModelUltraSonicSensorRun> DeserializedUltraSonicRuns(HttpServerResponse request)
        {
            var val = request.Content.ToString();
            val = System.Text.Encoding.UTF8.GetString(request.Content);
            var UltraSonicRuns = JsonConvert.DeserializeObject<List<ViewModelUltraSonicSensorRun>>(val);

            return UltraSonicRuns;
        }

        internal static bool IsUltraSonicRunning(RestRouteHandler restRouteHandler)
        {
            var basicGet = HttpRequestsSonic.GetRequest_IsSonicRunning();
            var request = restRouteHandler.HandleRequest(basicGet);
            var val = System.Text.Encoding.UTF8.GetString(request.Result.Content);

            var DefIsRunning = new { returnvalue = "" };
            var RetIsRunning = JsonConvert.DeserializeAnonymousType(val, DefIsRunning);

            return (Convert.ToBoolean(RetIsRunning.returnvalue));
        }

        internal static bool StartUltraSonicRun(RestRouteHandler restRouteHandler)
        {
            //Starts an Ultra Sonic run for x seconds....
            var basicPost = HttpRequestsSonic.PostRequest_StartRun(new UltraSonicRunRequest() { TimeInSecondsToRunSensor = 1 });
            var defRunStarted = new { runstarted = "" };
            var request = restRouteHandler.HandleRequest(basicPost);
            var val = System.Text.Encoding.UTF8.GetString(request.Result.Content);
            var RetHasStarted = JsonConvert.DeserializeAnonymousType(val, defRunStarted);

            return (Convert.ToBoolean(RetHasStarted.runstarted));
        }

    }
}
