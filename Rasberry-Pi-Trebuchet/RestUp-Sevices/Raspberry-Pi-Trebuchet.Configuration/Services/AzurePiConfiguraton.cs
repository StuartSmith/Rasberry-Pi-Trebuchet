using Raspberry_Pi_Trebuchet.RestUp.Configuration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

            nameValuePairService.SetValueIfOneDoesNotExist(nameof(AzureIOTConnectionString), "");
            nameValuePairService.SetValueIfOneDoesNotExist(nameof(ToastWebSendURL), "");

        }


        public bool AllowSendingofData
        {
            get { return Convert.ToBoolean(new PiNameValuePairDBSettings().GetPiNameValuePair(nameof(AllowSendingofData))?.value); }
            set { new PiNameValuePairDBSettings().SetNameValuePair(nameof(AllowSendingofData), Convert.ToString(value)); }
        }

        public bool AllowSendingToastLightData
        {
            get { return Convert.ToBoolean(new PiNameValuePairDBSettings().GetPiNameValuePair(nameof(AllowSendingToastLightData))?.value); }
            set { new PiNameValuePairDBSettings().SetNameValuePair(nameof(AllowSendingToastLightData), Convert.ToString(value)); }
        }


        public bool AllowSendingToastServoData
        {
            get { return Convert.ToBoolean(new PiNameValuePairDBSettings().GetPiNameValuePair(nameof(AllowSendingToastServoData))?.value); }
            set { new PiNameValuePairDBSettings().SetNameValuePair(nameof(AllowSendingToastServoData), Convert.ToString(value)); }
        }


        public bool AllowSendingUltraSonicData
        {
            get { return Convert.ToBoolean(new PiNameValuePairDBSettings().GetPiNameValuePair(nameof(AllowSendingUltraSonicData))?.value); }
            set { new PiNameValuePairDBSettings().SetNameValuePair(nameof(AllowSendingUltraSonicData), Convert.ToString(value)); }
        }

        public void CopyKeyValuePair(IPiNameValuePair from, IPiNameValuePair to)
        {
            new PiNameValuePairDBSettings().CopyKeyValuePair(from, to);
        }



        public string AzureIOTConnectionString
        {
            get { return (new PiNameValuePairDBSettings().GetPiNameValuePair(nameof(AzureIOTConnectionString))?.value); }
            set { new PiNameValuePairDBSettings().SetNameValuePair(nameof(AzureIOTConnectionString), Convert.ToString(value)); }
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
