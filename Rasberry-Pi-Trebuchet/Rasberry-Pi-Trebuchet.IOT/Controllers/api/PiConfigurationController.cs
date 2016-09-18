using Devkoes.Restup.WebServer.Attributes;
using Devkoes.Restup.WebServer.Models.Schemas;
using Devkoes.Restup.WebServer.Rest.Models.Contracts;
using Rasberry_Pi_Trebuchet.Common.Models;
using Rasberry_Pi_Trebuchet.IOT.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasberry_Pi_Trebuchet.IOT.Controllers.api
{
    [RestController(InstanceCreationType.Singleton)]
    class PiConfigurationController
    {
        [UriFormat("/piconfiguration/status?={time}")]
        public GetResponse GetStatus(string time)
        {
            var azureConnectionService = AzureConnectionService.Instance;
            azureConnectionService.GetAzureConfiguration();
            return new GetResponse(GetResponse.ResponseStatus.OK,
                                azureConnectionService.GetAzureConfiguration());
        }

        [UriFormat("/piconfiguration/status")]
        public IPostResponse SetStatuses([FromContent] AzurePiConfiguraton data)
        {
            var azureConnectionService = AzureConnectionService.Instance;
            azureConnectionService.SetAzureConfiguration(data);
            return new PostResponse(PostResponse.ResponseStatus.Created, $"/piconfiguration/status", data);
        }

    }

}


