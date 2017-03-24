using Raspberry_Pi_Trebuchet.RestUp.Common.RestViewModels;
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


        private void RunAzureMsgListenerAsync()
        {

            if (!(IsAzureMsgListenerRunning))
            {
                LogMessage("Starting Azure Msg Listener");

                MSGListenerTask = Task<bool>.Factory.StartNew(()=> 
                {
                    IsAzureMsgListenerRunning = true;
                    while (IsAzureMsgListenerRunning == true)
                    {
                        LogMessage("Azure ListenerServiceRunning");

                        Task.Delay(1000);
                    }
                    return true;
                }
                );
            }
                        
        }
       

        private void LogMessage(string msg)
        {
            if (AzurelistenerStatusEvent != null)
                AzurelistenerStatusEvent(new OperationResult<bool>(true, msg));
        }

    }
}
