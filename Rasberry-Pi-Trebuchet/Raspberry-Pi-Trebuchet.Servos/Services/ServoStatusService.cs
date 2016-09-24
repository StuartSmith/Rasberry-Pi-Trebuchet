using Rasberry_Pi_Trebuchet.Common.Models;
using Rasberry_Pi_Trebuchet.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rasberry_Pi_Trebuchet.Common.Enums;
using Raspberry_Pi_Trebuchet.Servos.Interfaces;
using Raspberry_Pi_Trebuchet.Servos.Models;
using Rasberry_Pi_Trebuchet.Servos.RestViewModels;

namespace Rasberry_Pi_Trebuchet.IOT.Services
{
    public class ServoStatusService : IServoStatus
    {
        private static ServoStatusService _instance;
        private List<Servo> _Servos;
        private const int NumberOfTimesToSendMotorPulse = 25;

        private ServoStatusService()
        {
            _Servos = new List<Servo>();

            var servo = new Servo()
            {
                Description = ServoType.LaunchServo.ToString(),
                ServoStatus = ServoWhereAbouts.OneEightyDegrees.ToString(),
                ServoGPIO = RaspberryPiGPI0Pin.GPIO13,
                Servosensor = new Sensors.ServoSensor(RaspberryPiGPI0Pin.GPIO13)

            };

           // for (int x = 0; x < NumberOfTimesToSendMotorPulse; x++)
           //     servo.Servosensor.PulseMotor(RotateServer.RotateToRight);

            _Servos.Add(servo);
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

        
        /// <summary>
        /// Sets the servo status
        /// </summary>
        /// <param name="servo"></param>
        private void SetServoStatus(Servo servo)
        {
            // A little confusing but this is to create a mapping
            // between the Rotate left right middle and 
            // the 0, 90 and 180 degree enum

           switch (servo.ServoStatus.ToUpper()) { 
                case "ZERODEGREES":
                    for (int x = 0; x < NumberOfTimesToSendMotorPulse; x++)
                        servo.Servosensor.PulseMotor(RotateServer.RotateToLeft);
                    break;

                case "NINETYDEGREES":
                    for (int x = 0; x < NumberOfTimesToSendMotorPulse; x++)
                        servo.Servosensor.PulseMotor(RotateServer.RotateToMiddle);
                    break;

                case "ONEEIGHTYDEGREES":
                    for (int x = 0; x < NumberOfTimesToSendMotorPulse; x++)
                        servo.Servosensor.PulseMotor(RotateServer.RotateToRight);
                    break;

                default:
                    throw new Exception($"Unknown ServoStatus  {servo.ServoStatus.ToUpper()}");
                }
        }


        public async Task<bool> SetServo(Servo servo)
        {
            // Send Servo status data to azure
            var servoList = new List<Servo>();
            servoList.Add(servo);
            //await AzureConnectionService.Instance.SendServoData(servoList);


            var RetrieveServos = await Task<bool>.Factory.StartNew(() =>
            {
                var query = from selectedServo in _Servos
                            where servo?.Description?.ToUpper() == selectedServo?.Description?.ToUpper()
                            select selectedServo;

                var ServoToUpdate = query.FirstOrDefault<Servo>();
                ServoToUpdate.ServoStatus = servo.ServoStatus;
                SetServoStatus(ServoToUpdate);

                return true;

            });
            
            return RetrieveServos;

        }

        public async Task<List<IServoRestViewModel>> RetrieveServos()
        {

            List<IServoRestViewModel> RetrieveServos = await Task<List<IServoRestViewModel>>.Factory.StartNew(() =>
            {

                var query = from servo in _Servos
                            select new ServoRestViewModel(servo);

                return query.ToList<IServoRestViewModel>();

            });


            return RetrieveServos;
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
