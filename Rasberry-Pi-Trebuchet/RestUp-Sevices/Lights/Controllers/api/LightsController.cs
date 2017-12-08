using System;
using System.Linq;
using System.Threading.Tasks;
using Restup.Webserver.Attributes;
using Restup.Webserver.Models.Schemas;
using Restup.Webserver.Models.Contracts;
using Raspberry_Pi_Trebuchet.RestUp.Lights.RestViewModels;
using Raspberry_Pi_Trebuchet.RestUp.Lights.Interfaces;
using Raspberry_Pi_Trebuchet.RestUp.Lights.Services;

namespace Raspberry_Pi_Trebuchet.RestUp.Lights.Controllers.api
{
    [RestController(InstanceCreationType.Singleton)]
    public class LightsController 
    {
     

        [UriFormat("/lights/statuses?={time}")]
        public async Task<GetResponse> GetStatuses(string time)
        {
            try
            {
                LightStatusService lightStatusService = LightStatusService.Instance;
                return new GetResponse(GetResponse.ResponseStatus.OK,
                                       await lightStatusService.RetrieveLightStatuses());
            }
            catch (Exception ex)
            {
                return new GetResponse(GetResponse.ResponseStatus.NotFound);
            }
        }


        [UriFormat("/lights/statuses/{description}?={time}")]
        public async Task<GetResponse> GetStatuses(string description, string time)
        {
            try { 
            LightStatusService lightStatusService = LightStatusService.Instance;
            var lights = await lightStatusService.RetrieveLightStatus(description);
            

            if (!(lights.Any()))
                return new GetResponse(GetResponse.ResponseStatus.NotFound, null);
                                   

            return new GetResponse(
                                    GetResponse.ResponseStatus.OK,
                                    lights);
            }
            catch (Exception ex)
            {
                return new GetResponse(GetResponse.ResponseStatus.NotFound);
            }
        }

        [UriFormat("/lights/statuses")]
        public IPostResponse SetLightStatus([FromContent] LightRestViewModel data)
        {
            try
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
            catch (Exception ex)
            {
                return new PostResponse(PostResponse.ResponseStatus.Conflict);
            }
        }



    }
}
