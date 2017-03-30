//using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using Rasberry_Pi_Trebuchet.Common.RestViewModels;
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
                return new OperationResult<bool>(true, "Stop Azure Message Listener");
            else
            {
                return new OperationResult<bool>(true, "Azure Message Listener is currently not running.");
            }
        }

       

        //Run Azure Message Listener Recieve Messages from Azure
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
                        _deviceClient = CreateAzureDeviceClient();

                        var restRouteHandler = new RestRouteHandler();

                        restRouteHandler.RegisterController<LightsController>();                       
                        restRouteHandler.RegisterController<ServoController>();
                        restRouteHandler.RegisterController<TrebuchetController>();
                        restRouteHandler.RegisterController<UltraSonicController>();
                       

                        while (IsAzureMsgListenerRunning == true)
                        {
                            LogMessage("Azure ListenerServiceRunning");
                            Task<Message> RecievedMessageTask = _deviceClient.ReceiveAsync();
                            RecievedMessageTask.Wait();
                            Message receivedMessage = RecievedMessageTask.Result;
                            if (receivedMessage == null)
                            {
                                Task.Delay(1000).Wait();
                                continue;
                               
                            }
                            string value = Encoding.ASCII.GetString(receivedMessage.GetBytes());

                            Task ConfirmReceiptTask =  _deviceClient.CompleteAsync(receivedMessage);
                            ConfirmReceiptTask.Wait();

                            MsgContentToAndFromAzure msgContentToAndFromAzure = new MsgContentToAndFromAzure(JsonConvert.DeserializeObject<MsgContentToAzure>(value));                            
                            var response = restRouteHandler.HandleRequest(msgContentToAndFromAzure.RestUpMsgRequest);

                            msgContentToAndFromAzure.Response = System.Text.Encoding.UTF8.GetString(response.Result.Content);
                            AzureMsgLogQueue.Instance.addMsgToQueue(msgContentToAndFromAzure);

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

        private DeviceClient CreateAzureDeviceClient()
        {
            var azurePiConfiguration = new AzurePiConfiguration();
            var iotHubConnectionString = azurePiConfiguration.AzureIOTConnectionString;
            var deviceName = azurePiConfiguration.DeviceName.ToUpper();
            var  deviceClient = DeviceClient.CreateFromConnectionString(iotHubConnectionString, deviceName, TransportType.Mqtt);

            return deviceClient;

        }



        private void LogMessage(string msg)
        {
            if (AzurelistenerStatusEvent != null)
                AzurelistenerStatusEvent(new OperationResult<bool>(true, msg));
        }

    }
}
