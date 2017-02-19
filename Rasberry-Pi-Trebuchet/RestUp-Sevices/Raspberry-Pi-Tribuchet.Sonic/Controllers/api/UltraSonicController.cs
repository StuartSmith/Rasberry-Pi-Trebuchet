
using Newtonsoft.Json;
using Raspberry_Pi_Trebuchet.Common.Models;

using Raspberry_Pi_Trebuchet.Sonic.Models;
using Raspberry_Pi_Trebuchet.Sonic.RestViewModels;
using Raspberry_Pi_Trebuchet.Sonic.Services;
using Restup.Webserver.Attributes;
using Restup.Webserver.Models.Contracts;
using Restup.Webserver.Models.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Raspberry_Pi_Trebuchet.IOT.Controllers.api
{
    [RestController(InstanceCreationType.Singleton)]
    public class UltraSonicController
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


        [UriFormat("ultrasonic/isultrasonicrunning?={time}")]
        public  GetResponse IsUltraSonicRunning(string time)
        {
            var ultraSonicService = UltraSonicSensorService.Instance;
            bool returnvalue = ultraSonicService.IsUltraSonicServiceRunning();
            return new GetResponse(
                                 GetResponse.ResponseStatus.OK,
                                 new { returnvalue });

        }


        [UriFormat("ultrasonic/ultrasonicruns/{id}?={time}")]
        public GetResponse GetStatus(long id, string time)
        {
            var ultraSonicService = UltraSonicSensorService.Instance;
            var RunSpecified = ultraSonicService.RetrieveUltraSonicRun(id);
            if (RunSpecified == null)
                return new GetResponse(GetResponse.ResponseStatus.NotFound);

            return new GetResponse(
                               GetResponse.ResponseStatus.OK,
                               new { RunSpecified });
        }


        [UriFormat("ultrasonic/lastrun?={time}")]
        public GetResponse LastRun(string time)
        {
            var ultraSonicService = UltraSonicSensorService.Instance;
            var lastRun = ultraSonicService.RetrieveLatestUltraSonicRun();
            
            return new GetResponse(
                              GetResponse.ResponseStatus.OK,
                              new { lastRun });
        }


        [UriFormat("ultrasonic/startrun")]
        public IPostResponse StartRun([FromContent] UltraSonicRunRequest runrequest)
        {
            var ultraSonicService = UltraSonicSensorService.Instance;
            bool runstarted = ultraSonicService.StartUltraSonicRun(runrequest); 
            return new PostResponse(PostResponse.ResponseStatus.Created, "", new { runstarted });
        }


        /// <summary>
        /// Needs to add Delete to the ultra sonic sensor run
        /// </summary>
        /// removeultrasonicruns
        [UriFormat("ultrasonic/removeultrasonicruns")]
        public IDeleteResponse RemoveUltraSonicRuns()
        {
            var ultraSonicService = UltraSonicSensorService.Instance;
            long RunRemovalReturned = ultraSonicService.RemoveAllUltraSonicRuns();
            if (RunRemovalReturned == 0) 
                return new DeleteResponse(DeleteResponse.ResponseStatus.NoContent);
            else
                return new DeleteResponse(DeleteResponse.ResponseStatus.OK);
        }

    }
}
