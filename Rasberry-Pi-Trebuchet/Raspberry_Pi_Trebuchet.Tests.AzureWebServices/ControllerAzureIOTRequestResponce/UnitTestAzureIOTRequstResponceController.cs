using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Raspberry_Pi_Trebuchet.Azure_WebService.Models.RestViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.Tests.AzureWebServices.ControllerAzureIOTRequestResponce
{
    [TestClass]
    public class UnitTestAzureIOTRequstResponceController
    {
        /// <summary>
        /// A Message with be generated and sent to the device.
        /// The Message contains a unique identifier that can be used
        /// to trace the message back. When the device has finished processing
        /// it will put a message up in an Azure Web Service that
        /// the caller can retrieve. 
        /// Message path = Caller->IOT EVent HUB->Device->Azure Web Service-> Device -> IOT Event HUB -> Azure Web Service -> Caller
        /// </summary>
        [TestMethod]
        public void AzureRequestResponse_TestAddAndRetrieval()
        {
            IOTAzureDeviceResponce requestMessage = new IOTAzureDeviceResponce();
            Guid msgGuid = Guid.NewGuid();


        }

}
}
