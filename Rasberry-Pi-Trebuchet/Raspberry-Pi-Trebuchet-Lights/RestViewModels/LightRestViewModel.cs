

using Rasberry_Pi_Trebuchet.Common.Enums;
using Raspberry_Pi_Trebuchet.Lights.Interfaces;

namespace Raspberry_Pi_Trebuchet.Lights.RestViewModels
{
    /// <summary>
    /// Object that is transfered back in forth in the form of JSON between the 
    /// server and the client
    /// </summary>
    public class LightRestViewModel : ILightRestViewModel
    {
        public LightRestViewModel() { }

        public LightRestViewModel(ILightRestViewModel iLightViewModel)
        {
            this.Description = iLightViewModel.Description;
            this.IsLightOn = iLightViewModel.IsLightOn;
            this.LightGPIO = iLightViewModel.LightGPIO;
            this.LightPosition = iLightViewModel.LightPosition;
         
        }
        public bool IsLightOn { get; set; }

        public string Description { get; set; }

        public LightType LightPosition { get; set; }

        public RaspberryPiGPI0Pin LightGPIO { get; set; }


    }
}
