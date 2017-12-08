using Raspberry_Pi_Trebuchet.Common.Enums;
using Raspberry_Pi_Trebuchet.RestUp.Lights.Enums;

namespace Raspberry_Pi_Trebuchet.RestUp.Lights.Interfaces
{

    /// <summary>
    /// Inteface to define the object that gets passed back and forth
    /// between the server and the client
    /// </summary>
    public interface ILightRestViewModel
    {
        bool IsLightOn { get; set; }

        string Description { get; set; }

        LightType LightPosition { get; set; }

        RaspberryPiGPI0Pin LightGPIO { get; set; }
    }
}
