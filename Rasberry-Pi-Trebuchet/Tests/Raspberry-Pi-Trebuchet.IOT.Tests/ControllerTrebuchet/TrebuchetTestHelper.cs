using Newtonsoft.Json;
using Raspberry_Pi_Trebuchet.RestUp.Trebuchet.RetupHttpRequests;
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
        internal static bool ResetTrebuchet(RestRouteHandler restRouteHandler)
        {
            var basicPost = HttpRequestsTrebuchet.PostRequest_TrebuchetReset();
            var request = restRouteHandler.HandleRequest(basicPost);
            var val = System.Text.Encoding.UTF8.GetString(request.Result.Content);

            var DefIsRunning = new { trebuchetResult = "" };
            var RetIsRunning = JsonConvert.DeserializeAnonymousType(val, DefIsRunning);

            return (RetIsRunning.trebuchetResult.ToUpper() =="true".ToUpper());
        }
    }
}
