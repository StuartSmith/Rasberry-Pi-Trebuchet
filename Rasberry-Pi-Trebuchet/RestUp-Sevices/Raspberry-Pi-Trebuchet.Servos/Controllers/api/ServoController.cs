using Restup.Webserver.Attributes;
using Restup.Webserver.Models.Schemas;
using Restup.Webserver.Models.Contracts;
using System;

using Raspberry_Pi_Trebuchet.RestUp.Servos.Services;
using Raspberry_Pi_Trebuchet.RestUp.Servos.RestViewModels;

namespace Raspberry_Pi_Trebuchet.RestUp.Servos.Controllers.api
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
        public IPostResponse SetServoStatus([FromContent] ServoRestViewModel servo)
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
    }
}
