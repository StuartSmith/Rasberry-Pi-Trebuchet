using Raspberry_Pi_Trebuchet.Servos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.Servos.Interfaces
{
    public interface IServoStatus
    {
        Task<List<Servo>> RetrieveServos();

        List<string> ServoStatuses { get; }

        Task<bool> SetServo(Servo servo);
    }
}
