using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raspberry_Pi_Trebuchet.Lights.RestViewModels;
using Trebuchet.Requestors.Lights.Interfaces;
using Trebuchet.Interfaces;

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
