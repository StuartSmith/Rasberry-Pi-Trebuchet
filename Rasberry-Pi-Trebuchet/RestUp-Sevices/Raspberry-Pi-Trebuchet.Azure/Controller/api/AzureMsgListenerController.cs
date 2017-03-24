using Raspberry_Pi_Trebuchet.RestUp.Azure.Services;
using Restup.Webserver.Attributes;
using Restup.Webserver.Models.Contracts;
using Restup.Webserver.Models.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.Azure.Controllers.api
{
    [RestController(InstanceCreationType.Singleton)]
    public class AzureMsgListenerController
    {
        [UriFormat("/azuremsglistener/status?={time}")]
        public GetResponse Status(string time)
        {
            AzureMsgListener msgListener = AzureMsgListener.Instance;

            return new GetResponse(
                                 GetResponse.ResponseStatus.OK,
                                 new { msgListener.IsAzureMsgListenerRunning});
          
        }


        [UriFormat("/azuremsglistener/start")]
        public IPutResponse Start()
        {
            try
            {
                AzureMsgListener msgListener = AzureMsgListener.Instance;
                var OperationResult =  msgListener.StartAzureMsgListener();
                return new PutResponse(PutResponse.ResponseStatus.OK, OperationResult);
            }
            catch(Exception ex)
            {
                return new PutResponse(PutResponse.ResponseStatus.NoContent);
            }      
        }


        [UriFormat("/azuremsglistener/stop")]
        public IPutResponse Stop()
        {
            try
            {
                AzureMsgListener msgListener = AzureMsgListener.Instance;
                var OperationResult = msgListener.StopAzureMsgListener();
                return new PutResponse(PutResponse.ResponseStatus.OK, OperationResult);
            }
            catch (Exception ex)
            {
                return new PutResponse(PutResponse.ResponseStatus.NoContent);
            }
        }



      

        }

}