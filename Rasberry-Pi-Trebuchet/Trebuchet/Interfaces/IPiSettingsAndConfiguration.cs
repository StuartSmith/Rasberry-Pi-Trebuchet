using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trebuchet.Interfaces
{
    public interface IPiSettingsAndConfiguration
    {
        int PiConfigid { get; set; }
        string Name { get; set; }
        string PiName { get; set; }
        string PiIp { get; set; }
        bool UserAzure { get; set; }
        bool SendToast { get; set; }      
        string ColorLedLight { get; set; }
        string ColorLedStroke { get; set;}
        string ColorPanelHighlight { get; set; }
        bool isConfigurationSetting { get; set; }
        bool UseIP { get; set; }

    }
}
