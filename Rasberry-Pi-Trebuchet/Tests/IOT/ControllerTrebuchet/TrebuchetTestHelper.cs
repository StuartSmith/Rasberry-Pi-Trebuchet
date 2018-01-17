using Newtonsoft.Json;
//using Raspberry_Pi_Trebuchet.RestUp.Trebuchet.RetupHttpRequests;
//using Raspberry_Pi_Trebuchet.Tests.IOT.ControllerSonic;
using Restup.Webserver.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.Tests.IOT.ControllerTrebuchet
{
    internal class TrebuchetTestHelper
    {
        //internal static bool ResetTrebuchet(RestRouteHandler restRouteHandler)
        //{
        //    var basicPost = HttpRequestsTrebuchet.PostRequest_TrebuchetReset();
        //    var request = restRouteHandler.HandleRequest(basicPost);
        //    var val = System.Text.Encoding.UTF8.GetString(request.Result.Content);

        //    var definitionResetTrebuchet = new { trebuchetResult = "" };
        //    var ResetTrebuchet = JsonConvert.DeserializeAnonymousType(val, definitionResetTrebuchet);

        //    return (ResetTrebuchet.trebuchetResult.ToUpper() =="true".ToUpper());
        //}


        //internal static bool FireTrebuchet(RestRouteHandler restRouteHandler)
        //{
        //    var basicPost = HttpRequestsTrebuchet.PostRequest_TrebuchetFire();
        //    var request = restRouteHandler.HandleRequest(basicPost);
        //    var val = System.Text.Encoding.UTF8.GetString(request.Result.Content);

        //    var definitionFireTrebuchet = new { trebuchetResult = "" };
        //    var FireTrebuchet = JsonConvert.DeserializeAnonymousType(val, definitionFireTrebuchet);

        //    return (FireTrebuchet.trebuchetResult.ToUpper() == "true".ToUpper());         
        //}


        //internal static bool WaitForTrebuchetToFinishRunning(RestRouteHandler restRouteHandler, int SecondsToWaitBeforeReturning)
        //{
        //    var Isrunning = UltraSonicRunTestHelper.IsUltraSonicRunning(restRouteHandler);
        //    int LoopUntil = 0;
        //    while (Isrunning && LoopUntil < SecondsToWaitBeforeReturning)
        //    {
        //        Task.Delay(1000).Wait();
        //        Isrunning = UltraSonicRunTestHelper.IsUltraSonicRunning(restRouteHandler);
        //        LoopUntil++;
        //    }
        //    bool trebuchetFinishedRunning = Isrunning ? false : true;

        //    return !(trebuchetFinishedRunning);
        //}
    }

}
