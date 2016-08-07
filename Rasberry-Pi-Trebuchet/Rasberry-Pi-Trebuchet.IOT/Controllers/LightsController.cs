using Devkoes.Restup.WebServer.Attributes;
using Devkoes.Restup.WebServer.Models.Schemas;
using Rasberry_Pi_Trebuchet.Common.Implementations.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasberry_Pi_Trebuchet.IOT.Controllers
{
    [RestController(InstanceCreationType.Singleton)]
    class LightsController
    {


        [UriFormat("/lights/statuses")]
        public GetResponse LightStatuses()
        {
            var lightStatusServer = LightStatusServer.Instance;

            return new GetResponse(
                                    GetResponse.ResponseStatus.OK,
                                    lightStatusServer.RetrieveLightStatuses().Result);

        }

        [UriFormat("/lights/statuses/{description}")]
        public GetResponse LightStatuses(string description)
        {

            var lightStatusServer = LightStatusServer.Instance;

            return new GetResponse(
                                    GetResponse.ResponseStatus.OK,
                                    lightStatusServer.RetrieveLightStatus(description).Result);

        }
    }
}
