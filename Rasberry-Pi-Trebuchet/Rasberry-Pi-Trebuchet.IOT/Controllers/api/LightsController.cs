using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devkoes.Restup.WebServer.Attributes;
using Devkoes.Restup.WebServer.Models.Schemas;
using Devkoes.Restup.WebServer.Rest.Models.Contracts;
using Rasberry_Pi_Trebuchet.IOT.Services;
using Rasberry_Pi_Trebuchet.Common.ServiceContracts.DataContracts;
using Rasberry_Pi_Trebuchet.Common.ServiceContracts;

namespace Rasberry_Pi_Trebuchet.IOT.Controllers.api
{
    [RestController(InstanceCreationType.Singleton)]
    public class LightsController 
    {
        public object LightStatuService { get; private set; }

        [UriFormat("/lights/statuses?={time}")]
        public GetResponse GetStatuses(string time)
        {
            LightStatusService lightStatusService = LightStatusService.Instance;
            var task = lightStatusService.RetrieveLightStatuses();
            task.Wait();

            return new GetResponse(
                                   GetResponse.ResponseStatus.OK,
                                   lightStatusService.RetrieveLightStatuses().Result);
        }


        [UriFormat("/lights/statuses/{description}?={time}")]
        public GetResponse GetStatuses(string description, string time)
        {
            LightStatusService lightStatusService = LightStatusService.Instance;
            var task = lightStatusService.RetrieveLightStatus(description);
            task.Wait();

            if (!(task.Result.Any()))
                return new GetResponse(GetResponse.ResponseStatus.NotFound, null);
                                   

            return new GetResponse(
                                    GetResponse.ResponseStatus.OK,
                                    task.Result);
        }

        [UriFormat("/lights/statuses")]
        public IPostResponse SetLightStatus([FromContent] Light data)
        {
            ILightStatus lightStatusServer = LightStatusService.Instance;
            var task = lightStatusServer.RetrieveLightStatus(data.Description);
            task.Wait();

            if (!(task.Result.Any()))
                return new PostResponse(PostResponse.ResponseStatus.Conflict);
                //return base.BadRequest();

            lightStatusServer.SetLight(data);

            return new PostResponse(PostResponse.ResponseStatus.Created, $"/lights/statuses/{data.LightPosition}", data);
                
        }



    }
}
