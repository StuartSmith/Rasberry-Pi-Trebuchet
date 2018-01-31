using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common;
using Raspberry_Pi_Trebuchet.RestUp.Azure.Enums;
using Raspberry_Pi_Trebuchet.RestUp.Azure.Model;
using Raspberry_Pi_Trebuchet.RestUp.Common.RestViewModels;
using Raspberry_Pi_Trebuchet.RestUp.Configuration.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.Azure.Services
{
    public class AzureDeviceRegistration
    {
        private const int _maxDevices = 1000;

        private static AzureDeviceRegistration _instance;

        private AzureDeviceRegistration() { }
        public static AzureDeviceRegistration Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AzureDeviceRegistration();
                }
                return _instance;
            }
        }


        public async Task <OperationResult<RegisterDeviceStatus>> RegisterDevice()
        {
            var retval = ValidateCurrentConfiguration();
            if (retval.Result == RegisterDeviceStatus.FailedToRegisterDevice)
                return retval;

            var azurePiConfigurtion = new AzurePiConfiguration();
            var iotHubConnectionString = azurePiConfigurtion.AzureIOTConnectionString;
            var deviceName = azurePiConfigurtion.DeviceName.ToUpper();

            RegistryManager registryManager = RegistryManager.CreateFromConnectionString(iotHubConnectionString);

            var listofDevices = await GetDevicesAsync(registryManager, _maxDevices);
            if (listofDevices.Where(x => x.Id == deviceName).Any())
            {
                return new OperationResult<RegisterDeviceStatus>(RegisterDeviceStatus.DeviceAlreadyRegistered, $"Device Already registered {deviceName}");
            }

            var device = new Device(deviceName)
            {
                Authentication = new AuthenticationMechanism()
            };

            device.Authentication.SymmetricKey.PrimaryKey = CryptoKeyGenerator.GenerateKey(32);
            device.Authentication.SymmetricKey.SecondaryKey = CryptoKeyGenerator.GenerateKey(32);

            await registryManager.AddDeviceAsync(device);

            return new OperationResult<RegisterDeviceStatus>(RegisterDeviceStatus.RegisteredDevice, $"Successfully registered the Device {deviceName}"); 
        }


        private OperationResult<RegisterDeviceStatus> IsAzureConnectionstringValid()
        {
            var result = new OperationResult<RegisterDeviceStatus>(RegisterDeviceStatus.RegisteredDevice,"");
            var azurePiConfigurtion = new AzurePiConfiguration();
            var AzureConnection = azurePiConfigurtion.AzureIOTConnectionString;

            if (string.IsNullOrEmpty(azurePiConfigurtion.AzureIOTConnectionString))
                return new OperationResult<RegisterDeviceStatus>(RegisterDeviceStatus.FailedToRegisterDevice, "Azure IOT Connection string is empty please provide a valid IOT Connection string.");

            return result;
        }


        private OperationResult<RegisterDeviceStatus> ValidateCurrentConfiguration()
        {
            var retval = IsAzureConnectionstringValid();
            if (retval.Result == RegisterDeviceStatus.FailedToRegisterDevice)
                return retval;

            ValidateDeviceName();

            return new OperationResult<RegisterDeviceStatus>(RegisterDeviceStatus.RegisteredDevice, "");
        }

        /// <summary>
        /// Retrieve the Machine Name if one is not present
        /// </summary>
        /// <returns></returns>
        private void ValidateDeviceName()
        {
            var azurePiConfiguration = new AzurePiConfiguration();
            var DeviceName = azurePiConfiguration.DeviceName;

            if (string.IsNullOrEmpty(DeviceName))
            {
                DeviceName = Dns.GetHostName();
                azurePiConfiguration.DeviceName = DeviceName;
            }
        }

        public async Task<List<DeviceEntity>> GetDevicesAsync(RegistryManager registryManager, int MaxCountDevices)
        {
            List<DeviceEntity> listOfDevices = new List<DeviceEntity>();

            try
            {
                DeviceEntity deviceEntity;
                var devices = await registryManager.GetDevicesAsync(MaxCountDevices);

                if (devices != null)
                {
                    foreach (var device in devices)
                    {
                        deviceEntity = new DeviceEntity()
                        {
                            Id = device.Id,
                            ConnectionState = device.ConnectionState.ToString(),
                            //ConnectionString = CreateDeviceConnectionString(device),
                            LastActivityTime = device.LastActivityTime,
                            LastConnectionStateUpdatedTime = device.ConnectionStateUpdatedTime,
                            LastStateUpdatedTime = device.StatusUpdatedTime,
                            MessageCount = device.CloudToDeviceMessageCount,
                            State = device.Status.ToString(),
                            SuspensionReason = device.StatusReason
                        };

                        if (device.Authentication != null)
                        {

                            deviceEntity.PrimaryKey = device.Authentication.SymmetricKey?.PrimaryKey;
                            deviceEntity.SecondaryKey = device.Authentication.SymmetricKey?.SecondaryKey;
                            deviceEntity.PrimaryThumbPrint = device.Authentication.X509Thumbprint?.PrimaryThumbprint;
                            deviceEntity.SecondaryThumbPrint = device.Authentication.X509Thumbprint?.SecondaryThumbprint;
                                                       
                        }

                        listOfDevices.Add(deviceEntity);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listOfDevices;
        }



    }
}
