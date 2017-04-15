//using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using Rasberry_Pi_Trebuchet.Common.RestViewModels;
using Raspberry_Pi_Trebuchet.Azure_WebService.Models.RestViewModels;
using Raspberry_Pi_Trebuchet.RestUp.Azure.RestViewModels;
using Raspberry_Pi_Trebuchet.RestUp.Common.RestViewModels;
using Raspberry_Pi_Trebuchet.RestUp.Configuration.Services;
using Raspberry_Pi_Trebuchet.RestUp.Lights.Controllers.api;
using Raspberry_Pi_Trebuchet.RestUp.Servos.Controllers.api;
using Raspberry_Pi_Trebuchet.RestUp.Sonic.Controllers.api;
using Raspberry_Pi_Trebuchet.RestUp.Trebuchet.Controllers.api;
using Restup.Webserver.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.Azure.Services
{
    public class AzureMsgListener
    {
        private static AzureMsgListener _instance;
        private static DeviceClient _deviceClient;

        public bool IsAzureMsgListenerRunning { get; set; } = false;

        public Action<OperationResult<bool>> AzurelistenerStatusEvent;

        private Task<bool> MSGListenerTask;


        private AzureMsgListener() { }
        public static AzureMsgListener Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AzureMsgListener();
                }
                return _instance;
            }
        }
       

        public OperationResult<bool> StartAzureMsgListener()
        {
            if (IsAzureMsgListenerRunning)
                return new OperationResult<bool>(true, "Azure Message Listener is currently running.");
            else
            {
                RunAzureMsgListenerAsync();
                return new OperationResult<bool>(true, "Starting Azure Message Listener");
            }
        }


        public OperationResult<bool> StopAzureMsgListener()
        {
            if (IsAzureMsgListenerRunning)
            { 
                IsAzureMsgListenerRunning = false;
                return new OperationResult<bool>(true, "Stopped Azure Message Listener");
            }
            else
            {
                return new OperationResult<bool>(true, "Azure Message Listener is currently not running.");
            }
        }

      
        private void RunAzureMsgListenerAsync()
        {
            if (!(IsAzureMsgListenerRunning))
            {
                LogMessage("Starting Azure Msg Listener");
                IsAzureMsgListenerRunning = true;

                MSGListenerTask = Task<bool>.Factory.StartNew(()=> 
                {
                    try
                    {
                        _deviceClient = CreateAzureIOTDeviceClient();

                        var restRouteHandler = new RestRouteHandler();

                        restRouteHandler.RegisterController<LightsController>();                       
                        restRouteHandler.RegisterController<ServoController>();
                        restRouteHandler.RegisterController<TrebuchetController>();
                        restRouteHandler.RegisterController<UltraSonicController>();

                        while (IsAzureMsgListenerRunning == true)
                        {
                            LogMessage("Azure ListenerServiceRunning");

                            Task<Message> RecievedMessageTask;
                            try
                            {
                                 RecievedMessageTask = _deviceClient.ReceiveAsync();
                                RecievedMessageTask.Wait();
                            }
                            catch (Exception ex)
                            {
                                var msglog = AzureMsgLogQueue.Instance;
                                msglog.addMsgToLog(new MsgContentToAndFromAzure(){ Response = ex.Message});
                                IsAzureMsgListenerRunning = false;
                                return false;                              
                            }

                            Message receivedMessage = RecievedMessageTask.Result;
                            if (receivedMessage == null)
                            {
                                Task.Delay(1000).Wait();
                                continue;                               
                            }
                            string value = Encoding.ASCII.GetString(receivedMessage.GetBytes());                           
                            //send Request to internal rest up 
                            MsgContentToAndFromAzure msgContentToAndFromAzure = new MsgContentToAndFromAzure(JsonConvert.DeserializeObject<MsgContentToAzure>(value));                            
                            var response = restRouteHandler.HandleRequest(msgContentToAndFromAzure.RestUpMsgRequest);

                            //Log Message once request is complete
                            msgContentToAndFromAzure.Response = System.Text.Encoding.UTF8.GetString(response.Result.Content);
                            AzureMsgLogQueue.Instance.addMsgToLog(msgContentToAndFromAzure);

                            //Create Object to send to Azure Web Service 
                            var rvm_IOTAzureRequestResponce = AzureMsgListener.Instance.Create_IOTAzureRequestResponce_From_MsgContentToAndFromAzure(msgContentToAndFromAzure);

                            //confirm message was recieved and processed
                            Task ConfirmReceiptTask = _deviceClient.CompleteAsync(receivedMessage);
                            ConfirmReceiptTask.Wait();

                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                   
                }
                );
            }                        
        }

        private DeviceClient CreateAzureIOTDeviceClient()
        {
            var azurePiConfiguration = new AzurePiConfiguration();
            var iotHubConnectionString = azurePiConfiguration.AzureIOTConnectionString;
            var deviceName = azurePiConfiguration.DeviceName.ToUpper();
            var  deviceClient = DeviceClient.CreateFromConnectionString(iotHubConnectionString, deviceName, TransportType.Mqtt);

            return deviceClient;

        }

        public IOTAzureDeviceResponce Create_IOTAzureRequestResponce_From_MsgContentToAndFromAzure(MsgContentToAndFromAzure msgContent)

        {
            IOTAzureDeviceResponce iOTDeviceResponce = new IOTAzureDeviceResponce();

            iOTDeviceResponce.MSGGUID =  msgContent.MSGGUID;
            iOTDeviceResponce.RequestContent = msgContent.RestUpMsgRequest.Content;
            iOTDeviceResponce.RequestContentLength = msgContent.RestUpMsgRequest.ContentLength;
            iOTDeviceResponce.RequestContentType = msgContent.RestUpMsgRequest.ContentType;
            iOTDeviceResponce.RequestUri = msgContent.RestUpMsgRequest.Uri;
            iOTDeviceResponce.Response = msgContent.Response;
            iOTDeviceResponce.ProcessedRequestDateTime = msgContent.ProcessedRequestDateTime;

           string msg =  JsonConvert.SerializeObject(iOTDeviceResponce);

            return iOTDeviceResponce;
        }


        private void LogMessage(string msg)
        {
            if (AzurelistenerStatusEvent != null)
                AzurelistenerStatusEvent(new OperationResult<bool>(true, msg));
            else
            {
             //To Do add code to log start and stop message to error log   
            }
        }

    }
}
