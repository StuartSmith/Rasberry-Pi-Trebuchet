using Rasberry_Pi_Trebuchet.Common.ServiceContracts;
using Rasberry_Pi_Trebuchet.Common.ServiceContracts.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasberry_Pi_Trebuchet.IOT.Services
{
    public class ServoStatusService : IServoStatus
    {
        private static ServoStatusService _instance;
        private List<Servo> _Servos;

        private ServoStatusService()
        {
            _Servos = new List<Servo>();
            _Servos.Add(new Servo()
            {
                Description = ServoType.LaunchServo.ToString(),
                ServoStatus = ServoWhereAbouts.OneEightyDegrees.ToString(),
                ServoGPIO = RaspberryPiGPI0Pin.GPIO13
            });
        }

        public static ServoStatusService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ServoStatusService();
                }
                return _instance;
            }
        }

        //Sets the servo
        private void SetServoStatus(Servo servo)
        {

        }


        public async Task<bool> SetServo(Servo servo)
        {
            // Send Servo status data to azure
            var servoList = new List<Servo>();
            servoList.Add(servo);
            await AzureConnectionService.Instance.SendServoData(servoList);


            Task<bool> RetrieveServos = Task<bool>.Factory.StartNew(() =>
            {
                var query = from selectedServo in _Servos
                            where servo?.Description?.ToUpper() == selectedServo?.Description?.ToUpper()
                            select selectedServo;

                var ServoToUpdate = query.FirstOrDefault<Servo>();
                ServoToUpdate.ServoStatus = servo.ServoStatus;
                SetServoStatus(ServoToUpdate);
                return true;

            });

            return RetrieveServos.Result;

        }

        public async Task<List<Servo>> RetrieveServos()
        {

            Task<List<Servo>> RetrieveServos = Task<List<Servo>>.Factory.StartNew(() =>
            {
                return _Servos;

            });
            return RetrieveServos.Result;
        }


        /// <summary>
        /// Retrieves the different states a servo motor, the status can be
        /// 0 Degrees
        /// 90 Degrees
        /// 180 Degrees
        /// </summary>
        public List<string> ServoStatuses
        {
            get
            {
                List<string> servosStatuses = new List<string>();

                servosStatuses.Add(ServoWhereAbouts.NinetyDegrees.ToString());
                servosStatuses.Add(ServoWhereAbouts.OneEightyDegrees.ToString());
                servosStatuses.Add(ServoWhereAbouts.ZeroDegrees.ToString());

                return servosStatuses;
            }

        }
    }
}
