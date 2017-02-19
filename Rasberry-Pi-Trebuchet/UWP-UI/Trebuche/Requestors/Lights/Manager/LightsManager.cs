using Raspberry_Pi_Trebuchet.UWP_UI.Desktop.Requestors.Lights.Controllers;
using Raspberry_Pi_Trebuchet.UWP_UI.Desktop.Requestors.Lights.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trebuchet.Interfaces;


namespace Raspberry_Pi_Trebuchet.UWP_UI.Desktop.Requestors.Lights.Manager
{
    public class LightsManager
    {
        private const string defaultLightColor = "#FFFFFFFF";

        IMainPaigeFlipViewModel _mainPageFlipViewModel;
        ILightsController _LightsController;
        public LightsManager(IMainPaigeFlipViewModel mainPaigeFlipViewModel)
        {
            _mainPageFlipViewModel = mainPaigeFlipViewModel;

            if (_mainPageFlipViewModel.UseAzure == true)
                _LightsController = new AzureLightsController(mainPaigeFlipViewModel);
            else
                _LightsController = new DirectIPLightsController(mainPaigeFlipViewModel);
        }



       public  async Task<bool> TurnLeftLightOn()
        {
           var lightRestViewmodel =  await  _LightsController.TurnLeftLightOn();

            if (lightRestViewmodel.IsLightOn == true)
            {
                _mainPageFlipViewModel.ColorLedLightLeft = _mainPageFlipViewModel.ColorLedLight;
            }
            else
                _mainPageFlipViewModel.ColorLedLightLeft = defaultLightColor;


            return true;
        }
        public async Task<bool> TurnLeftLightOff()
        {
            var lightRestViewModel =   await _LightsController.TurnLeftLightOff();
            _mainPageFlipViewModel.ColorLedLightLeft = defaultLightColor;
            return true;
        }

        public async Task<bool> TurnRightLightOn()
        {
            var lightRestViewmodel = await _LightsController.TurnRightLightOn();

            if (lightRestViewmodel.IsLightOn == true)
            {
                _mainPageFlipViewModel.ColorLedLightRight = _mainPageFlipViewModel.ColorLedLight;
            }
            else
                _mainPageFlipViewModel.ColorLedLightRight = defaultLightColor;

            return true;
        }


        public async Task<bool> TurnRightLightOff()
         {
            var lightRestViewModel = await _LightsController.TurnRightLightOff();
            _mainPageFlipViewModel.ColorLedLightRight = defaultLightColor;
            return true;
        }


       public  async Task<bool> TurnBothLightsOff()
           {
            var tasks = new List<Task<bool>>();

            tasks.Add(TurnRightLightOff());
            tasks.Add(TurnLeftLightOff());

            await Task.WhenAll(tasks.ToArray());

            return true;
        }


       public  async Task<bool> TurnBothLightsOn()
            {
            var tasks = new List<Task<bool>>();

            tasks.Add(TurnRightLightOn());
            tasks.Add(TurnLeftLightOn());

            await Task.WhenAll(tasks.ToArray());

            return true;
        }

        public async Task<bool> GetLightStatuses()
         {
             var lightRestViewmodels = await _LightsController.GetLightStatuses();

            //Is left Light On or Off            
            var LeftLight = (from lightRestViewmodel in lightRestViewmodels
                             where lightRestViewmodel.Description.ToUpper() == "LeftLight".ToUpper()
                             select lightRestViewmodel).FirstOrDefault();
            if (LeftLight?.IsLightOn == true)
                _mainPageFlipViewModel.ColorLedLightLeft = _mainPageFlipViewModel.ColorLedLight;
            else
                _mainPageFlipViewModel.ColorLedLightLeft = defaultLightColor;

            //Is Right Light On or Off
            var RightLight = (from lightRestViewmodel in lightRestViewmodels
                             where lightRestViewmodel.Description.ToUpper() == "LeftLight".ToUpper()
                             select lightRestViewmodel).FirstOrDefault();
            if (LeftLight?.IsLightOn == true)
                _mainPageFlipViewModel.ColorLedLightRight = _mainPageFlipViewModel.ColorLedLight;
            else
                _mainPageFlipViewModel.ColorLedLightRight = defaultLightColor;

            return true;
          }
    }
}
