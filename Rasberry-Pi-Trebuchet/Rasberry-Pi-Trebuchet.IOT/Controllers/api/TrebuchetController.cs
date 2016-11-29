using Devkoes.Restup.WebServer.Attributes;
using Devkoes.Restup.WebServer.Models.Schemas;
using Devkoes.Restup.WebServer.Rest.Models.Contracts;
using Raspberry_Pi_Tribuchet.Tribuchet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.IOT.Controllers.api
{
    [RestController(InstanceCreationType.Singleton)]
    public class TrebuchetController
    {
        private object runrequest;

        [UriFormat("trebuchet/fire")]
        public IPostResponse Fire()
        {
            TrebuchetService trebuchetService = new TrebuchetService();
            bool trebuchetResult = trebuchetService.FireTrebuchet();

            return new PostResponse(PostResponse.ResponseStatus.Created, $"/Servo/statuses", new { trebuchetResult });

        }

        [UriFormat("trebuchet/reset")]
        public IPostResponse Reset()
        {
            TrebuchetService trebuchetService = new TrebuchetService();
            bool trebuchetResult = trebuchetService.ResetTrebuchet();

            return new PostResponse(PostResponse.ResponseStatus.Created, $"/Servo/statuses", new { trebuchetResult });
        }

    }
}
