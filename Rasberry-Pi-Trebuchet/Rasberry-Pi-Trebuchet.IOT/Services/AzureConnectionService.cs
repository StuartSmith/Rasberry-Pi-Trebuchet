using Raspberry_Pi_Trebuchet.Common.Interfaces;
using Raspberry_Pi_Trebuchet.Common.Models;
using Raspberry_Pi_Trebuchet.Configuration.Services;
using Raspberry_Pi_Trebuchet.Lights.Interfaces;
using Raspberry_Pi_Trebuchet.Servos.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.IOT.Services
{
    /// <summary>
    /// Class to Manage if the pi can and should 
    /// connect to the Azure IOT Hub 
    /// </summary>
    /// 
    /// <example>
    /// var instance = new AzureConnectionService.Instance;
    /// 
    /// instance.SetAzureConfiguration(new  AzurePiConfiguraton())    
    /// </example>
    public class AzureConnectionService : AzurePiConfiguraton, IAzureConnection
    {
        private static AzureConnectionService _instance;


        private AzureConnectionService()
        {
            AllowSendingofData = false;
            AllowSendingToastLightData = true;
            AllowSendingToastServoData = true;
            AllowSendingUltraSonicData = true;
        }


        public static AzureConnectionService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AzureConnectionService();
                }
                return _instance;
            }
        }

      

        public async Task<bool> SendLightData(List<ILightRestViewModel> data)
        {

            if ((AllowSendingofData == true) && (AllowSendingToastLightData == true))
            {
                throw new NotImplementedException();
            }

           bool SendData = await Task<bool>.Factory.StartNew(() =>
            {
                return true;

            });

            return SendData;
        }


        public async Task<bool> SendServoData(List<Servo> data)
        {
            if ((AllowSendingofData == true) && (AllowSendingToastServoData == true))
            {
                throw new NotImplementedException();
            }

            Task<bool> SendData = Task<bool>.Factory.StartNew(() =>
            {
                return true;

            });

            return SendData.Result;
        }


        public async Task<bool> SendUltraSonicData(List<UltraSonicSensor> data)
        {
            if ((AllowSendingofData == true) && (this.AllowSendingUltraSonicData == true))
            {
                throw new NotImplementedException();
            }

            Task<bool> SendData = Task<bool>.Factory.StartNew(() =>
            {
                return true;

            });

            return SendData.Result;
        }

        public bool SetAzureConfiguration(AzurePiConfiguraton azureConfig)
        {
            this.AllowSendingofData = azureConfig.AllowSendingofData;
            this.AllowSendingToastLightData = azureConfig.AllowSendingToastLightData;
            this.AllowSendingToastServoData = azureConfig.AllowSendingToastServoData;
            this.AllowSendingUltraSonicData = azureConfig.AllowSendingUltraSonicData;

            return true;
        }

        public AzurePiConfiguraton GetAzureConfiguration()
        {

            var azureConfig = new AzurePiConfiguraton();

            azureConfig.AllowSendingofData = this.AllowSendingofData;
            azureConfig.AllowSendingToastLightData = this.AllowSendingToastLightData;

            azureConfig.AllowSendingToastServoData = this.AllowSendingToastServoData;
            azureConfig.AllowSendingUltraSonicData = this.AllowSendingUltraSonicData;

            return azureConfig;
        }

      
    }
}
