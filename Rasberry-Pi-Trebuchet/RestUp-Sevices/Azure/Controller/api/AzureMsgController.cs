using Raspberry_Pi_Trebuchet.RestUp.Azure.Enums;
using Raspberry_Pi_Trebuchet.RestUp.Azure.Services;
using Raspberry_Pi_Trebuchet.RestUp.Common.RestViewModels;
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
    public class AzureMsgController
    {
        [UriFormat("/azuremsglistener/registerdevice")]
        public async Task<IPutResponse> RegisterDevice([FromContent] bool data)
        {
            try
            {
                AzureDeviceRegistration azureRegistration = AzureDeviceRegistration.Instance;
                var OperationResult = await azureRegistration.RegisterDevice();
                return new PutResponse(PutResponse.ResponseStatus.OK, OperationResult);
            }
            catch 
            {
                var retval = new  OperationResult <RegisterDeviceStatus> (RegisterDeviceStatus.FailedToRegisterDevice , "Failed to Register Device");
                return new PutResponse(PutResponse.ResponseStatus.NoContent);
            }
        }

        [UriFormat("/azuremsglistener/loggedmessages?={time}")]
        public async Task<GetResponse> LoggedMessages(string time)
        {
            AzureMsgLogQueue msgListener = AzureMsgLogQueue.Instance;

            return new GetResponse(
                                 GetResponse.ResponseStatus.OK,
                                 await msgListener.RetrieveAllQueuedMessages());
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
            catch
            {
                return new PutResponse(PutResponse.ResponseStatus.NoContent);
            }      
        }

        [UriFormat("/azuremsglistener/status?={time}")]
        public GetResponse Status(string time)
        {
            AzureMsgListener msgListener = AzureMsgListener.Instance;

            return new GetResponse(
                                 GetResponse.ResponseStatus.OK,
                                 new { msgListener.IsAzureMsgListenerRunning });
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
            catch 
            {
                return new PutResponse(PutResponse.ResponseStatus.NoContent);
            }
        }


       
    }


   

}