using Devkoes.Restup.WebServer.Attributes;
using Devkoes.Restup.WebServer.Models.Schemas;
using Devkoes.Restup.WebServer.Rest.Models.Contracts;
using Rasberry_Pi_Trebuchet.Common.Models;
using Rasberry_Pi_Trebuchet.IOT.Services;
using Raspberry_Pi_Tribuchet.Sonic.RestViewModels;
using Raspberry_Pi_Tribuchet.Sonic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasberry_Pi_Trebuchet.IOT.Controllers.api
{
    [RestController(InstanceCreationType.Singleton)]
    class UltraSonicController
    {
        //[UriFormat("/ultrasonic/statuses?={time}")]       
        //public GetResponse GetStatuses(string time)
        //{

        //    var ultraSonic = UltraSonicSensorService.Instance;
        //    UltraSonicSensorRequest NonAzureRetrieval = new UltraSonicSensorRequest();
        //    var task = ultraSonic.RetrieveSensorsData(NonAzureRetrieval);
        //    task.Wait();

        //    return new GetResponse( GetResponse.ResponseStatus.OK, task.Result);

        //}

        [UriFormat("/ultrasonic/StartUltraSonicSensorProcessing")]
        public IPostResponse StartSonicSensorProcessing([FromContent] SonicRunStatusViewModel sonicRunRestStatusViewModel)
        {

            try
            {
                UltraSonicSensorService SonicService = UltraSonicSensorService.Instance;

               var t =  SonicService.StartSonicSensorProcessing(sonicRunRestStatusViewModel.NumberofSecondsSonicSensorRecordsDataFor);
               

                return new PostResponse(PostResponse.ResponseStatus.Created);

            }
            catch (Exception Exception)
            {
                return new PostResponse(PostResponse.ResponseStatus.Conflict);
            }


        }

        //[UriFormat("/ultrasonic/IsUltraSonicSensorProcessing?={time}")]
        //public GetResponse GetStatuses(string time)
        //{
        //}

    }
}
