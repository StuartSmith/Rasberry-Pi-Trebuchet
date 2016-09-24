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
            try
            {
                ServoStatusService servoStatusServer = ServoStatusService.Instance;
            var task = servoStatusServer.RetrieveServos();
            task.Wait();


            return new GetResponse(
                                   GetResponse.ResponseStatus.OK,
                                   task.Result);

            }
            catch (Exception ex)
            {
                return new GetResponse(GetResponse.ResponseStatus.OK);
            }

        }


        [UriFormat("/servo/statuses")]
        public IPostResponse SetServoStatus([FromContent] Servo servo)
        {
            try
            {
                ServoStatusService servoStatusServer = ServoStatusService.Instance;
            var task = servoStatusServer.SetServo(servo);
            task.Wait();

            return new PostResponse(PostResponse.ResponseStatus.Created, $"/Servo/statuses", task.Result);
            }           
            catch (Exception ex)
            {
                return new PostResponse(PostResponse.ResponseStatus.Created);
            }
        }
        

        [UriFormat("/servo/servopositions")]
        public GetResponse GetServoStatuses()
        {
            try
            {
                ServoStatusService servoStatusServer = ServoStatusService.Instance;

                return new GetResponse(
                                 GetResponse.ResponseStatus.OK,
                                 servoStatusServer.ServoStatuses);

            }
            catch (Exception ex)
            {
                return new GetResponse(GetResponse.ResponseStatus.OK);
            }
        }   
    }
}
