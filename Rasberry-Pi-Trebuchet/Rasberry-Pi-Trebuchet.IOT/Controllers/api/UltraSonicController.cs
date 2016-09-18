using Devkoes.Restup.WebServer.Attributes;
using Devkoes.Restup.WebServer.Models.Schemas;
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
    class UltraSonicController
    {
        [UriFormat("/ultrasonic/statuses?={time}")]       
        public GetResponse GetStatuses(string time)
        {

            var ultraSonic = UltraSonicSensorService.Instance;
            UltraSonicSensorRequest NonAzureRetrieval = new UltraSonicSensorRequest();
            var task = ultraSonic.RetrieveSensorsData(NonAzureRetrieval);
            task.Wait();

            return new GetResponse( GetResponse.ResponseStatus.OK, task.Result);

        }
    }
}
