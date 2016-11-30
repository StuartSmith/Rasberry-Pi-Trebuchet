using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trebuchet.Interfaces
{
    public interface IMainPaigeFlipViewModel: IPiSettingsAndConfiguration
    {    
   
        string ColorLedLightLeft { get; set; }
        string ColorLedLightRight { get; set; }
         string ColorLedStrokeLeft { get; set; }
         string ColorLedStrokeRight { get; set; }

    }
}
