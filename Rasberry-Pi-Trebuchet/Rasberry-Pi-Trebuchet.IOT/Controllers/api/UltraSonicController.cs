using Devkoes.Restup.WebServer.Attributes;
using Devkoes.Restup.WebServer.Models.Schemas;
using Devkoes.Restup.WebServer.Rest.Models.Contracts;
using Newtonsoft.Json;
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
        
        [UriFormat("ultrasonic/ultrasonicruns?={time}")]       
         public GetResponse GetUltraSonicRuns(string time)
        {
            var ultraSonicService = UltraSonicSensorService.Instance;
            var AllRuns = ultraSonicService.RetrieveAllRuns();
            string output = JsonConvert.SerializeObject(AllRuns);

            return new GetResponse(
                                  GetResponse.ResponseStatus.OK,
                                  new { AllRuns });
            
        }

    }
}
