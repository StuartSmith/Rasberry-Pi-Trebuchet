using Raspberry_Pi_Trebuchet.RestUp.Lights.RestViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.UWP_UI.Desktop.Requestors.Lights.Interfaces
{
    public interface ILightsController
    {
        Task<LightRestViewModel> TurnLeftLightOn();
        Task<LightRestViewModel> TurnLeftLightOff();
        Task<LightRestViewModel> TurnRightLightOn();
        Task<LightRestViewModel> TurnRightLightOff();

        //Task<List<LightRestViewModel>> TurnBothLightsOff();
        //Task<List<LightRestViewModel>> TurnBothLightsOn();

        Task<List<LightRestViewModel>> GetLightStatuses();

    }
}
