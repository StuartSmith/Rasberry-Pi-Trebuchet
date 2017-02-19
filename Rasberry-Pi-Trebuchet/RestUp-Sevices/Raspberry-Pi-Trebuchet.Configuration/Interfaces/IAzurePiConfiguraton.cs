using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.Configuration.Interfaces
{
    public interface IAzurePiConfiguraton
    {
        bool AllowSendingofData { get; set; }
        bool AllowSendingToastLightData { get; set; }
        bool AllowSendingToastServoData { get; set; }
        bool AllowSendingUltraSonicData { get; set; }
        string AzureIOTConnectionString { get; set; }
        string ToastWebSendURL { get; set; }

        List<IPiNameValuePair> GetAllValues();
        bool UpdateValues(List<IPiNameValuePair> PiValuePairs);
    }
}
