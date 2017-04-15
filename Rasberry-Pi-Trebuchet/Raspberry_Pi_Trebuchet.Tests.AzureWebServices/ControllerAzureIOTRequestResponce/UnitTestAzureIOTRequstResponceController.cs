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
        [TestMethod]
        public void AzureRequestResponse_TestAddAndRetrieval()
        {
            IOTAzureDeviceResponce requestMessage = new IOTAzureDeviceResponce();
            Guid msgGuid = Guid.NewGuid();


        }

}
}
