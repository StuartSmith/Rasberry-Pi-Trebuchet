using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trebuchet.Interfaces;

namespace Trebuchet.Models
{
    public class PiSettingsAndConfiguration: IPiSettingsAndConfiguration
    {
        [Key]
        public int PiConfigid { get; set; }

        public string Name { get; set; }
        public string PiName { get; set; }
        public string PiIP { get; set; }
        public bool UserAzure { get; set; }
        public bool SendToast { get; set; }       
        /// <summary>
        /// The Color of the Led lights displayed in the UI
        /// </summary>
        public string LedLightColor { get; set; }
        public  bool isConfigurationSetting { get; set; }     

        public static void CopyIPiconfigurationSettings(IPiSettingsAndConfiguration piConfig1, IPiSettingsAndConfiguration piConfig2)
        {
            piConfig1.PiConfigid = piConfig2.PiConfigid;
            piConfig1.isConfigurationSetting = piConfig2.isConfigurationSetting;
            piConfig1.LedLightColor = piConfig2.LedLightColor;
            piConfig1.Name = piConfig2.Name;
            piConfig1.PiIP = piConfig2.PiIP;
            piConfig1.PiName = piConfig2.PiName;
            piConfig1.SendToast = piConfig2.SendToast;
            piConfig1.UserAzure = piConfig2.UserAzure;

                
        }
    }
}
