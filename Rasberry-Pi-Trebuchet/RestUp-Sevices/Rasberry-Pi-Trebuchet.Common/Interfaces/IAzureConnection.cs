using Raspberry_Pi_Trebuchet.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.Common.Interfaces
{
   public  interface IAzureConnection
    {
        /// <summary>
        /// Stops the sending of any data to Azure
        /// </summary>
        bool AllowSendingofData { get; set; }

        /// <summary>
        /// Stops the sending of ultraSonic data to Azure
        /// Toast data to azure
        /// </summary>
        bool AllowSendingToastLightData { get; set; }

        /// <summary>
        /// Stops the sending of servo data to Azure
        /// Toast Notifications to Azure
        /// </summary>
        bool AllowSendingToastServoData { get; set; }

        /// <summary>
        /// Stops the sending of ultraSonic data to Azure
        /// </summary>
        bool AllowSendingUltraSonicData { get; set; }



        //Task<bool> SendServoData(List<Servo> data);

        //Task<bool> SendUltraSonicData(List<UltraSonicSensor> data);

        //Task<bool> SendLightData(List<ILightRestViewModel> data);



    }
}
