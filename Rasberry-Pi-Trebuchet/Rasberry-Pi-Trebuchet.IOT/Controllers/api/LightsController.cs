using System;
using System.Linq;
using System.Threading.Tasks;
using Devkoes.Restup.WebServer.Attributes;
using Devkoes.Restup.WebServer.Models.Schemas;
using Devkoes.Restup.WebServer.Rest.Models.Contracts;
using Raspberry_Pi_Trebuchet.Lights.RestViewModels;
using Raspberry_Pi_Trebuchet.Lights.Interfaces;
using Rasberry_Pi_Trebuchet.IOT.Services;

namespace Rasberry_Pi_Trebuchet.IOT.Controllers.api
{
    [RestController(InstanceCreationType.Singleton)]
    public class LightsController 
    {
        public object LightStatuService { get; private set; }

        [UriFormat("/lights/statuses?={time}")]
        public async Task<GetResponse> GetStatuses(string time)
        {
            LightStatusService lightStatusService = LightStatusService.Instance;
          

            return new GetResponse(GetResponse.ResponseStatus.OK,
                                   await lightStatusService.RetrieveLightStatuses());
        }


        [UriFormat("/lights/statuses/{description}?={time}")]
        public async Task<GetResponse> GetStatuses(string description, string time)
        {
            LightStatusService lightStatusService = LightStatusService.Instance;
            var lights = await lightStatusService.RetrieveLightStatus(description);
            

            if (!(lights.Any()))
                return new GetResponse(GetResponse.ResponseStatus.NotFound, null);
                                   

            return new GetResponse(
                                    GetResponse.ResponseStatus.OK,
                                    lights);
        }

        [UriFormat("/lights/statuses")]
        public IPostResponse SetLightStatus([FromContent] LightRestViewModel data)
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
