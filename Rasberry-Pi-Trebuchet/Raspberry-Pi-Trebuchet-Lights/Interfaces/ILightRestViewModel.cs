using Rasberry_Pi_Trebuchet.Common.Enums;

namespace Raspberry_Pi_Trebuchet.Lights.Interfaces
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
