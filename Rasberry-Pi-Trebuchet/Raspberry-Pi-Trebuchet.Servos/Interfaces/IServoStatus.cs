using Rasberry_Pi_Trebuchet.Common.Models;
using Rasberry_Pi_Trebuchet.Servos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
