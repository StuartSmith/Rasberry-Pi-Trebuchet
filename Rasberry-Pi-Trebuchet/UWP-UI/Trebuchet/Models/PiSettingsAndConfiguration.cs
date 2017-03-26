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
        /// <summary>
        /// The Color of the Led lights displayed in the UI
        /// </summary>
        public string ColorLedLight { get; set; }
        /// <summary>
        /// Stroke Color around the Led Light
        /// </summary>
        public string ColorLedStroke { get; set; }
        /// <summary>
        /// Background color for the led light
        /// </summary>
        public string ColorPanelHighlight { get; set; }
        public string Name { get; set; }
        public string PiName { get; set; }
        public string PiIp { get; set; }
        public bool UserAzure { get; set; }
        public bool SendToast { get; set; }       
      
        public string LedLightColor { get; set; }
        public  bool isConfigurationSetting { get; set; } 
        
        public bool UseAzure { get; set; }    

        public static void CopyIPiconfigurationSettings(IPiSettingsAndConfiguration piConfig1, IPiSettingsAndConfiguration piConfig2)
        {
            piConfig1.Name = piConfig2.Name;

            piConfig1.ColorLedLight = piConfig2.ColorLedLight;
            piConfig1.ColorLedStroke = piConfig2.ColorLedStroke;
            piConfig1.ColorPanelHighlight = piConfig2.ColorPanelHighlight;

            piConfig1.PiConfigid = piConfig2.PiConfigid;
            piConfig1.isConfigurationSetting = piConfig2.isConfigurationSetting;
           
            
            piConfig1.PiIp = piConfig2.PiIp;
            piConfig1.PiName = piConfig2.PiName;
           
            piConfig1.UseAzure = piConfig2.UseAzure;

            piConfig1.SendToast = piConfig2.SendToast;
        }
    }
}
