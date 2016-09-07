using Devkoes.Restup.WebServer.Attributes;
using Devkoes.Restup.WebServer.Models.Schemas;
using Devkoes.Restup.WebServer.Rest.Models.Contracts;
using Rasberry_Pi_Trebuchet.Common.Implementations.Server;
using Rasberry_Pi_Trebuchet.Common.ServiceContracts;
using Rasberry_Pi_Trebuchet.Common.ServiceContracts.DataContracts;
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

        [UriFormat("/lights/statuses")]
        public IPutResponse SetLightStatus([FromContent] Light data)
        {
            ILightStatus lightStatusServer = LightStatusServer.Instance;
            lightStatusServer.SetLight(data);

            return new PutResponse(PutResponse.ResponseStatus.OK);
        }

        [UriFormat("/lights/statuses/{description}")]
        public GetResponse LightStatuses(string description)
        {
            var lightStatusServer = LightStatusServer.Instance;

            var lights = lightStatusServer.RetrieveLightStatus(description).Result;

            if (lights.Count == 0)
                return new GetResponse(
                                   GetResponse.ResponseStatus.NotFound,
                                   lightStatusServer.RetrieveLightStatus(description).Result);

            return new GetResponse(
                                    GetResponse.ResponseStatus.OK,
                                    lightStatusServer.RetrieveLightStatus(description).Result);
        }
    }
}
