using Devkoes.Restup.WebServer.Attributes;
using Devkoes.Restup.WebServer.Models.Schemas;
using Devkoes.Restup.WebServer.Rest.Models.Contracts;
using Rasberry_Pi_Trebuchet.IOT.Services;
using System;
using Raspberry_Pi_Trebuchet.Servos.Models;

namespace Rasberry_Pi_Trebuchet.IOT.Controllers.api
{
    [RestController(InstanceCreationType.Singleton)]
    public class ServoController
    {
        [UriFormat("/servo/statuses?={time}")]
        public GetResponse GetStatuses(string time)
        {
            ServoStatusService servoStatusServer = ServoStatusService.Instance;
            var task = servoStatusServer.RetrieveServos();
            task.Wait();


            return new GetResponse(
                                   GetResponse.ResponseStatus.OK,
                                   task.Result);
        }


        [UriFormat("/servo/statuses")]
        public IPostResponse SetServoStatus([FromContent] Servo servo)
        {

            ServoStatusService servoStatusServer = ServoStatusService.Instance;
            var task = servoStatusServer.SetServo(servo);
            task.Wait();

            return new PostResponse(PostResponse.ResponseStatus.Created, $"/Servo/statuses", task.Result);

        }

        [UriFormat("/servo/servopositions")]
        public GetResponse GetServoStatuses()
        {
            ServoStatusService servoStatusServer = ServoStatusService.Instance;

            return new GetResponse(
                             GetResponse.ResponseStatus.OK,
                             servoStatusServer.ServoStatuses);
    
        }
    }
}
