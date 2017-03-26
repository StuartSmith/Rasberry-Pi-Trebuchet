using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Trebuchet.Requestors.Lights.Interfaces;
using Trebuchet.Interfaces;
using Raspberry_Pi_Trebuchet.RestUp.Lights.RestViewModels;

namespace Trebuchet.Requestors.Lights.Controllers
{
    public class AzureLightsController : ILightsController
    {
        IMainPaigeFlipViewModel _mainPageFlipViewModel;

        public AzureLightsController(IMainPaigeFlipViewModel mainPaigeFlipViewModel)
        {
            _mainPageFlipViewModel = mainPaigeFlipViewModel;
        }

        public Task<List<LightRestViewModel>> GetLightStatuses()
        {
            throw new NotImplementedException();
        }

        public Task<List<LightRestViewModel>> TurnBothLightsOff()
        {
            throw new NotImplementedException();
        }

        public Task<List<LightRestViewModel>> TurnBothLightsOn()
        {
            throw new NotImplementedException();
        }

        public Task<LightRestViewModel> TurnLeftLightOff()
        {
            throw new NotImplementedException();
        }

        public Task<LightRestViewModel> TurnLeftLightOn()
        {
            throw new NotImplementedException();
        }

        public Task<LightRestViewModel> TurnRightLightOff()
        {
            throw new NotImplementedException();
        }

        public Task<LightRestViewModel> TurnRightLightOn()
        {
            throw new NotImplementedException();
        }
    }
}
