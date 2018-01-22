
using Raspberry_Pi_Trebuchet.RestUp.Tribuchet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restup.Webserver.Attributes;
using Restup.Webserver.Models.Schemas;
using Restup.Webserver.Models.Contracts;

namespace Raspberry_Pi_Trebuchet.RestUp.Trebuchet.Controllers.api
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
