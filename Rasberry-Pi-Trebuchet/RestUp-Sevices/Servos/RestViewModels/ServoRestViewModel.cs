using Raspberry_Pi_Trebuchet.Common.Enums;
using Raspberry_Pi_Trebuchet.RestUp.Servos.Interfaces;

namespace Raspberry_Pi_Trebuchet.RestUp.Servos.RestViewModels
{
    /// <summary>
    /// Object that is transfered back in forth in the form of JSON between the 
    /// server and the client
    /// </summary>
    public class ServoRestViewModel : IServoRestViewModel
    {
        public ServoRestViewModel() { }
        public ServoRestViewModel(IServoRestViewModel iServoViewModel)
        {
            this.Description = iServoViewModel.Description;
            this.ServoGPIO = iServoViewModel.ServoGPIO;
            this.ServoStatus = iServoViewModel.ServoStatus;
        }

        public string Description { get; set; }   

        public RaspberryPiGPI0Pin ServoGPIO { get; set; }

        public string ServoStatus { get; set; }






    }
}
