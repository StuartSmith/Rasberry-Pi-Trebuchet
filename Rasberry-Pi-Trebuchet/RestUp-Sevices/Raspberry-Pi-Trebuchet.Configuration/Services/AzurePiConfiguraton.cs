using Raspberry_Pi_Trebuchet.RestUp.Configuration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.Configuration.Services
{
   public class AzurePiConfiguration : IAzurePiConfiguraton
    {
        public AzurePiConfiguration()
        {
            var nameValuePairService = new PiNameValuePairDBSettings();


            //If value is not defined than then the rest service will not be able to set the value
            nameValuePairService.SetValueIfOneDoesNotExist(nameof(AllowSendingofData), "false");
            nameValuePairService.SetValueIfOneDoesNotExist(nameof(AllowSendingToastLightData), "true");
            nameValuePairService.SetValueIfOneDoesNotExist(nameof(AllowSendingToastServoData), "true");
            nameValuePairService.SetValueIfOneDoesNotExist(nameof(AllowSendingUltraSonicData), "true");

            nameValuePairService.SetValueIfOneDoesNotExist(nameof(AzureIOTConnectionString), "HostName=TrebuchetIOTHub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=cRSCEQHPczFag4TYDpnUcTdq9V/ABpt//diRRmYk/eE=");
            nameValuePairService.SetValueIfOneDoesNotExist(nameof(ToastWebSendURL), "");

            ///only set this value when we register
            nameValuePairService.SetValueIfOneDoesNotExist(nameof(DeviceName), Dns.GetHostName().ToUpper());
          
        }

        /// <summary>
        /// Allow the sending from the Raspberry pi back to azure
        /// </summary>
        public bool AllowSendingofData
        {
            get { return Convert.ToBoolean(new PiNameValuePairDBSettings().GetPiNameValuePair(nameof(AllowSendingofData))?.value); }
            set { new PiNameValuePairDBSettings().SetNameValuePair(nameof(AllowSendingofData), Convert.ToString(value)); }
        }

        /// <summary>
        /// Send a toast message each time the light data is changed
        /// </summary>
        public bool AllowSendingToastLightData
        {
            get { return Convert.ToBoolean(new PiNameValuePairDBSettings().GetPiNameValuePair(nameof(AllowSendingToastLightData))?.value); }
            set { new PiNameValuePairDBSettings().SetNameValuePair(nameof(AllowSendingToastLightData), Convert.ToString(value)); }
        }

        /// <summary>
        /// Send a toast message each time the servoe data is changed
        /// </summary>
        public bool AllowSendingToastServoData
        {
            get { return Convert.ToBoolean(new PiNameValuePairDBSettings().GetPiNameValuePair(nameof(AllowSendingToastServoData))?.value); }
            set { new PiNameValuePairDBSettings().SetNameValuePair(nameof(AllowSendingToastServoData), Convert.ToString(value)); }
        }

        /// <summary>
        /// Send the data from the ultra Sonic Sensor to Azure
        /// </summary>
        public bool AllowSendingUltraSonicData
        {
            get { return Convert.ToBoolean(new PiNameValuePairDBSettings().GetPiNameValuePair(nameof(AllowSendingUltraSonicData))?.value); }
            set { new PiNameValuePairDBSettings().SetNameValuePair(nameof(AllowSendingUltraSonicData), Convert.ToString(value)); }
        }

        public string AzureIOTConnectionString
        {
            get { return (new PiNameValuePairDBSettings().GetPiNameValuePair(nameof(AzureIOTConnectionString))?.value); }
            set { new PiNameValuePairDBSettings().SetNameValuePair(nameof(AzureIOTConnectionString), Convert.ToString(value)); }
        }

        /// <summary>
        /// makes a clone of the key value pair values
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void CopyKeyValuePair(IPiNameValuePair from, IPiNameValuePair to)
        {
            new PiNameValuePairDBSettings().CopyKeyValuePair(from, to);
        }


        public string DeviceName
        {
            get { return (new PiNameValuePairDBSettings().GetPiNameValuePair(nameof(DeviceName))?.value.ToUpper()); }
            set { new PiNameValuePairDBSettings().SetNameValuePair(nameof(DeviceName), value.ToUpper()); }
        }       

        public string ToastWebSendURL
        {
            get { return (new PiNameValuePairDBSettings().GetPiNameValuePair(nameof(ToastWebSendURL))?.value); }
            set { new PiNameValuePairDBSettings().SetNameValuePair(nameof(ToastWebSendURL), value); }
        }


        public List<IPiNameValuePair> GetAllValues()
        {
            return new PiNameValuePairDBSettings().GetAllNameValuePairs();
        }

        public bool UpdateValues(List<IPiNameValuePair> PiValuePairs)
        {
            return new PiNameValuePairDBSettings().SetAllNameValuePairs(PiValuePairs);
        }
    }
}
